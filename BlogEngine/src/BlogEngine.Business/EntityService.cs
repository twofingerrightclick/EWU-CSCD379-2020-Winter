using AutoMapper;
using BlogEngine.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Business
{
    public abstract class EntityService<TEntity, TDto, TInputDto> : IEntityService<TDto, TInputDto>
        where TEntity : EntityBase
        where TDto : class, TInputDto
        where TInputDto : class
    {
        protected ApplicationDbContext ApplicationDbContext { get; }

        protected IMapper Mapper { get; }

        public EntityService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            ApplicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            TEntity entity = await ApplicationDbContext.FindAsync<TEntity>(id);
            if (entity is { })
            {
                ApplicationDbContext.Remove(entity);
                await ApplicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<TDto>> FetchAllAsync()
        {
            List<TEntity> entities = await ApplicationDbContext.Set<TEntity>().ToListAsync();
            return Mapper.Map<List<TEntity>, List<TDto>>(entities);
        }

        public virtual async Task<TDto> FetchByIdAsync(int id)
        {
            return Mapper.Map<TEntity, TDto>(await ApplicationDbContext.FindAsync<TEntity>(id));
        }

        public async Task<TDto> InsertAsync(TInputDto inputDto)
        {
            TEntity entity = Mapper.Map<TInputDto, TEntity>(inputDto);

            ApplicationDbContext.Add(entity);
            await ApplicationDbContext.SaveChangesAsync();
            return Mapper.Map<TEntity, TDto>(entity);
        }

        public async Task<TDto?> UpdateAsync(int id, TInputDto inputDto)
        {
            if (await ApplicationDbContext.FindAsync<TEntity>(id) is TEntity result)
            {
                Mapper.Map(inputDto, result);
                await ApplicationDbContext.SaveChangesAsync();
                return Mapper.Map<TEntity, TDto>(result);
            }
            return null;
        }
    }
}
