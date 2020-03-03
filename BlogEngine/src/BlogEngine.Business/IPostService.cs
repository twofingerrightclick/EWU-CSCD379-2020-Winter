using BlogEngine.Business.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Business
{
    public interface IPostService: IEntityService<Dto.Post, Dto.PostInput>
    {
        Task<List<Post>> FetchAllWithAuthorsAsync();
    }
}
