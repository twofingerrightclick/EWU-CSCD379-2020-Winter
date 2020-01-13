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
    public class UserTests

    {
       
        //
        [DataTestMethod]
        //correct data
        [DataRow(1, "FirstName", "LastName",false)]
        
        public void All_UserTests_Properties_Are_Set_Correctly(int id, string firstName, string lastName, bool assert)
        {
            //arrange
            User sampleUser = new User(id,firstName,lastName);
           
            IEnumerable<PropertyInfo> userProperties = sampleUser.GetType().GetProperties();

               //Act
                bool GiftPropertiesFilledIncorrectly = userProperties
                .Select(propertyInfo => { return (value: propertyInfo.GetValue(sampleUser)!, propertyInfo); })
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
        //firstName null
        [DataRow(1, null, "LastName")]
        //lastName null
        [DataRow(1, "firstName", null)]
     
        public void All_User_Properties_Are_Filled_Correctly_No_Nulls_User_And_User_Constructors_Throw_Exceptions(int id, string firstName, string lastName)
        {
            User sampleUser = new User(id, firstName, lastName);

           


        }



    }



    

    
}