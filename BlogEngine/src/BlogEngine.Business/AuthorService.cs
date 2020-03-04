using AutoMapper;
using BlogEngine.Data;

namespace BlogEngine.Business
{
    public class AuthorService : EntityService<Author, Dto.Author, Dto.AuthorInput>, IAuthorService
    {
        public AuthorService(ApplicationDbContext context, IMapper mapper):
            base(context, mapper)
        { }
    }
}
