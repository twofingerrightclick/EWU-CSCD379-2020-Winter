using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using SecretSanta.Business.Services;


namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GroupTests : TestBase
    {
        //[TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Group_SetNameToNull_ThrowsArgumentNullException()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            dbContext.Groups.Add(new Group(null!));

            //act

            //assert

        }

        //[TestMethod]
        public async Task Group_CanBeSavedToDatabase()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            dbContext.Groups.Add(new Group("title"));
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            //act

            //assert

        }
    }
}
