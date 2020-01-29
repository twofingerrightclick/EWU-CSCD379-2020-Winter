using BlogEngine.Api.Controllers;
using BlogEngine.Business;
using BlogEngine.Data;
using BlogEngine.Data.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Api.Tests.Controllers
{
    [TestClass]
    public class AuthorControllerTests
    {
        [TestMethod]
        public void Create_AuthorController_Success()
        {
            //Arrange
            var service = new AuthorService();

            //Act
            _ = new AuthorController(service);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            //Arrange
            
            //Act
            _ = new AuthorController(null!);

            //Assert
        }

        [TestMethod]
        public async Task GetById_WithExistingAuthor_Success()
        {
            // Arrange
            var service = new AuthorService();
            Author author = SampleData.CreateInigoMontoya();
            author = await service.InsertAsync(author);

            var controller = new AuthorController(service);

            // Act
            ActionResult<Author> rv = await controller.Get(author.Id!.Value);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

    }

    public class AuthorService : IAuthorService
    {
        private Dictionary<int, Author> Items { get; } = new Dictionary<int, Author>();

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Author>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Author?> FetchByIdAsync(int id)
        {
            if(Items.TryGetValue(id, out Author? author))
            {
                Task<Author?> t1 = Task.FromResult<Author?>(author);
                return t1;
            }
            Task<Author?> t2 =  Task.FromResult<Author?>(null);
            return t2;
        }

        public Task<Author> InsertAsync(Author entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestAuthor(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<Author[]> InsertAsync(params Author[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Author?> UpdateAsync(int id, Author entity)
        {
            throw new NotImplementedException();
        }
    }

    public class  TestAuthor : Author
    {
        public TestAuthor(Author author, int id)
            : base(author.FirstName, author.LastName, author.Email)
        {
            Id = id;
        }
    }
}
