using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTest : TestBase
    {
        // MethodBeingTested_ConditionBeingTested_WhatWeExpectedToHappen
        [TestMethod]
        public async Task InsertUserIntoUserService_ByCheckingAsyncUserId_ExpectingUserIdNotNull()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = AutoMapperProfileConfiguration.CreateMapper();
            var userService = new UserService(dbContext, mapper);
            var sampleUser = SampleData.CreateUser1();
            await userService.InsertAsync(sampleUser);

            //act
            User sampleUser2 = await userService.FetchByIdAsync(1);

            //assert
            Assert.IsNotNull(sampleUser2.Id);
        }

        [TestMethod]
        public async Task DeleteUserFromUserService_ByAsyncId_ThenDeleteAgainExpectedFalse()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = AutoMapperProfileConfiguration.CreateMapper();
            var userService = new UserService(dbContext, mapper);
            await userService.InsertAsync(SampleData.CreateUser1());

            //act & assert
            Assert.IsTrue(await userService.DeleteAsync(1));
            Assert.IsFalse(await userService.DeleteAsync(1));
        }

        [TestMethod]
        public async Task UpdateUserProperty_ShouldSaveIntoDatabase_ExpectingFetchedValueEqualsTestValue()
        {
            //arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            var mapper = AutoMapperProfileConfiguration.CreateMapper();
            var userService = new UserService(dbContextInsert, mapper);
            var sampleUser1 = SampleData.CreateUser1();
            var sampleUser2 = SampleData.CreateUser2();
            await userService.InsertAsync(sampleUser1);
            await userService.InsertAsync(sampleUser2);


            //act
            using var dbContextFetch = new ApplicationDbContext(Options);
            var fetchedUser1 = await dbContextFetch.Users.SingleAsync(item => item.Id == sampleUser1.Id);
            const string newLastname = "updated_lname";
            fetchedUser1.LastName = newLastname;
            await userService.UpdateAsync(sampleUser2.Id!, fetchedUser1);

            //assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            fetchedUser1 = await dbContextAssert.Users.SingleAsync(item => item.Id == fetchedUser1.Id);
            var fetchedUser2 = await dbContextAssert.Users.SingleAsync(item => item.Id == 2);
            //Assert.AreEqual((sampleUser1.FirstName, newLastname), (fetchedUser1.FirstName, fetchedUser2.LastName));
            //Assert.AreEqual((sampleUser1.FirstName, sampleUser1.LastName), (fetchedUser1.FirstName, fetchedUser1.LastName));
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async Task UpdateUserProperty_WithInvalidId_ThrowsException()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, User>();
                }).CreateMapper();
            var userService = new UserService(dbContext, mapper);
            var sampleUser1 = SampleData.CreateUser1();
            var sampleUser2 = SampleData.CreateUser2();
            await userService.InsertAsync(sampleUser1);
            await userService.InsertAsync(sampleUser2);

            //act
            sampleUser2 = await userService.FetchByIdAsync(2);
            sampleUser2.LastName = "updated_lname";
            await userService.UpdateAsync(1, sampleUser2);

            //assert
                // (check method attribute)
        }
    }
}
