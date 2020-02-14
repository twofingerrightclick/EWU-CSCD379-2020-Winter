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
    public class GiftControllTests
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
        public async Task Get_WithGiftsInDB_ReturnsGifts()
        {
            // Arrange
            using Data.ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift im = SampleData.CreateGift();
            context.Gifts.Add(im);
            context.SaveChanges();
            Uri uri = new Uri("api/Gift", UriKind.Relative);

            // Act
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Gift[] gifts =
                JsonSerializer.Deserialize<Business.Dto.Gift[]>(jsonData, options);

            Assert.AreEqual(1, gifts.Length);

            Assert.AreEqual(im.Id, gifts[0].Id);
            Assert.AreEqual(im.Title, gifts[0].Title);
            Assert.AreEqual(im.Description, gifts[0].Description);
            Assert.AreEqual(im.Url, gifts[0].Url);
            Assert.AreEqual(im.UserId, gifts[0].UserId);

        }


        [TestMethod]
        public async Task Get_ValidId_ReturnsGift()
        {
            // Arrange
            using Data.ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift im = SampleData.CreateGift();
            context.Gifts.Add(im);
            context.SaveChanges();
            Uri uri = new Uri("api/Gift/1", UriKind.Relative);

            // Act
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();


            Business.Dto.Gift gift =
                JsonSerializer.Deserialize<Business.Dto.Gift>(jsonData, _JsonOptions);


            Assert.AreEqual(im.Id, gift.Id);
            Assert.AreEqual(im.Title, gift.Title);
            Assert.AreEqual(im.Description, gift.Description);
            Assert.AreEqual(im.Url, gift.Url);
            Assert.AreEqual(im.UserId, gift.UserId);


        }



        [TestMethod]
        public async Task Get_InvalidId_Fails()
        {
            // Arrange

            Uri uri = new Uri("api/Gift/1", UriKind.Relative);

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
            Data.Gift im = SampleData.CreateGift();
            context.Gifts.Add(im);
            context.SaveChanges();

            Uri uri = new Uri("api/Gift/1", UriKind.Relative);

            // Act
            HttpResponseMessage response = await Client.DeleteAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299


            using Data.ApplicationDbContext assertContext = Factory.GetDbContext();

            Assert.IsNull(assertContext.Gifts.Find(im.Id));


        }

        [TestMethod]
        public async Task Delete_InValidId_Fails()
        {
            // Arrange

            Uri uri = new Uri("api/Gift/1", UriKind.Relative);

            // Act
            HttpResponseMessage response = await Client.DeleteAsync(uri);

            // Assert
            Assert.IsTrue(response.StatusCode is HttpStatusCode.NotFound);


        }




        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            // Arrange

            Business.Dto.GiftInput im = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(SampleData.CreateGift());
            string jsonData = JsonSerializer.Serialize(im);
            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/Gift/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);


            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithValidId_AndValidForeignUserId_Ok()
        {


            //arrange
            using Data.ApplicationDbContext context = Factory.GetDbContext();
            var storedUser = context.Users.Add(SampleData.CreateDataUser1());

            var storedGift = context.Gifts.Add(SampleData.CreateGift());
            context.SaveChanges();


            Business.Dto.GiftInput inputGift = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(SampleData.CreateGift());
            inputGift.UserId = storedUser.Entity.Id;
            string jsonData = JsonSerializer.Serialize(inputGift);
            using StringContent inputGiftStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Uri uri = new Uri($"api/Gift/{storedGift.Entity.Id}", UriKind.Relative);

            //act
            HttpResponseMessage responseMessage = await Client.PutAsync(uri, inputGiftStringContent);

            //assert
            responseMessage.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
            string retunedJson = await responseMessage.Content.ReadAsStringAsync();


            Business.Dto.Gift returnedGift = JsonSerializer.Deserialize<Business.Dto.Gift>(retunedJson, _JsonOptions);


            // Assert that returnedGift matches im values
            Assert.AreEqual<string>(inputGift.Title!, returnedGift.Title!);
            Assert.AreEqual<string>(inputGift.Description!, returnedGift.Description!);
            Assert.AreEqual<string>(inputGift.Url!, returnedGift.Url!);
            Assert.AreEqual(inputGift.UserId, returnedGift.UserId);


            // Assert that returnedGift matches database value
            using Data.ApplicationDbContext assertContext = Factory.GetDbContext();

            Data.Gift databaseGift = assertContext.Gifts.Find(returnedGift.Id);

            Assert.AreEqual<string>(databaseGift.Title!, returnedGift.Title!);
            Assert.AreEqual<string>(databaseGift.Description!, returnedGift.Description!);
            Assert.AreEqual<string>(databaseGift.Url!, returnedGift.Url!);
            Assert.AreEqual(databaseGift.UserId, returnedGift.UserId);


        }



        [TestMethod]
        public async Task Put_WithValidId_InvalidForeignUserId_Fails()
        {

            //arrange
            using Data.ApplicationDbContext context = Factory.GetDbContext();


            var storedGift = context.Gifts.Add(SampleData.CreateGift());
            context.SaveChanges();


            Business.Dto.GiftInput inputGift = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(SampleData.CreateGift());
            inputGift.UserId = 42;
            string jsonData = JsonSerializer.Serialize(inputGift);
            using StringContent inputGiftStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Uri uri = new Uri($"api/Gift/{storedGift.Entity.Id}", UriKind.Relative);

            //act
            HttpResponseMessage responseMessage = await Client.PutAsync(uri, inputGiftStringContent);

            //assert

            Assert.AreEqual(HttpStatusCode.InternalServerError, responseMessage.StatusCode);


        }




        [TestMethod]
        public async Task Post_WithValidGiftInput_AndValidForeignUserId_Ok()
        {
            //arrange
            using Data.ApplicationDbContext context = Factory.GetDbContext();
            var storedUser = context.Users.Add(SampleData.CreateDataUser1());

            var storedGift = context.Gifts.Add(SampleData.CreateGift());
            context.SaveChanges();


            Business.Dto.GiftInput inputGift = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(SampleData.CreateGift());
            inputGift.UserId = storedUser.Entity.Id;
            string jsonData = JsonSerializer.Serialize(inputGift);
            using StringContent inputGiftStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Uri uri = new Uri("api/Gift/", UriKind.Relative);

            HttpResponseMessage responseMessage = await Client.PostAsync(uri, inputGiftStringContent);

            //assert

            responseMessage.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);


            string returnedJson = await responseMessage.Content.ReadAsStringAsync();


            Business.Dto.Gift returnedGift = JsonSerializer.Deserialize<Business.Dto.Gift>(returnedJson, _JsonOptions);

            // Assert that returnedGift matches im values
            Assert.AreEqual<string>(inputGift.Title!, returnedGift.Title!);
            Assert.AreEqual<string>(inputGift.Description!, returnedGift.Description!);
            Assert.AreEqual<string>(inputGift.Url!, returnedGift.Url!);
            Assert.AreEqual(inputGift.UserId, returnedGift.UserId);


            // Assert that returnedGift matches database value
            using Data.ApplicationDbContext assertContext = Factory.GetDbContext();

            Data.Gift databaseGift = assertContext.Gifts.Find(returnedGift.Id);

            Assert.AreEqual<string>(databaseGift.Title!, returnedGift.Title!);
            Assert.AreEqual<string>(databaseGift.Description!, returnedGift.Description!);
            Assert.AreEqual<string>(databaseGift.Url!, returnedGift.Url!);
            Assert.AreEqual(databaseGift.UserId, returnedGift.UserId);


        }



        [TestMethod]
        public async Task Post_WithValidId_InvalidForeignUserId_Fails()
        {

            //arrange
            Business.Dto.GiftInput inputGift = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(SampleData.CreateGift());
            inputGift.UserId = 42;
            string jsonData = JsonSerializer.Serialize(inputGift);
            using StringContent inputGiftStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Uri uri = new Uri($"api/Gift/", UriKind.Relative);

            //act
            HttpResponseMessage responseMessage = await Client.PostAsync(uri, inputGiftStringContent);

            //assert

            Assert.AreEqual(HttpStatusCode.InternalServerError, responseMessage.StatusCode);


        }




        [DataTestMethod]
        [DataRow(nameof(Business.Dto.GiftInput.Title), null)]
        [DataRow(nameof(Business.Dto.GiftInput.Description), null)]
        [DataRow(nameof(Business.Dto.GiftInput.Url), null)]
       

        public async Task Post_GiftInputWithMissingProperty_Fails(string propName, string? propValue)
        {

            //arrange

            using Data.ApplicationDbContext context = Factory.GetDbContext();
            var storedUser = context.Users.Add(SampleData.CreateDataUser1());
            context.SaveChanges();

            Business.Dto.GiftInput inputGift = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(SampleData.CreateGift());
            inputGift.UserId = storedUser.Entity.Id;
            Type inputGiftType = typeof(Business.Dto.GiftInput);
            
            PropertyInfo piInputGift = inputGiftType.GetProperty(propName!);
            piInputGift.SetValue(inputGift, propValue);

            string jsonData = JsonSerializer.Serialize(inputGift);
            using StringContent inputGiftStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Uri uri = new Uri("api/Gift/", UriKind.Relative);

            //act
            HttpResponseMessage responseMessage = await Client.PostAsync(uri, inputGiftStringContent);
            //Trace.WriteLine(responseMessage.Content.);

            //assert
            Assert.IsTrue(responseMessage.StatusCode is HttpStatusCode.BadRequest);

        }

    }
}
