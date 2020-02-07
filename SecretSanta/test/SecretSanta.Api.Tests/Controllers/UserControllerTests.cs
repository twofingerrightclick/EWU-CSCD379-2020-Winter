using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests: ControllerBase
    {
        [TestMethod]
        public void Create_UserController_Success()
        {
            //Arrange
           // var service = new UserService();
            var controller = new Mock<UserController>();
           // service.Setup(x => x.P);


            //Act
         

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
            //whats the difference between why have to use IUserService? versus UserService is it that the Mock object can inherent and thus override the non virtual methods?
            // Arrange

            var user = SampleData.CreateUser2();

            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.FetchByIdAsync(42))
                .ReturnsAsync(user);

            var controller = new UserController(mockService.Object);

            //act
            ActionResult<User> getResult = await controller.Get(42);
            OkObjectResult? okCode = getResult.Result as OkObjectResult;
            User? resultValue = okCode!.Value as User;

            // Assert
           Assert.IsTrue(getResult.Result is OkObjectResult);
            Assert.IsNotNull(resultValue);
           
            

           
        }


        [TestMethod]
        public async Task GetById_NoUserExists_ReturnsNull()
        {
            //whats the difference between why have to use IUserService? versus UserService
            // Arrange
            User nullUser = null!;
            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.FetchByIdAsync(42))
                .ReturnsAsync(nullUser);

            var controller = new UserController(mockService.Object);

            //act
            var getResult = await controller.Get(42);

            // Assert
            Assert.IsNull(getResult.Value);



        }

        [TestMethod]
        public async Task PutById_WithNewUser_Success()
        {

            // Arrange

            var mockService = new Mock<IUserService>();

            User user = SampleData.CreateUser1();

            mockService.Setup(s => s.UpdateAsync(42, user))
                .ReturnsAsync(user) ;

            var controller = new UserController(mockService.Object);

            //act
            ActionResult<User> putResult = await controller.Put(42,user);


            // Assert
   
            Assert.IsTrue(putResult.Result is OkResult);


        }

        [TestMethod]
        public async Task PutById_UserNotFoud_ReturnsNull()
        {

            // Arrange

            var mockService = new Mock<IUserService>();

            User user = SampleData.CreateUser1();

            User nullUser = null!;

            mockService.Setup(s => s.UpdateAsync(42, nullUser))
                .ReturnsAsync(nullUser);

            var controller = new UserController(mockService.Object);


            //act
            var putResult = await controller.Put(42, user);

            // Assert
           

            Assert.IsTrue(putResult.Result is NotFoundResult);




        }

        [TestMethod]
        public async Task Delete_EntityWithId_Success()
        {
            //Arrange
            var mockService = new Mock<IUserService>();

           
            mockService.Setup(s => s.DeleteAsync(42)).ReturnsAsync(true);
                

            var controller = new UserController(mockService.Object);

            //Act
            ActionResult deleteResult = await controller.Delete(42);

            //Assert
            Assert.IsTrue(deleteResult is OkResult);
        }

        [TestMethod]
        public async Task Delete_UserWithWrongId_Fails()
        {
            //Arrange
            var mockService = new Mock<IUserService>();


            mockService.Setup(s => s.DeleteAsync(42)).ReturnsAsync(false);


            var controller = new UserController(mockService.Object);

            //Act
            ActionResult deleteResult = await controller.Delete(42);

            //Assert
            Assert.IsTrue(deleteResult is NotFoundResult);
        }

        /*  [HttpGet]
        public async Task<IEnumerable<TEntity>> Get()
        {
            List<TEntity> entities = await EntityService.FetchAllAsync();
            return entities;
        }
*/
        [TestMethod]
        public async Task Get_WithUsers_ReturnsAllUsers()
        {
            //whats the difference between why have to use IUserService? versus UserService
            // Arrange
            List<User> users = new List<User>();
            users.Add(SampleData.CreateUser1());
            users.Add(SampleData.CreateUser2());

            int expectedSize = users.Count;

            var mockService = new Mock<IUserService>();
            mockService.Setup(gs => gs.FetchAllAsync())
                .ReturnsAsync(users);

            var controller = new UserController(mockService.Object);

            //act
            var getResult = await controller.Get();


            // Assert
            Assert.IsTrue(getResult is IEnumerable<User>);

            List<User> getResultList = (List<User>)getResult;

            Assert.IsTrue(getResultList.Count == expectedSize);



        }

        [TestMethod]
        public async Task Get_NoUsers_Returns_EmptyList()
        {
            //whats the difference between why have to use IUserService? versus UserService
            // Arrange
            List<User> users = new List<User>();

            int expectedSize = users.Count;

            var mockService = new Mock<IUserService>();
            mockService.Setup(gs => gs.FetchAllAsync())
                .ReturnsAsync(users);

            var controller = new UserController(mockService.Object);

            //act
            var getResult = await controller.Get();


            // Assert
            Assert.IsTrue(getResult is IEnumerable<User>);


            List<User> getResultList = (List<User>)getResult;

            Assert.IsTrue(getResultList.Count == expectedSize);



        }





        /* // POST: api/Entity
         [HttpPost]
         [ProducesResponseType(StatusCodes.Status200OK)]
         
         public async Task<TEntity> Post(TEntity value)
         {
             return await EntityService.InsertAsync(value);
         }*/

        [TestMethod]
        public async Task Post_User_Success()
        {
            //Arrange
            var mockService = new Mock<IUserService>();

            var user = SampleData.CreateUser1();


            mockService.Setup(s => s.InsertAsync(user)).ReturnsAsync(user);


            var controller = new UserController(mockService.Object);

            //Act
            ActionResult<User> resultEntity = await controller.Post(user);

            //Assert
            Assert.IsTrue(resultEntity.Result is OkObjectResult);
            //not sure about what dbcontext.add(TEntity) returns if there is a failure
            OkObjectResult? okCode = resultEntity.Result as OkObjectResult;
            User? resultValue = okCode?.Value as User;
            Assert.IsNotNull(resultValue);
        }

    }

  
}