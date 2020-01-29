using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTest : TestBase
    {
        // MethodBeingTested_ConditionBeingTested_WhatWeExpectedToHappen
        [TestMethod]
        public async Task InsertGiftIntoGiftService_ByCheckingAsyncGiftId_ExpectingGiftIdNotNull()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;
            var giftService = new GiftService(dbContext, mapper);
            var sampleGift = SampleData.CreateGift();
            await giftService.InsertAsync(sampleGift);

            //act
            Gift fetchedGift = await giftService.FetchByIdAsync(1);

            //assert
            Assert.IsNotNull(fetchedGift.User.LastName);
        }

        [TestMethod]
        public async Task DeleteGiftFromGiftService_ByAsyncId_ThenDeleteAgainExpectedFalse()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            //var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;
            var mapper = AutoMapperProfileConfiguration.CreateMapper();
            var giftService = new GiftService(dbContext, mapper);
            await giftService.InsertAsync(SampleData.CreateGift());

            //act & assert
            Assert.IsTrue(await giftService.DeleteAsync(1));
            Assert.IsFalse(await giftService.DeleteAsync(1));
        }

        [TestMethod]
        public async Task InsertGiftListIntoGiftService_ByFetchingAllAsync_ExpectingSameCountAndNotNull()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;
            var giftService = new GiftService(dbContext, mapper);
            var gifts = new List<Gift>() { SampleData.CreateGift(), SampleData.CreateGift(), SampleData.CreateGift() };
            await giftService.InsertAsync(gifts.ToArray());

            //act
            var fetchedGifts = await giftService.FetchAllAsync();

            //assert
            Assert.IsTrue(fetchedGifts.Count == gifts.Count);
            Assert.IsNotNull(gifts.Select(g => g.User.LastName));
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async Task UpdateGiftProperty_WithInvalidId_ThrowsException()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<Gift, Gift>();
            }).CreateMapper();
            var giftService = new GiftService(dbContext, mapper);
            var sampleGift1 = SampleData.CreateGift();
            var sampleGift2 = SampleData.CreateGift();
            await giftService.InsertAsync(sampleGift1);
            await giftService.InsertAsync(sampleGift2);

            //act
            sampleGift2 = await giftService.FetchByIdAsync(2);
            sampleGift2.Title = "updated_title";
            await giftService.UpdateAsync(1, sampleGift2);

            //assert
                // (check method attribute)
        }
    }
}
