using BlogEngine.Data;
using BlogEngine.Data.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Business.Tests
{
    [TestClass]
    public class AuthoreServicesTests : TestBase
    {
        [TestMethod]
        public async Task InsertAuthor_InigoMontoya_Success()
        {
            // Arrange
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);

            AuthorService service = new AuthorService(dbContext);

            Author author = SampleData.CreateInigoMontoya();

            // Act
            Author insertedAuthor = await service.InsertAsync(author);

            // Assert
            using ApplicationDbContext dbContextAssert = new ApplicationDbContext(Options);

            Author dbAuthor = await dbContextAssert.Authors.FindAsync(insertedAuthor.Id);

            Assert.AreEqual<(int, string, string)>(
                (insertedAuthor.Id, author.FirstName, author.LastName),
                (dbAuthor.Id, dbAuthor.FirstName, dbAuthor.LastName));
        }
    }
}
