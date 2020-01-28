using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SampleData;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTest : TestBase
    {


        [TestMethod]

        public async Task Fetch_Gift_Returns_Gift_And_Associated_User()
        {

            using (var dbContext = new ApplicationDbContext(Options))
            {
                //setup
                var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;

                GiftService giftService = new GiftService(dbContext, mapper);

                var sampleGift = CreateGift();

                await giftService.InsertAsync(sampleGift);

                //act
                Gift sampleGift2 = await giftService.FetchByIdAsync(1);

                //assert
                Assert.IsNotNull(sampleGift2.User.LastName);


            }
        }


        [TestMethod]

        public async Task Delete_Gift_By_Id()
        {

            using (var dbContext = new ApplicationDbContext(Options))
            {

                //setup
                //var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;
                
                var mapper = AutoMapperProfileConfiguration.CreateMapper();

                GiftService giftService = new GiftService(dbContext, mapper);

                await giftService.InsertAsync(CreateGift());

                //act
                Assert.IsTrue(await giftService.DeleteAsync(1));

                Assert.IsFalse(await giftService.DeleteAsync(1));


            }


            }

        [TestMethod]

        public async Task Insert_Array_Of_Gifts_Fetch_All_Gifts_Returns_All_Gifts_And_Associated_Users()
        {

            using (var dbContext = new ApplicationDbContext(Options))
            {
                //setup
                var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;

                GiftService giftService = new GiftService(dbContext, mapper);

                List<Gift> gifts = new List<Gift>();

                int numberOfGifts = 3;

                for (int i = 0; i < numberOfGifts; i++)
                {
                    gifts.Add(CreateGift());

                }

                await giftService.InsertAsync(gifts.ToArray());


                //act
                var fetchedGifts = await giftService.FetchAllAsync();

                //assert
                Assert.IsTrue(fetchedGifts.Count == numberOfGifts);

                foreach (var gift in fetchedGifts)
                {
                    Assert.IsNotNull(gift.User.LastName);
                }



            }
        }



        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async Task GiftService_Created_With_Mapper_That_Maps_Id_Trows_EF_Exception_On_UpdateAsync()
        {
            
            using (var dbContext = new ApplicationDbContext(Options))
            {

                //setup
                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Gift, Gift>();

                });

                var mapper = mapperConfig.CreateMapper();


                GiftService giftService = new GiftService(dbContext, mapper);

                var sampleGift = SampleData.CreateGift();

                var sampleGift2 = SampleData.CreateGift();

                //act
                await giftService.InsertAsync(sampleGift);

                await giftService.InsertAsync(sampleGift2);


                sampleGift2 = await giftService.FetchByIdAsync(2);

                sampleGift2.Title = "New Title";

                //assert
                await giftService.UpdateAsync(1, sampleGift2);



            }




        }


        [TestMethod]

        public async Task GiftService_Created_With_Mapper_That_Doesnt_Map_Id_No_EF_Exception_On_UpdateAsync()
        {

            using (var dbContext = new ApplicationDbContext(Options))
            {

                //setup
                var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;

                GiftService giftService = new GiftService(dbContext, mapper);


                var sampleGift = SampleData.CreateGift();

                var sampleGift2 = SampleData.CreateGift();

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
