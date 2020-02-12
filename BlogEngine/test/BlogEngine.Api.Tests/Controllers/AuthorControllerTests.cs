using AutoMapper;
using BlogEngine.Api.Controllers;
using BlogEngine.Business;
using BlogEngine.Data;
using BlogEngine.Data.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogEngine.Api.Tests.Controllers
{
    [TestClass]
    public class AuthorControllerTests
    {
        private BlogWebApplicationFactory Factory { get; set; }
        private HttpClient Client { get; set; }
        private IMapper Mapper { get; } = AutomapperProfileConfiguration.CreateMapper();



        [TestInitialize]
        public void TestSetup()
        {
            Factory = new BlogWebApplicationFactory();

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
        public async Task Get_ReturnsAuthors()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Author im = SampleData.CreateInigoMontoya();
            context.Authors.Add(im);
            context.SaveChanges();

            // Act
            HttpResponseMessage response = await Client.GetAsync("api/Author");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Author[] authors =
                JsonSerializer.Deserialize<Business.Dto.Author[]>(jsonData, options);
            Assert.AreEqual(1, authors.Length);

            Assert.AreEqual(im.Id, authors[0].Id);
            Assert.AreEqual(im.FirstName, authors[0].FirstName);
            Assert.AreEqual(im.LastName, authors[0].LastName);
            Assert.AreEqual(im.Email, authors[0].Email);
        }

        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            // Arrange
            Business.Dto.AuthorInput im = Mapper.Map<Author, Business.Dto.Author>(SampleData.CreateInigoMontoya());
            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/Author/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithId_UpdatesEntity()
        {
            // Arrange
            var entity = SampleData.CreateInigoMontoya();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Authors.Add(entity);
            context.SaveChanges();

            Business.Dto.AuthorInput im = new Business.Dto.AuthorInput
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };

            im.FirstName += "changed";
            im.LastName += "changed";
            im.Email += "changed";

            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await Client.PutAsync($"api/Author/{entity.Id}", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retunedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Author returnedAuthor = JsonSerializer.Deserialize<Business.Dto.Author>(retunedJson, options);

            // Assert that returnedAuthor matches im values
            // Why?

            // Assert that returnedAuthor matches database value
            // Why?
        }

        [TestMethod]
        [DataRow(nameof(Business.Dto.AuthorInput.FirstName))]
        [DataRow(nameof(Business.Dto.AuthorInput.LastName))]
        [DataRow(nameof(Business.Dto.AuthorInput.Email))]
        public async Task Post_WithoutFirstName_BadResult(string propertyName)
        {
            // Arrange
            Data.Author entity = SampleData.CreateInigoMontoya();

            //DTO
            Business.Dto.AuthorInput im = Mapper.Map<Author, Business.Dto.Author>(entity);
            System.Type inputType = typeof(Business.Dto.AuthorInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(im, null);

            string jsonData = JsonSerializer.Serialize(im);
            
            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await Client.PostAsync($"api/Author", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
