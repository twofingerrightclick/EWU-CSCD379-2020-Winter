using AutoMapper;
using BlogEngine.Data;

namespace BlogEngine.Business
{
    public class AuthorService : EntityService<Author>, IAuthorService
    {
        public AuthorService(ApplicationDbContext context, IMapper mapper):
            base(context, mapper)
        { }
    }
}
