using BlogEngine.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;

namespace BlogEngine.Business
{
    public class AuthorService
    {
        private ApplicationDbContext DbContext { get; }

        public AuthorService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Author> InsertAsync(Author author)
        {
            EntityEntry<Author> insertedEntity = DbContext.Authors.Add(author);
            await DbContext.SaveChangesAsync();
            return insertedEntity.Entity;
        }
    }
}