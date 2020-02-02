using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SecretSanta.Business
{
	public abstract class EntityService<TEntity> : IEntityService<TEntity>
		where TEntity : EntityBase
	{
		#region Properties
		protected ApplicationDbContext ApplicationDbContext { get; }
		protected IMapper Mapper { get; }
		#endregion

		#region Constructor
		public EntityService(ApplicationDbContext applicationDbContext, IMapper mapper)
		{
			ApplicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
			Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		#endregion

		#region Methods

		#region Fetch
		public async Task<List<TEntity>> FetchAllAsync() =>
			await ApplicationDbContext.Set<TEntity>().ToListAsync();

		virtual public async Task<TEntity> FetchByIdAsync(int id)
		{
			return await ApplicationDbContext.Set<TEntity>().SingleAsync(item => item.Id == id);
		}
		#endregion

		#region Insert
		public async Task<TEntity> InsertAsync(TEntity entity)
		{
			await InsertAsync(new[] { entity });
			return entity;
		}

		public async Task<TEntity[]> InsertAsync(params TEntity[] entities)
		{
			foreach (TEntity entity in entities) {
				ApplicationDbContext.Set<TEntity>().Add(entity);
				await ApplicationDbContext.SaveChangesAsync();
			}
			return entities;
		}
		#endregion

		#region Update
		public async Task<TEntity> UpdateAsync(int id, TEntity entity)
		{
			TEntity result = await ApplicationDbContext.Set<TEntity>().SingleAsync(item => item.Id == id);
			Mapper.Map(entity, result);
			await ApplicationDbContext.SaveChangesAsync();
			return result;
		}
		#endregion

		#region Delete
		public async Task<bool> DeleteAsync(int id)
		{
			TEntity entity = ApplicationDbContext.Set<TEntity>().Find(id);
			if (entity!=null)
			{
				ApplicationDbContext.Set<TEntity>().Remove(entity);
				await ApplicationDbContext.SaveChangesAsync();
				return true;
			}
			return false;
		}
		#endregion

		#endregion

	}
}