using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SecretSanta.Data;
using SecretSanta.Data.Tests;


namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTest : TestBase
    {
        // MethodBeingTested_ConditionBeingTested_WhatWeExpectedToHappen
        [TestMethod]
        public async Task CreateNewGroupInDbContext_ShouldBeSavedToDatabase_ExpectingUpdatedDbData()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            var mapper = AutoMapperProfileConfiguration.CreateMapper();
            var groupService = new GroupService(dbContext, mapper);
            var sampleGroup1 = SampleData.CreateGroup1();
            var sampleGroup2 = SampleData.CreateGroup2();
            await groupService.InsertAsync(sampleGroup1);
            await groupService.InsertAsync(sampleGroup2);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            //act
            using var dbContextFetch = new ApplicationDbContext(Options);
            var fetchedGroup1 = await dbContextFetch.Groups.SingleAsync(item => item.Id == sampleGroup1.Id);
            const string newTitle = "updated_title";
            fetchedGroup1.Title = newTitle;
            await groupService.UpdateAsync(sampleGroup2.Id!, fetchedGroup1);

            //assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            fetchedGroup1 = await dbContextAssert.Groups.SingleAsync(item => item.Id == fetchedGroup1.Id);
            var fetchedGroup2 = await dbContextAssert.Groups.SingleAsync(item => item.Id == 2);
            Assert.AreEqual(newTitle, fetchedGroup2.Title);
            Assert.AreEqual(sampleGroup1.Title, fetchedGroup1.Title);
        }
    }
}
