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
    public class GiftControllerTests : ControllerBase
    {
        [TestMethod]
        public void Create_GiftController_Success()
        {
            //Arrange
            // var service = new GiftService();
            var controller = new Mock<GiftController>();
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
            _ = new GiftController(null!);


            //Assert
        }

        [TestMethod]
        public async Task GetById_WithExistingGift_Success()
        {
            //whats the difference between why have to use IGiftService? versus GiftService is it that the Mock object can inherent and thus override the non virtual methods?
            // Arrange

            var gift = SampleData.CreateGift();

            var mockService = new Mock<IGiftService>();
            mockService.Setup(s => s.FetchByIdAsync(42))
                .ReturnsAsync(gift);

            var controller = new GiftController(mockService.Object);

            //act
            ActionResult<Gift> getResult = await controller.Get(42);
            OkObjectResult? okCode = getResult.Result as OkObjectResult;
            Gift? resultValue = okCode?.Value as Gift;

            // Assert
            Assert.IsTrue(getResult.Result is OkObjectResult);
            Assert.IsNotNull(resultValue);
        }


        [TestMethod]
        public async Task GetById_NoGiftExists_ReturnsNull()
        {
            //whats the difference between why have to use IGiftService? versus GiftService
            // Arrange
            Gift nullGift = null!;
            var mockService = new Mock<IGiftService>();
            mockService.Setup(x => x.FetchByIdAsync(42))
                .ReturnsAsync(nullGift);

            var controller = new GiftController(mockService.Object);

            //act
            var getResult = await controller.Get(42);

            // Assert
            Assert.IsNull(getResult.Value);



        }

        [TestMethod]
        public async Task PutById_WithNewGift_Success()
        {

            // Arrange

            var mockService = new Mock<IGiftService>();

            Gift gift = SampleData.CreateGift();

            mockService.Setup(s => s.UpdateAsync(42, gift))
                .ReturnsAsync(gift);

            var controller = new GiftController(mockService.Object);

            //act
            ActionResult<Gift> putResult = await controller.Put(42, gift);


            // Assert
            Assert.IsTrue(putResult.Result is OkResult);
        }

        [TestMethod]
        public async Task PutById_GiftNotFoud_ReturnsNull()
        {

            // Arrange

            var mockService = new Mock<IGiftService>();

            Gift gift = SampleData.CreateGift();

            Gift nullGift = null!;

            mockService.Setup(s => s.UpdateAsync(42, nullGift))
                .ReturnsAsync(nullGift);

            var controller = new GiftController(mockService.Object);


            //act
            var putResult = await controller.Put(42, gift);

            // Assert
            Assert.IsTrue(putResult.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Delete_EntityWithId_Success()
        {
            //Arrange
            var mockService = new Mock<IGiftService>();


            mockService.Setup(s => s.DeleteAsync(42)).ReturnsAsync(true);


            var controller = new GiftController(mockService.Object);

            //Act
            ActionResult deleteResult = await controller.Delete(42);

            //Assert
            Assert.IsTrue(deleteResult is OkResult);
        }

        [TestMethod]
        public async Task Delete_GiftWithWrongId_Fails()
        {
            //Arrange
            var mockService = new Mock<IGiftService>();


            mockService.Setup(s => s.DeleteAsync(42)).ReturnsAsync(false);


            var controller = new GiftController(mockService.Object);

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
        public async Task Get_WithGifts_ReturnsAllGifts()
        {
            //whats the difference between why have to use IGiftService? versus GiftService
            // Arrange
           List<Gift> gifts = new List<Gift>();
            gifts.Add(SampleData.CreateGift());
            gifts.Add(SampleData.CreateGift());

           int expectedSize = gifts.Count;

            var mockService = new Mock<IGiftService>();
            mockService.Setup(gs => gs.FetchAllAsync())
                .ReturnsAsync(gifts);

            var controller = new GiftController(mockService.Object);

            //act
            var getResult = await controller.Get();


            // Assert
            Assert.IsTrue(getResult is IEnumerable<Gift>);
            
            List<Gift> getResultList = (List<Gift>)getResult; 
            
            Assert.IsTrue(getResultList.Count==expectedSize);



        }

        [TestMethod]
        public async Task Get_NoGifts_Returns_EmptyList()
        {
            //whats the difference between why have to use IGiftService? versus GiftService
            // Arrange
            List<Gift> gifts = new List<Gift>();

            int expectedSize = gifts.Count;

            var mockService = new Mock<IGiftService>();
            mockService.Setup(gs => gs.FetchAllAsync())
                .ReturnsAsync(gifts);

            var controller = new GiftController(mockService.Object);

            //act
            var getResult = await controller.Get();


            // Assert
            Assert.IsTrue(getResult is IEnumerable<Gift>);
            

            List<Gift> getResultList = (List<Gift>)getResult;
            
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
        public async Task Post_Gift_Success()
        {
            //Arrange
            var mockService = new Mock<IGiftService>();

            var gift = SampleData.CreateGift();


            mockService.Setup(s => s.InsertAsync(gift)).ReturnsAsync(gift);


            var controller = new GiftController(mockService.Object);

            //Act
            ActionResult<Gift> resultEntity = await controller.Post(gift);

            //Assert
            Assert.IsTrue(resultEntity.Result is OkObjectResult);
            //not sure about what dbcontext.add(TEntity) returns if there is a failure
            OkObjectResult? okCode = resultEntity.Result as OkObjectResult;
            Gift? resultValue = okCode?.Value as Gift;
            Assert.IsNotNull(resultValue);
        }

    }


}