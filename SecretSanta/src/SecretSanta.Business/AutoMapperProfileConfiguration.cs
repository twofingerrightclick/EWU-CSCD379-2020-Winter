using AutoMapper;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SecretSanta.Business
{
    public class AutoMapperProfileConfiguration : Profile 
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Gift, Gift>().ForMember(property => property.Id, option => option.Ignore());
            
        }

        static public IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
