using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;


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
            dbContext.Groups.Add(new Group());

            //act

            //assert

        }

        //[TestMethod]
        public async Task Group_CanBeSavedToDatabase()
        {
            //arrange
            using var dbContext = new ApplicationDbContext(Options);
            dbContext.Groups.Add(new Group());
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            //act

            //assert

        }
    }
}
