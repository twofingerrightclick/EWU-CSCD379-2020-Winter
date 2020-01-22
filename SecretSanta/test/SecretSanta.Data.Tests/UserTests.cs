using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass()]
    public class UserTests : TestInitializer

    {

        [TestMethod]

        public async Task Save_User_Retrieves_User_All_Properties_Preserved()
        {
            //arrange
            var fixture = new Fixture();
            string firstName = "FirstName";
            string lastName = "LastName";

            var sampleUser = new User(firstName, lastName);
        
            var title = fixture.Create<string>();
            var description = fixture.Create<string>();
            var url = fixture.Create<string>();

            Gift sampleGift = new Gift(title, description, url, sampleUser);

            
            sampleUser.Gifts.Add(sampleGift);


            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, _HttpContextAccessor))
            {

                dbContext.Users.Add(sampleUser);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);

            }

            User user;

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, _HttpContextAccessor))
            {

                user = await dbContext.Users.Where(u => u.LastName == "LastName").Include(user=>user.Gifts).SingleOrDefaultAsync().ConfigureAwait(false);

            }

            Assert.AreEqual<string>(user.FirstName,firstName);
            Assert.AreEqual<string>(user.Gifts.ElementAt(0).User.LastName, lastName);


        }



    }


}