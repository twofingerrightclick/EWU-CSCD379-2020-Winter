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
    public class BlogPostTests
    {
        [TestMethod()]
        public void BlogPost_Constructor_Correctly_Assigns_Variables()
        {
            BlogPost a = new BlogPost(title: "hello", content: "that", date: DateTime.Now);
            Assert.AreEqual<string>("hello", a.Title, "wrong title");
        }

        [TestMethod()]
        public void Constructor_No_Nulls()
        {
            BlogPost blogPost = new BlogPost(title: null,"content",) ;

        }


    }


    

    
}