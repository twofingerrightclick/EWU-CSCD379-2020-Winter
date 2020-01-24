using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogEngine.Business;
using System;
using System.Collections.Generic;
using System.Text;
using BlogEngine.Data;
using BlogEngine.Data.Tests;
using AutoMapper;

namespace BlogEngine.Business.Tests
{
    [TestClass]
    public class AutomapperConfigurationProfileTests
    {

        class MockAuthor : Author
        {
            public MockAuthor(int id, string firstName, string lastName) :
                base(firstName, lastName, "apple@microsoft.com")
            {
                Id = id;
            }
                
        }
        [TestMethod]
        public void AutomapperConfigurationProfileTest()
        {
            Author source = SampleData.CreateInigoMontoya();
            Author target = SampleData.CreatePrincess();

            IMapper mapper = AutomapperConfigurationProfile.CreateMapper();
            mapper.Map(source, target);
            Assert.AreNotEqual<int?>(source.Id, target.Id);
            Assert.AreEqual<string>(source.LastName, target.LastName);
        }

        [TestMethod]
        public void Map_Author_SuccessWithNoIdMapped()
        {
            //(Author source, Author target) = (
            //    new MockAuthor(42, "Inigo", "Montoya"), new MockAuthor(0, "Invalid", "Invalid"));

        }
    }
}