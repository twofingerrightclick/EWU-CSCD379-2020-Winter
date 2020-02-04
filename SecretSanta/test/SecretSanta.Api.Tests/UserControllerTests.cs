using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace BlogEngine.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Create_UserController_Success()
        {
            //Arrange
            //var service = new UserService();
           // var service = new Mock(EntityController<User>);

            //Act
           // _ = new UserController(service);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            //Arrange

            //Act
            _ = new UserController(null!);

            //Assert
        }

        [TestMethod]
        public async Task GetById_WithExistingUser_Success()
        {
            // Arrange
           // var service = new UserService();
            User user = SampleData.CreateUser1();
           // user = await service.InsertAsync(user);

           // var controller = new UserController(service);

            // Act
           // ActionResult<User> rv = await controller.Get(user.Id!);

            // Assert
          //  Assert.IsTrue(rv.Result is OkObjectResult);
        }

    }

   /* public class UserService : IUserService
    {
        private Dictionary<int, User> Items { get; } = new Dictionary<int, User>();

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out User? user))
            {
                Task<User?> t1 = Task.FromResult<User?>(user);
                return t1;
            }
            Task<User?> t2 = Task.FromResult<User?>(null);
            return t2;
        }

        public Task<User> InsertAsync(User entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestUser(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<User[]> InsertAsync(params User[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateAsync(int id, User entity)
        {
            throw new NotImplementedException();
        }

        private class TestUser : User
        {
            public TestUser(User user, int id)
                : base((user ?? throw new ArgumentNullException(nameof(user))).FirstName,
                      user.LastName, user.Email)
            {
                Id = id;
            }
        }
    }*/
}