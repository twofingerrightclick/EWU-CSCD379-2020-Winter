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
    public class GroupControllerTests : ControllerBase
    {
        [TestMethod]
        public void Create_GroupController_Success()
        {
            //Arrange
            // var service = new GroupService();
            var controller = new Mock<GroupController>();
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
            _ = new GroupController(null!);


            //Assert
        }

        [TestMethod]
        public async Task GetById_WithExistingGroup_Success()
        {
            //whats the difference between why have to use IGroupService? versus GroupService is it that the Mock object can inherent and thus override the non virtual methods?
            // Arrange

            var group = SampleData.CreateGroup1();

            var mockService = new Mock<IGroupService>();
            mockService.Setup(s => s.FetchByIdAsync(42))
                .ReturnsAsync(group);

            var controller = new GroupController(mockService.Object);

            //act
            ActionResult<Group> getResult = await controller.Get(42);
            OkObjectResult? okCode = getResult.Result as OkObjectResult;
            Group? resultValue = okCode!.Value as Group;

            // Assert
            Assert.IsTrue(getResult.Result is OkObjectResult);
            Assert.IsNotNull(resultValue);
        }


        [TestMethod]
        public async Task GetById_NoGroupExists_ReturnsNull()
        {
            //whats the difference between why have to use IGroupService? versus GroupService
            // Arrange
            Group nullGroup = null!;
            var mockService = new Mock<IGroupService>();
            mockService.Setup(x => x.FetchByIdAsync(42))
                .ReturnsAsync(nullGroup);

            var controller = new GroupController(mockService.Object);

            //act
            var getResult = await controller.Get(42);

            // Assert
            Assert.IsNull(getResult.Value);



        }

        [TestMethod]
        public async Task PutById_WithNewGroup_Success()
        {

            // Arrange

            var mockService = new Mock<IGroupService>();

            Group group = SampleData.CreateGroup1();

            mockService.Setup(s => s.UpdateAsync(42, group))
                .ReturnsAsync(group);

            var controller = new GroupController(mockService.Object);

            //act
            ActionResult<Group> putResult = await controller.Put(42, group);


            // Assert
            Assert.IsTrue(putResult.Result is OkResult);
        }

        [TestMethod]
        public async Task PutById_GroupNotFoud_ReturnsNull()
        {

            // Arrange

            var mockService = new Mock<IGroupService>();

            Group group = SampleData.CreateGroup1();

            Group nullGroup = null!;

            mockService.Setup(s => s.UpdateAsync(42, nullGroup))
                .ReturnsAsync(nullGroup);

            var controller = new GroupController(mockService.Object);


            //act
            var putResult = await controller.Put(42, group);

            // Assert
            Assert.IsTrue(putResult.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Delete_EntityWithId_Success()
        {
            //Arrange
            var mockService = new Mock<IGroupService>();


            mockService.Setup(s => s.DeleteAsync(42)).ReturnsAsync(true);


            var controller = new GroupController(mockService.Object);

            //Act
            ActionResult deleteResult = await controller.Delete(42);

            //Assert
            Assert.IsTrue(deleteResult is OkResult);
        }

        [TestMethod]
        public async Task Delete_GroupWithWrongId_Fails()
        {
            //Arrange
            var mockService = new Mock<IGroupService>();


            mockService.Setup(s => s.DeleteAsync(42)).ReturnsAsync(false);


            var controller = new GroupController(mockService.Object);

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
        public async Task Get_WithGroups_ReturnsAllGroups()
        {
            //whats the difference between why have to use IGroupService? versus GroupService
            // Arrange
            List<Group> groups = new List<Group>();
            groups.Add(SampleData.CreateGroup2());
            groups.Add(SampleData.CreateGroup1());

            int expectedSize = groups.Count;

            var mockService = new Mock<IGroupService>();
            mockService.Setup(gs => gs.FetchAllAsync())
                .ReturnsAsync(groups);

            var controller = new GroupController(mockService.Object);

            //act
            var getResult = await controller.Get();


            // Assert
            Assert.IsTrue(getResult is IEnumerable<Group>);

            List<Group> getResultList = (List<Group>)getResult;

            Assert.IsTrue(getResultList.Count == expectedSize);



        }

        [TestMethod]
        public async Task Get_NoGroups_Returns_EmptyList()
        {
            //whats the difference between why have to use IGroupService? versus GroupService
            // Arrange
            List<Group> groups = new List<Group>();

            int expectedSize = groups.Count;

            var mockService = new Mock<IGroupService>();
            mockService.Setup(gs => gs.FetchAllAsync())
                .ReturnsAsync(groups);

            var controller = new GroupController(mockService.Object);

            //act
            var getResult = await controller.Get();


            // Assert
            Assert.IsTrue(getResult is IEnumerable<Group>);


            List<Group> getResultList = (List<Group>)getResult;

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
        public async Task Post_Group_Success()
        {
            //Arrange
            var mockService = new Mock<IGroupService>();

            var group = SampleData.CreateGroup1();


            mockService.Setup(s => s.InsertAsync(group)).ReturnsAsync(group);


            var controller = new GroupController(mockService.Object);

            //Act
            ActionResult<Group> resultEntity = await controller.Post(group);

            //Assert
            Assert.IsTrue(resultEntity.Result is OkObjectResult);
            //not sure about what dbcontext.add(TEntity) returns if there is a failure
            OkObjectResult? okCode = resultEntity.Result as OkObjectResult;
            Group? resultValue = okCode?.Value as Group;
            Assert.IsNotNull(resultValue);
        }

    }


}