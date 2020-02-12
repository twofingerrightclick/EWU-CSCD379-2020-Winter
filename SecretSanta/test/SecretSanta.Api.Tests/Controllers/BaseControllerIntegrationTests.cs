using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Tests;
using SecretSanta.Data;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecretSanta.Api.IntegrationTests
{

    [TestClass]
    public abstract class BaseControllerIntegrationTests<TDto, TInputDto>
    {
        protected abstract string BaseUrl { get; }

        protected abstract TInputDto CreateInputDto();

        protected abstract bool ExistsInDatabase(TDto dto);

        //Set in test initialize
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        protected SecretSantaWebApplicationFactory Factory { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        [TestInitialize]
        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();

            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();

            SeedData(context);
            context.SaveChanges();
        }

        private static void SeedData(ApplicationDbContext context)
        {
            var group = new Data.Group("Test");
            context.Groups.Add(group);

            var user = new Data.User("First", "Last");
            context.Users.Add(user);

            var gift = new Data.Gift("Title", "url", "description", user);
            context.Gifts.Add(gift);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Factory.Dispose();
        }

        [TestMethod]
        public async Task GetEndpoints_ReturnSuccessAndSingleArrayElement()
        {
            // Arrange
            HttpClient client = Factory.CreateClient();
            var uri = new Uri(BaseUrl, UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonString = await response.Content.ReadAsStringAsync();

            Array items = (Array)JsonSerializer.Deserialize(jsonString, typeof(TDto).MakeArrayType());
            Assert.AreEqual(1, items.Length);
        }

        [TestMethod]
        public async Task GetEndpoint_WithId_ReturnSuccessAndItem()
        {
            // Arrange
            HttpClient client = Factory.CreateClient();
            var uri = new Uri($"{BaseUrl}/1", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonString = await response.Content.ReadAsStringAsync();

            TDto dto = JsonSerializer.Deserialize<TDto>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            Assert.IsTrue(ExistsInDatabase(dto));
        }

        [TestMethod]
        public async Task GetEndpoint_WithBadId_ReturnNotFound()
        {
            // Arrange
            HttpClient client = Factory.CreateClient();
            var uri = new Uri($"{BaseUrl}/99", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await client.GetAsync(uri);

            // Assert
            Assert.AreEqual(StatusCodes.Status404NotFound, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task PostEndpoint_WithValidData_ReturnsCreatedInstance()
        {
            // Arrange

            HttpClient client = Factory.CreateClient();
            var uri = new Uri(BaseUrl, UriKind.RelativeOrAbsolute);
            using var content = new StringContent(JsonSerializer.Serialize(CreateInputDto()), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await client.PostAsync(uri, content);

            // Assert
            response.EnsureSuccessStatusCode();
            string jsonString = await response.Content.ReadAsStringAsync();
            TDto dto = JsonSerializer.Deserialize<TDto>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            Assert.IsTrue(ExistsInDatabase(dto));
        }
    }
}
