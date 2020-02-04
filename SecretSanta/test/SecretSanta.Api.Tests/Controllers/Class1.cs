using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Data;
using System;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class Class1<TEntity> : ControllerBase where TEntity : EntityBase
    {
        [TestMethod]
        public void Create_UserController_Success()
        {
            //Arrange
            // var service = new UserService();
            var controller = new Mock<EntityController<TEntity>>();
            // service.Setup(x => x.P);


            //Act
            // _ = new EntityController<IUserService>(service);

            //Assert
        }
    }
}