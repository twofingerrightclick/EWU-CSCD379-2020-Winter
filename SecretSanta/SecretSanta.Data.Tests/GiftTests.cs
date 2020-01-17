using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SecretSanta.Data.Tests
{
    [TestClass()]
    public class GiftTests

    {
       
        //
        [DataTestMethod]
        //correct data
        [DataRow(1, "FirstName", "LastName", 1, "Title", "Description", "Url",false)]
        
        public void All_Gift_Properties_Are_Set_Correctly(int id, string firstName, string lastName, int giftId, string title, string description, string url, bool assert)
        {
            //arrange
            User sampleUser = new User(id,firstName,lastName);
            Gift sampleGift = new Gift(giftId,title,description,url,sampleUser);
            IEnumerable<PropertyInfo> giftProperties = sampleGift.GetType().GetProperties();

               //Act
                bool GiftPropertiesFilledIncorrectly = giftProperties
                .Select(propertyInfo => { return (value: propertyInfo.GetValue(sampleGift)!, propertyInfo); })
                .Any((valueAndProperty) =>
                {
                    //not concerned about empty string values here.
                    if (valueAndProperty.value == null)
                    {
                        Trace.WriteLine($"Gift { valueAndProperty.propertyInfo} was null");
                        return true;
                    }

                    if (valueAndProperty.propertyInfo.PropertyType == typeof(string))
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
                Assert.IsTrue(GiftPropertiesFilledIncorrectly == assert);

            }


        [DataTestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
     
        //title null
        [DataRow( 1, null, "Description", "Url")]
        // description null
        [DataRow( 1, "Title", null, "Url")]
        //url null
        [DataRow( 1, "Title", "Description", null)]

        public void All_Gift_Properties_Are_Filled_Correctly_No_Nulls_User_And_Gift_Constructors_Throw_Exceptions(int giftId, string title, string description, string url)
        {
            var sampleUser = new User(1, "string", "string");

            Gift sampleGift = new Gift(giftId, title, description, url, sampleUser);


        }



    }



    

    
}