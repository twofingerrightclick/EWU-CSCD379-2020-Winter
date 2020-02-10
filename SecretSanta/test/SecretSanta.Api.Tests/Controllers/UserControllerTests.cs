using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private SecretSantaWebApplicationFactory Factory { get; set; }
        private HttpClient Client { get; set; }
        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();



        [TestInitialize]
        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();

            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();

            Client = Factory.CreateClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Factory.Dispose();
        }

        [TestMethod]
        public async Task Get_ReturnsUsers()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
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
        public async Task Put_WithMissingId_NotFound()
        {
            // Arrange
            Business.Dto.UserInput im = Mapper.Map<User, Business.Dto.User>(SampleData.CreateDataUser1());
            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/User/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);
            

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithId_Ok()
        {

            //arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            var user1 = SampleData.CreateDataUser1();
            var savedUser = await context.AddAsync<User>(user1);
            context.SaveChanges();



            Business.Dto.UserInput im = Mapper.Map<User, Business.Dto.User>(SampleData.CreateDataUser2());
            string jsonData = JsonSerializer.Serialize(im);
            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Uri uri = new Uri("api/User/1", UriKind.Relative);

            //act
            HttpResponseMessage responseMessage = await Client.PutAsync(uri, stringContent);
            //assert

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);





        }


    }
}
