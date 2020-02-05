using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Business
{
    public interface IEntityService<TDto, TInputDto>
        where TDto : class, TInputDto
        where TInputDto : class
    {
        Task<List<TDto>> FetchAllAsync();
        Task<TDto> FetchByIdAsync(int id);
        Task<TDto> InsertAsync(TInputDto entity);
        //Task<TEntity[]> InsertAsync(params TEntity[] entity);
        Task<TDto?> UpdateAsync(int id, TInputDto entity);
        Task<bool> DeleteAsync(int id);
    }
}
