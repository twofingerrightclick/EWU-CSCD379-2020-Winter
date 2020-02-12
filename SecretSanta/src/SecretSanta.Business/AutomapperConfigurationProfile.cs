using AutoMapper;
using SecretSanta.Data;
using System.Reflection;

namespace SecretSanta.Business
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Dto.GiftInput, Gift>();
            CreateMap<Gift, Dto.Gift>();

            CreateMap<Dto.UserInput, User>();
            CreateMap<User, Dto.User>();

            CreateMap<Dto.GroupInput, Group>();
            CreateMap<Group, Dto.Group>();
        }

        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
