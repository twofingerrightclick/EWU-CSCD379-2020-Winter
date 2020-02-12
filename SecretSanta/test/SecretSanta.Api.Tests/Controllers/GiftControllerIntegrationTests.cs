using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Dto;
using SecretSanta.Data;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecretSanta.Api.IntegrationTests
{
    [TestClass]
    public class GiftControllerIntegrationTests : BaseControllerIntegrationTests<Business.Dto.Gift, GiftInput>
    {
        protected override string BaseUrl { get; } = "api/Gift";

        [TestMethod]
        public async Task PostEndpoint_WithWithoutUserId_BadRequest()
        {
            // Arrange
            HttpClient client = Factory.CreateClient();
            var uri = new Uri(BaseUrl, UriKind.RelativeOrAbsolute);
            GiftInput inputDto = CreateInputDto();
            inputDto.UserId = null;
            using var content = new StringContent(JsonSerializer.Serialize(inputDto), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await client.PostAsync(uri, content);

            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task PostEndpoint_WithoutTitle_BadRequest()
        {
            // Arrange
            HttpClient client = Factory.CreateClient();
            var uri = new Uri(BaseUrl, UriKind.RelativeOrAbsolute);
            GiftInput inputDto = CreateInputDto();
            inputDto.Title = null;
            using var content = new StringContent(JsonSerializer.Serialize(inputDto), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await client.PostAsync(uri, content);

            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)response.StatusCode);
        }

        protected override GiftInput CreateInputDto()
        {
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.User user = context.Users.First();
            return new GiftInput
            {
                Description = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                Url = Guid.NewGuid().ToString(),
                UserId = user.Id
            };
        }

        protected override bool ExistsInDatabase(Business.Dto.Gift gift)
        {
            if (gift is null)
            {
                throw new ArgumentNullException(nameof(gift));
            }

            using ApplicationDbContext context = Factory.GetDbContext();
            return context.Find<Data.Gift>(gift.Id) != null;
        }
    }
}
