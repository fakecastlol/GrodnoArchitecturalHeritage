using Identity.Domain.Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Identity.Domain.Interfaces.Repositories.Generic;


namespace Identity.Infrastructure.Data.Repositories.Abstract
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DbContext _context;

        protected readonly DbSet<TEntity> _dbSet;

        protected GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            return await Task.Run(() =>
            {
                var result = _dbSet.AsQueryable();

                if (includes != null)
                    result = includes(result);

                return predicate != null ? result.Where(predicate) : result;
            });
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var result = await _dbSet.FindAsync(id);

            return result;
        }

        public async Task<TEntity> CreateAsync(TEntity item)
        {
            var entityEntry = _dbSet.Add(item);
            await SaveAsync();

            return entityEntry.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity item)
        {
            var entityEntry = _dbSet.Update(item);
            await SaveAsync();

            return entityEntry.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _dbSet.FindAsync(id);

            if (item != null)
            {
                _dbSet.Remove(item);
            }

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
