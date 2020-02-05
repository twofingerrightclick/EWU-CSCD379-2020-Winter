using AutoMapper;
using BlogEngine.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogEngine.Business
{
    public class PostService : EntityService<Post, Dto.Post, Dto.PostInput>, IPostService
    {
        public PostService(ApplicationDbContext applicationDbContext, IMapper mapper) 
            : base(applicationDbContext, mapper)
        {
        }

         //public override async Task<Dto.Post> FetchByIdAsync(int id) =>
         //   await ApplicationDbContext.Set<Post>().Include(nameof(Post.Author)).SingleAsync(item => item.Id == id);
    }
}
