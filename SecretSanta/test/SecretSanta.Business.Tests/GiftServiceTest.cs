using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static SampleData;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTest : TestBase
    {
        // MethodBeingTested_ConditionBeingTested_WhatWeExpectedToHappen


        [TestMethod]
        public async Task InsertAsyncGift_ExpectingGiftIdChanged_UserNotNull()
        {
            //arrange


            using (var dbContext = new ApplicationDbContext(Options))
            {
                //setup
                var mapper = AutomapperConfigurationProfile.CreateMapper();

                GiftService giftService = new GiftService(dbContext, mapper);

                var sampleGift = CreateGift();
                int originalId = sampleGift.Id;

                //act
                var insertResult = await giftService.InsertAsync(sampleGift);

                //assert
                Assert.IsTrue(insertResult.Id != originalId);
                Assert.IsNotNull(insertResult.User);


            }
        }

        [TestMethod]
        public async Task FetchByIdAsync_ExpectingGiftIdChanged_UserNotNull()
        {
            //arrange


            using (var dbContext = new ApplicationDbContext(Options))
            {
                //setup
                var mapper = AutomapperConfigurationProfile.CreateMapper();

                GiftService giftService = new GiftService(dbContext, mapper);

                var sampleGift = CreateGift();
                int originalId = sampleGift.Id;

                var insertResult = await giftService.InsertAsync(sampleGift);
               


                //act
                Gift fetchResult = await giftService.FetchByIdAsync(1);

                //assert
                Assert.IsTrue(fetchResult.Id != originalId);
                Assert.IsNotNull(fetchResult.User);


            }
        }


        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async Task FetchByIdAsync_InvalidID_ThrowsException()
        {
            //arrange


            using (var dbContext = new ApplicationDbContext(Options))
            {
                //setup
                var mapper = AutomapperConfigurationProfile.CreateMapper();

                GiftService giftService = new GiftService(dbContext, mapper);

                //act
                Gift fetchResult = await giftService.FetchByIdAsync(1);

                //assert
                //Assert.IsNull(fetchResult);
                


            }
        }

        [TestMethod]
        public async Task DeleteGiftFromGiftService_ByAsyncId_ThenDeleteAgainExpectedFalse()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);

            var mapper = AutomapperConfigurationProfile.CreateMapper();
            var giftService = new GiftService(dbContext, mapper);
            await giftService.InsertAsync(CreateGift());



            //act & assert
            Assert.IsTrue(await giftService.DeleteAsync(1));
            Assert.IsFalse(await giftService.DeleteAsync(1));
        }

        //no more insert array in EntityService
       /* [TestMethod]
        public async Task InsertGiftListIntoGiftService_InsertArrayOfGifts_ExpectingSameCountAndNotNull()
        {
            //arrange

            using (var dbContext = new ApplicationDbContext(Options))
            {
                //setup
                var mapper = AutomapperConfigurationProfile.CreateMapper();

                GiftService giftService = new GiftService(dbContext, mapper);

                List<Gift> gifts = new List<Gift>();

                int numberOfGifts = 3;

                for (int i = 0; i < numberOfGifts; i++)
                {
                    gifts.Add(CreateGift());

                }

                Gift[] result = await giftService.InsertAsync(gifts.ToArray());



                //assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length == numberOfGifts);

                foreach (var gift in result)
                {
                    Assert.IsNotNull(gift.User);
                }



            }
        }*/

        // no more insert array service
      /*  [TestMethod]
        public async Task FetchAllAsync_AfterInsertingGifts_ExpectingSameCountAndNotNullAndUserNotNull()
        {
            //arrange

            using (var dbContext = new ApplicationDbContext(Options))
            {
                //setup
                var mapper = AutomapperConfigurationProfile.CreateMapper();

                GiftService giftService = new GiftService(dbContext, mapper);

                List<Gift> gifts = new List<Gift>();

                int numberOfGifts = 3;

                for (int i = 0; i < numberOfGifts; i++)
                {
                    gifts.Add(CreateGift());

                }

                await giftService.InsertAsync(gifts.ToArray());

                List<Gift> result = await giftService.FetchAllAsync();

                //assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count == numberOfGifts);

                foreach (var gift in result)
                {
                    Assert.IsNotNull(gift.User);
                    //the toListAsync method includes the User! 
                }



            }
        }*/

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async Task UpdateGiftProperty_WithInvalidId_ThrowsException()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Gift, Gift>();
            }).CreateMapper();
            var giftService = new GiftService(dbContext, mapper);
            var sampleGift1 = CreateGift();
            var sampleGift2 = CreateGift();
            await giftService.InsertAsync(sampleGift1);
            await giftService.InsertAsync(sampleGift2);



            //act
            sampleGift2 = await giftService.FetchByIdAsync(2);
            sampleGift2.Title = "updated_title";
            var gift = await giftService.UpdateAsync(1, sampleGift2);

            string name = gift.User.FirstName;
            Trace.WriteLine(name);
            //assert
            // (check method attribute)
        }


        [TestMethod]

        public async Task GiftService_Created_With_Mapper_That_Doesnt_Map_Id_No_EF_Exception_On_UpdateAsync()
        {

            using (var dbContext = new ApplicationDbContext(Options))
            {

                //setup
                var mapper = AutomapperConfigurationProfile.CreateMapper();

                GiftService giftService = new GiftService(dbContext, mapper);


                var sampleGift = CreateGift();

                var sampleGift2 = CreateGift();

                await giftService.InsertAsync(sampleGift);

                await giftService.InsertAsync(sampleGift2);


                //act
                sampleGift2 = await giftService.FetchByIdAsync(2);

                sampleGift2.Title = "New Title";
                //assert

                await giftService.UpdateAsync(1, sampleGift2);



            }




        }



    }
}
