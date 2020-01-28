using AutoMapper;
using SecretSanta.Data;
using System.Reflection;

namespace SecretSanta.Business
{
    public class IgnoreIDAutomapperConfigurationProfile<TEntity> where TEntity : EntityBase 
    {
        private MapperConfiguration _MapperConfiguration;

        public IMapper Mapper { get; }

        public IgnoreIDAutomapperConfigurationProfile()
        {
            
             _MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntity, TEntity>().Ignore(option => option.Id);

            });

            Mapper = _MapperConfiguration.CreateMapper();
          
        }

        
    }
}