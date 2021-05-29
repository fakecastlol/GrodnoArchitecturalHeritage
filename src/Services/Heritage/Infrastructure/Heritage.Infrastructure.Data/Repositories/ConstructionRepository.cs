using Heritage.Domain.Core.Entities;
using Heritage.Domain.Interfaces.Repositories;
using Heritage.Infrastructure.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Heritage.Infrastructure.Data.Repositories
{
    public class ConstructionRepository : GenericRepository<Construction>, IConstructionRepository
    {
        public ConstructionRepository(DbContext context) : base(context)
        {

        }

        public async Task<Construction> GetConstructionByIdAsync(Guid id)
        {
            var result = await _dbSet.Include(c => c.Images).FirstOrDefaultAsync(c => c.Id.Equals(id));

            return result;
        }

        public async Task<IQueryable<Construction>> GetAllConstructionsAsync(Expression<Func<Construction, bool>> predicate = null,
           Func<IQueryable<Construction>, IIncludableQueryable<Construction, object>> includes = null)
        {
            return await Task.Run(() =>
            {
                var result = _dbSet.Include(c => c.Images).AsQueryable();

                if (includes != null)
                    result = includes(result);

                return predicate != null ? result.Where(predicate) : result;
            });
        }
    }
}