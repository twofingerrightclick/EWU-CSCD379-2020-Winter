using BlogEngine.Data;
using BlogEngine.Data.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Business.Tests
{
    [TestClass]
    public class AuthorServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_InigoAndPrincess_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IAuthorService service = new AuthorService(dbContextInsert, Mapper);

            var inigo = new Dto.AuthorInput
            {
                FirstName = SampleData.Inigo,
                LastName = SampleData.Montoya,
                Email = SampleData.InigoMontoyaEmail
            };

            // Act
            var createdAuthor = await service.InsertAsync(inigo);

            // Assert
            Assert.IsNotNull(createdAuthor.Id);
        }

        [TestMethod]
        public async Task UpdateAuthor_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IAuthorService service = new AuthorService(dbContextInsert, Mapper);

            var inigo = new Dto.AuthorInput
            {
                FirstName = SampleData.Inigo,
                LastName = SampleData.Montoya,
                Email = SampleData.InigoMontoyaEmail
            };
            var princess = new Dto.AuthorInput
            {
                FirstName = SampleData.Princess,
                LastName = SampleData.Buttercup,
                Email = SampleData.PrincessButtercupEmail
            };

            var createdInigo = await service.InsertAsync(Mapper.Map<Dto.AuthorInput>(inigo));
            var createdPrincess = await service.InsertAsync(Mapper.Map<Dto.AuthorInput>(princess));

            // Act
            using var dbContextFetch = new ApplicationDbContext(Options);
            Author inigoFromDb = await dbContextFetch.Authors.SingleAsync(item => item.Id == createdInigo.Id);

            const string montoyaThe3rd = "Montoya The 3rd";
            inigoFromDb.LastName = montoyaThe3rd;

            // Update Inigo Montoya using the princesses Id.
            await service.UpdateAsync(createdPrincess.Id, new Dto.AuthorInput { FirstName = inigoFromDb.FirstName, LastName = inigoFromDb.LastName, Email = inigoFromDb.Email });

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            inigoFromDb = await dbContextAssert.Authors.SingleAsync(item => item.Id == createdInigo.Id);
            var princessFromDb = await dbContextAssert.Authors.SingleAsync(item => item.Id == 2); 

            Assert.AreEqual(
                (SampleData.Inigo, montoyaThe3rd), (princessFromDb.FirstName, princessFromDb.LastName));

            Assert.AreEqual(
                (SampleData.Inigo, SampleData.Montoya), (inigoFromDb.FirstName, inigoFromDb.LastName));
        }
    }
}
