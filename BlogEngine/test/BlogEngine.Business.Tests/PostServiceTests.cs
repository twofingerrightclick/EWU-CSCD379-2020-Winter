using BlogEngine.Data;
using BlogEngine.Data.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Business.Tests
{
    [TestClass]
    public class PostServiceTests : TestBase
    {
        [TestMethod]
        public async Task CreatePost_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IPostService service = new PostService(dbContext, Mapper);
            IAuthorService authorService = new AuthorService(dbContext, Mapper);

            var author = new Dto.AuthorInput
            {
                FirstName = "Inigo",
                LastName = "Montoya",
                Email = "inigo@montoya.me"
            };

            var createdAuthor = await authorService.InsertAsync(author);

            var post = new Dto.PostInput()
            {
                Title = "Title",
                AuthorId = createdAuthor.Id,
                Content = "Content"
            };

            var createdPost = await service.InsertAsync(post);

            // Act

            // Assert
            Assert.IsNotNull(createdPost.Id);
            Assert.IsNotNull(createdAuthor.Id);
            Assert.AreEqual(createdAuthor.Id, createdPost.Author.Id);
        }

        [TestMethod]
        public async Task FetchByIdPost_ShouldIncludeAuthor()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IPostService service = new PostService(dbContext, Mapper);
            IAuthorService authorService = new AuthorService(dbContext, Mapper);

            var author = new Dto.AuthorInput
            {
                FirstName = "Inigo",
                LastName = "Montoya",
                Email = "inigo@montoya.me"
            };

            var createdAuthor = await authorService.InsertAsync(author);

            var post = new Dto.PostInput
            {
                Title = "Title",
                Content = "Content",
                AuthorId = createdAuthor.Id
            };

            var createdPost = await service.InsertAsync(post);

            // Act

            // Assert
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new PostService(dbContext, Mapper);
            var fetchedPost = await service.FetchByIdAsync(createdPost.Id);

            Assert.IsNotNull(fetchedPost.AuthorId);
        }

        [TestMethod]
        public async Task UpdateAuthor_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IAuthorService service = new AuthorService(dbContext, Mapper);
            IAuthorService authorService = new AuthorService(dbContext, Mapper);

            var author = new Dto.AuthorInput
            {
                FirstName = "Inigo",
                LastName = "Montoya",
                Email = "inigo@montoya.me"
            };

            var author2 = new Dto.AuthorInput
            {
                FirstName = "Inigo",
                LastName = "Montoya",
                Email = "inigo@montoya.me"
            };

            var insertedAuthor = await service.InsertAsync(author);
            var insertedAuthor2 = await service.InsertAsync(author2);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            Author fetchAuthor = await dbContext2.Authors.SingleAsync(item => item.Id == insertedAuthor.Id);

            using var dbContext3 = new ApplicationDbContext(Options);
            var service2 = new AuthorService(dbContext3, Mapper);
            await service2.UpdateAsync(2, new Dto.AuthorInput
            {
                FirstName = "Princess",
                LastName = "Buttercup",
                Email = fetchAuthor.Email
            });

            // Assert
            using var dbContext4 = new ApplicationDbContext(Options);
            Author savedAuthor = await dbContext4.Authors.SingleAsync(item => item.Id == insertedAuthor.Id);
            var otherAuthor = await dbContext4.Authors.SingleAsync(item => item.Id == 2);
            Assert.AreEqual(("Inigo", "Montoya"), (savedAuthor.FirstName, savedAuthor.LastName));
            Assert.AreNotEqual((savedAuthor.FirstName, savedAuthor.LastName), (otherAuthor.FirstName, otherAuthor.LastName));

        }
    }
}
