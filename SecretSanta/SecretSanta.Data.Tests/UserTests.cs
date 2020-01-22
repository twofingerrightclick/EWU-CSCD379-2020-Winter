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

           

            //var gift = new Gift("Title", "Description", "Url", user);

           /* Gift sampleGift = new Gift
            {
                Title = title,
                Description = description,
                Url = url
            };*/


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


      /*  [TestMethod]

        public async Task Save_User_Retrieves_Users_Correct_GroupInfo()
        {
            //arrange
            var fixture = new Fixture();

            var user1 = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Santa = new User(),
                Gifts = new List<Gift>(),
                UserGroups = new List<UserGroup>()

            };

            UserGroup =



            var title = fixture.Create<string>();
            var descrip = fixture.Create<string>();
            var url = fixture.Create<string>();

            Gift sampleGift = new Gift
            {
                Title = title,
                Description = descrip,
                Url = url

            };






            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, _HttpContextAccessor))
            {

                dbContext.Users.Add(user1);
                await dbContext.SaveChangesAsync().ConfigureAwait(true);

            }

            User sampleUser;

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, _HttpContextAccessor))
            {

                sampleUser = await dbContext.Users.Where(u => u.LastName == "LastName").SingleOrDefaultAsync().ConfigureAwait(true);

            }


            Trace.WriteLine($"sampleUser { sampleUser.FirstName} ");

            //this is a really bulky all at once test

            IEnumerable<PropertyInfo> userProperties = sampleUser.GetType().GetProperties();
            FingerPrintEntityBase t = new FingerPrintEntityBase();
            IEnumerable<string> fingerprintProperties = t.GetType().GetProperties().Select(PropertyInfo => PropertyInfo.Name);

            //Act
            bool UserPropertiesFilledIncorrectly = userProperties
            .Select(propertyInfo => { return (value: propertyInfo.GetValue(sampleUser)!, propertyInfo); })
            .Any((valueAndProperty) =>
            {



                if (valueAndProperty.propertyInfo.PropertyType == typeof(string) && !fingerprintProperties.Contains(valueAndProperty.propertyInfo.Name))
                {
                    if (valueAndProperty.propertyInfo.Name != (string)valueAndProperty.value)
                    {
                        Trace.WriteLine($"User { valueAndProperty.propertyInfo} was incorrectly assigned: {valueAndProperty.value}");
                        return true;
                    }
                }
                return false;
            });
            //assert
            Assert.IsFalse(UserPropertiesFilledIncorrectly);

        }*/



    }


}