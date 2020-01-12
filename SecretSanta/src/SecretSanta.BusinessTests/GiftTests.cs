using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SecretSanta.Business.Tests
{
    [TestClass()]
    public class GiftTests

    {
       

        [TestMethod]
        public void All_Gift_Properties_Are_Filled_Correctly_No_Nulls()
        {
            User sampleUser = new User(1,"FirstName","LastName",null);
            
            Gift sampleGift = new Gift(1,"Title","Description","Url",sampleUser);


           

                IEnumerable<PropertyInfo> giftProperties = sampleGift.GetType().GetProperties();

                //done this way to practice with linq and reflection

                bool nullsInGiftProperties = giftProperties
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

                Assert.IsFalse(nullsInGiftProperties);

            }
        }



    

    
}