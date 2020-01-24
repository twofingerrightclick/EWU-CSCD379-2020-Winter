using AutoMapper;
using BlogEngine.Data;

namespace BlogEngine.Business
{
    public class AutomapperConfigurationProfile :Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Author, Author>()
        }
    }
}


