using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass()]
    public class GiftTests: TestInitializer

    {

        [TestMethod]

        public async Task Initialize_Passes_If_Entity_Constructors_Are_Working_Correctly() {

            var user = new User("FirstName", "LastName");

            var title = "Title";
            var description = "Description";
            var url = "Url";
            var gift = new Gift(title, description, url, user);

            User s = gift.User;

       


            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, _HttpContextAccessor))
            {

                dbContext.Gifts.Add(gift);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);

            }


          


        }

        [TestMethod]
    
        public async Task Save_Gift_Retrieves_Gift_All_Properties_The_Same()
        {
            //arrange
            var fixture = new Fixture();


            var user = new User( "FirstName", "LastName");

            var title = "Title";
            var description = "Description";
            var url = "Url";

            var gift = new Gift(title, description, url, user);

          /*  Gift gift = new Gift {
                Title = title, Description = description, Url = url 
            }; */
           
          

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, _HttpContextAccessor))
            {
                
                    dbContext.Gifts.Add(gift);
                    await dbContext.SaveChangesAsync().ConfigureAwait(false);
             
            }

            List<Gift> gifts;

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, _HttpContextAccessor))
            {

                gifts = await dbContext.Gifts.Include(p => p.User).ToListAsync().ConfigureAwait(false);

            }

            var sampleGift = gifts.ElementAt(0);

            //this is a really bulky all at once test

            IEnumerable<PropertyInfo> giftProperties = sampleGift.GetType().GetProperties();
            FingerPrintEntityBase t = new FingerPrintEntityBase();
            IEnumerable<string> fingerprintProperties = t.GetType().GetProperties().Select(PropertyInfo=>PropertyInfo.Name);

                 //Act
                  bool GiftPropertiesFilledIncorrectly = giftProperties
                  .Select(propertyInfo => { return (value: propertyInfo.GetValue(sampleGift)!, propertyInfo); })
                  .Any((valueAndProperty) =>
                  {
                      
                      if (valueAndProperty.value == null)
                      {
                          Trace.WriteLine($"Gift { valueAndProperty.propertyInfo} was null");
                          return true;
                      }

                      if (valueAndProperty.propertyInfo.PropertyType == typeof(string)&& !fingerprintProperties.Contains(valueAndProperty.propertyInfo.Name))
                      {
                          if (valueAndProperty.propertyInfo.Name != (string)valueAndProperty.value)
                          {
                              Trace.WriteLine($"Gift { valueAndProperty.propertyInfo} was incorrectly assigned: {valueAndProperty.value}");
                              return true;
                          }
                      }
                      return false;
                  });
              //assert
                  Assert.IsFalse(GiftPropertiesFilledIncorrectly);
  
        }


    }



    

    
}