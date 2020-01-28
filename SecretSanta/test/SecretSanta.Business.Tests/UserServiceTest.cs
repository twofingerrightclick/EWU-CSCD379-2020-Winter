using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTest : TestBase
    {
        readonly string _Firstname = "Ring Doorbell";
        readonly string _Lastname = "www.ring.com";



        // MethodBeingTested_ConditionBeingTested_WhatWeExpectedToHappen
        [TestMethod]

        public async Task InsertUserIntoUserService_ByCheckingAsyncUserId_ExpectedUserIdNotNull()
        {
            //setup
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;
            var userService = new UserService(dbContext, mapper);
            var sampleUser = new User(_Firstname, _Lastname);
            await userService.InsertAsync(sampleUser);

            //act
            User sampleUser2 = await userService.FetchByIdAsync(1);

            //assert
            Assert.IsNotNull(sampleUser2.Id);
        }


        [TestMethod]

        public async Task UpdateUserProperty_ShouldSaveIntoDatabase_ExpectedUserPropertyInDbEqualsTestValue()
        {
            //setup
            using var dbContextInsert = new ApplicationDbContext(Options);
            var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;
            var userService = new UserService(dbContextInsert, mapper);
            var sampleUser1 = new User("original_a_fname", "original_a_lname");
            var sampleUser2 = new User("original_b_fname", "original_b_lname");
            await userService.InsertAsync(sampleUser1);
            await userService.InsertAsync(sampleUser2);


            //act
            using var dbContextFetch = new ApplicationDbContext(Options);
            var fetchedUser1 = await dbContextFetch.Users.SingleAsync(item => item.Id == sampleUser1.Id);
            const string newLastname = "updated_a_lname";
            fetchedUser1.LastName = newLastname;
            await userService.UpdateAsync(sampleUser2.Id!, fetchedUser1);

            //assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            fetchedUser1 = await dbContextAssert.Users.SingleAsync(item => item.Id == fetchedUser1.Id);
            var fetchedUser2 = await dbContextAssert.Users.SingleAsync(item => item.Id == 2);
            Assert.AreEqual((sampleUser1.FirstName, newLastname), (fetchedUser2.FirstName, fetchedUser2.LastName));
            Assert.AreEqual((sampleUser1.FirstName, sampleUser1.LastName), (fetchedUser1.FirstName, fetchedUser1.LastName));
        }



        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async Task GiftService_Created_With_Mapper_That_Maps_Id_Trows_EF_Exception_On_UpdateAsync()
        {

            using (var dbContext = new ApplicationDbContext(Options)) {


                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Gift, Gift>();

                });

                var mapper = mapperConfig.CreateMapper();


                GiftService giftService = new GiftService(dbContext, mapper);

                var sampleGift = new Gift(_Title, _Description, _Url, _SampleUser);

                var sampleGift2 = new Gift(_Title, _Description, _Url, _SampleUser);

                await giftService.InsertAsync(sampleGift);

                await giftService.InsertAsync(sampleGift2);


                sampleGift2 = await giftService.FetchByIdAsync(2);

                sampleGift2.Title = "New Title";

                await giftService.UpdateAsync(1, sampleGift2);



            }




        }


        [TestMethod]

        public async Task GiftService_Created_With_Mapper_That_Doesnt_Map_Id_No_EF_Exception_On_UpdateAsync()
        {

            using (var dbContext = new ApplicationDbContext(Options)) {


                var mapper = new IgnoreIDAutomapperConfigurationProfile<Gift>().Mapper;

                GiftService giftService = new GiftService(dbContext, mapper);


                var sampleGift = new Gift(_Title, _Description, _Url, _SampleUser);

                var sampleGift2 = new Gift(_Title, _Description, _Url, _SampleUser);

                await giftService.InsertAsync(sampleGift);

                await giftService.InsertAsync(sampleGift2);


                sampleGift2 = await giftService.FetchByIdAsync(2);

                sampleGift2.Title = "New Title";

                await giftService.UpdateAsync(1, sampleGift2);



            }




        }



    }
}
