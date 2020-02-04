using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Create_UserController_Success()
        {
            //Arrange
           // var service = new UserService();
            var controller = new Mock<EntityController<User>>();
           // service.Setup(x => x.P);


            //Act
          // _ = new EntityController<IUserService>(service);

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

            var mockService = new Mock<UserService>();
            mockService.Setup(x => x.FetchByIdAsync(42))
                .ReturnsAsync(SampleData.CreateUser1());

            var controller = new UserController(mockService.Object);

            //act
            var getResult = await controller.Get(42);

            // Assert
            Assert.IsNotNull(getResult);
            

           
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