using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private SecretSantaWebApplicationFactory Factory { get; set; } = null!;
        private HttpClient Client { get; set; } = null!;
        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        private JsonSerializerOptions _JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };



        [TestInitialize]
        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();

            using Data.ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();

            Client = Factory.CreateClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Factory.Dispose();
        }

        [TestMethod]
        public async Task Get_WithUsersInDB_ReturnsUsers()
        {
            // Arrange
            using Data.ApplicationDbContext context = Factory.GetDbContext();
            Data.User im = SampleData.CreateDataUser1();
            context.Users.Add(im);
            context.SaveChanges();
            Uri uri = new Uri("api/User", UriKind.Relative);

            // Act
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.User[] users =
                JsonSerializer.Deserialize<Business.Dto.User[]>(jsonData, options);
            Assert.AreEqual(1, users.Length);

            Assert.AreEqual(im.Id, users[0].Id);
            Assert.AreEqual(im.FirstName, users[0].FirstName);
            Assert.AreEqual(im.LastName, users[0].LastName);

        }


        [TestMethod]
        public async Task Get_ValidId_ReturnsUser()
        {
            // Arrange
            using Data.ApplicationDbContext context = Factory.GetDbContext();
            Data.User im = SampleData.CreateDataUser1();
            context.Users.Add(im);
            context.SaveChanges();
            Uri uri = new Uri("api/User/1", UriKind.Relative);

            // Act
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

           
            Business.Dto.User user =
                JsonSerializer.Deserialize<Business.Dto.User>(jsonData, _JsonOptions);
      

            Assert.AreEqual(im.Id, user.Id);
            Assert.AreEqual(im.FirstName, user.FirstName);
            Assert.AreEqual(im.LastName, user.LastName);

        }



        [TestMethod]
        public async Task Get_InvalidId_Fails()
        {
            // Arrange
          
            Uri uri = new Uri("api/User/1", UriKind.Relative);

            // Act
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            Assert.IsTrue(response.StatusCode is HttpStatusCode.NotFound);
           

        }

        [TestMethod]
        public async Task Delete_ValidId_Ok()
        {
            // Arrange
            using Data.ApplicationDbContext context = Factory.GetDbContext();
            Data.User im = SampleData.CreateDataUser1();
            context.Users.Add(im);
            context.SaveChanges();

            Uri uri = new Uri("api/User/1", UriKind.Relative);

            // Act
            HttpResponseMessage response = await Client.DeleteAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            

            using Data.ApplicationDbContext assertContext = Factory.GetDbContext();

            Assert.IsNull(assertContext.Users.Find(im.Id));


        }

        [TestMethod]
        public async Task Delete_InValidId_Fails()
        {
            // Arrange
          
            Uri uri = new Uri("api/User/1", UriKind.Relative);

            // Act
            HttpResponseMessage response = await Client.DeleteAsync(uri);

            // Assert
            Assert.IsTrue(response.StatusCode is HttpStatusCode.NotFound);


        }




        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            // Arrange
            Business.Dto.UserInput im = Mapper.Map<Data.User, Business.Dto.UserInput>(SampleData.CreateDataUser1());
            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/User/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);


            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithValidId_Ok()
        {

            //arrange
            using Data.ApplicationDbContext context = Factory.GetDbContext();
            var user1 = SampleData.CreateDataUser1();
            var savedUser = context.Users.Add(user1);
            context.SaveChanges();



            Business.Dto.UserInput inputUser = Mapper.Map<Data.User, Business.Dto.UserInput>(SampleData.CreateDataUser2());
            string jsonData = JsonSerializer.Serialize(inputUser);
            using StringContent inputUserStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Uri uri = new Uri("api/User/1", UriKind.Relative);

            //act
            HttpResponseMessage responseMessage = await Client.PutAsync(uri, inputUserStringContent);

            //assert
            responseMessage.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
            string retunedJson = await responseMessage.Content.ReadAsStringAsync();


            Business.Dto.User returnedUser = JsonSerializer.Deserialize<Business.Dto.User>(retunedJson, _JsonOptions);


            // Assert that returnedAuthor matches im values
            Assert.AreEqual<string>(inputUser.FirstName!, returnedUser.FirstName!);
            Assert.AreEqual<string>(inputUser.LastName!, returnedUser.LastName!);


            // Assert that returnedAuthor matches database value
            using Data.ApplicationDbContext assertContext = Factory.GetDbContext();

            Data.User databaseUser = assertContext.Users.Find(returnedUser.Id);
            Assert.AreEqual<string>(databaseUser.FirstName!, returnedUser.FirstName!);
            Assert.AreEqual<string>(databaseUser.LastName!, returnedUser.LastName!);


        }

        [TestMethod]
        public async Task Post_WithValidUserInput_Ok()
        {
            //arrange
            Business.Dto.UserInput inputUser = Mapper.Map<Data.User, Business.Dto.UserInput>(SampleData.CreateDataUser1());
            string jsonData = JsonSerializer.Serialize(inputUser);
            using StringContent inputUserStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Uri uri = new Uri("api/User/", UriKind.Relative);

            HttpResponseMessage responseMessage = await Client.PostAsync(uri, inputUserStringContent);

            //assert
            //ensure success
            responseMessage.EnsureSuccessStatusCode();

            string returnedJson = await responseMessage.Content.ReadAsStringAsync();


            Business.Dto.User returnedUser = JsonSerializer.Deserialize<Business.Dto.User>(returnedJson, _JsonOptions);

            //assert matches returned jason object matches input jason object

            Assert.AreEqual<string>(inputUser.FirstName!, returnedUser.FirstName!);
            Assert.AreEqual<string>(inputUser.LastName!, returnedUser.LastName!);

            //assert matches stored entity
            using Data.ApplicationDbContext assertContext = Factory.GetDbContext();

            Data.User databaseUser = assertContext.Users.Find(returnedUser.Id);
            Assert.AreEqual<string>(databaseUser.FirstName!, returnedUser.FirstName!);
            Assert.AreEqual<string>(databaseUser.LastName!, returnedUser.LastName!);

        }



        
        //trying to figure out how to do this test by getting the property name dynamically
        // was just passing in literal "FirstName" to get the FirstName property. Not sure if the enum is any better

        public enum DtoInputUserPropName 
        {
            FirstName = 0,
            LastName = 1,
        }

        [DataTestMethod]
        [DataRow(DtoInputUserPropName.FirstName, null)]
        [DataRow(DtoInputUserPropName.LastName, null)]

        public async Task Post_UserInputWithMissingProperty_Fails(DtoInputUserPropName prop, string? propValue)
        {

            //arrange
            Business.Dto.UserInput inputUser = Mapper.Map<Data.User, Business.Dto.UserInput>(SampleData.CreateDataUser1());

            Type inputUserType = typeof(Business.Dto.UserInput);
            string propName = typeof(Data.User).GetProperties()[(int)prop].Name;
            PropertyInfo piInputUser = inputUserType.GetProperty(propName)!;
            piInputUser.SetValue(inputUser, propValue);

            string jsonData = JsonSerializer.Serialize(inputUser);
            using StringContent inputUserStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Uri uri = new Uri("api/User/", UriKind.Relative);

            //act
            HttpResponseMessage responseMessage = await Client.PostAsync(uri, inputUserStringContent);
            //Trace.WriteLine(responseMessage.Content.);

            //assert
            Assert.IsTrue(responseMessage.StatusCode is HttpStatusCode.BadRequest);

        }





    }
}
