using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass()]
    public class UserTests:TestInitializer

    {

        [TestMethod]

        public async Task Save_Gift_Retrieves_Gift_All_Properties_The_Same_Including_Associated_User()
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

            

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, _HttpContextAccessor))
            {

                dbContext.Users.Add(user1);
                await dbContext.SaveChangesAsync().ConfigureAwait(true);

            }

            List<User> users;

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, _HttpContextAccessor))
            {

                users = await dbContext.Users.ToListAsync().ConfigureAwait(true);

            }

            var sampleUser = users.ElementAt(0);
            Trace.WriteLine($"sampleUser { sampleUser.FirstName} ");

            Assert.IsTrue(users.Count > 0) ;
            //this is a really bulky all at once test

            IEnumerable<PropertyInfo> userProperties = sampleUser.GetType().GetProperties();
            FingerPrintEntityBase t = new FingerPrintEntityBase();
            IEnumerable<string> fingerprintProperties = t.GetType().GetProperties().Select(PropertyInfo => PropertyInfo.Name);

            //Act
            bool UserPropertiesFilledIncorrectly = userProperties
            .Select(propertyInfo => { return (value: propertyInfo.GetValue(sampleUser), propertyInfo); })
            .Any((valueAndProperty) =>
            {

                if (valueAndProperty.value == null)
                {
                    Trace.WriteLine($"User { valueAndProperty.propertyInfo} was null");
                    return true;
                }

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

        }


    }



    

    
}