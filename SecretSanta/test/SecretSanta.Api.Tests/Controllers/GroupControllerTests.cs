using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : BaseApiControllerTests<Group, GroupInput, GroupInMemoryService>
    {
        protected override BaseApiController<Group, GroupInput> CreateController(GroupInMemoryService service)
            => new GroupController(service);

        protected override Group CreateEntity()
            => new Group
            {
                Title = Guid.NewGuid().ToString(),
            };
    }


    public class GroupInMemoryService : InMemoryEntityService<Group, GroupInput>, IGroupService
    {
        private int NextId { get; set; }

        protected override Group Convert(GroupInput dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Group
            {
                Id = NextId++,
                Title = dto.Title,
            };
        }
    }
}
