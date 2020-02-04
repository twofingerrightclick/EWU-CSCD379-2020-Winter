using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTests : EntityServiceTests<Dto.User, Dto.UserInput, Data.User>
    {
        protected override Data.User CreateEntity()
            => new Data.User(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

        protected override UserInput CreateInputDto()
        {
            return new UserInput
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString()
            };
        }

        protected override IEntityService<Dto.User, Dto.UserInput> GetService(ApplicationDbContext dbContext, IMapper mapper)
            => new UserService(dbContext, mapper);
    }
}