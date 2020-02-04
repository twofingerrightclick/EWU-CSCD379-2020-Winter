using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Data;
using System;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftTests
    {


        string _Title = "Ring Doorbell";
        string _Url = "www.ring.com";
        string _Description = "The doorbell that saw too much";
      


        [TestMethod]
        public async Task Gift_CanBeSavedToDatabase()
        {
            // Arrange
           
        }

        [TestMethod]
        public void Create_UserController_Success()
        {
            //Arrange
            // var service = new UserService();
            var controller = new Mock<EntityController<User>>();
            // service.Setup(x => x.P);


            //Act
            // _ = new EntityController<IUserService>(service);

            //Assert
        }

    }
}
