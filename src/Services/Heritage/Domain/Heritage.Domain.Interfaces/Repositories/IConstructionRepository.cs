using Heritage.Domain.Core.Entities;
using Heritage.Domain.Interfaces.Repositories.Generic;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Heritage.Domain.Interfaces.Repositories
{
    public interface IConstructionRepository : IGenericRepository<Construction>
    {
        Task<Construction> GetConstructionByIdAsync(Guid id);

        Task<IQueryable<Construction>> GetAllConstructionsAsync(Expression<Func<Construction, bool>> predicate = null,
           Func<IQueryable<Construction>, IIncludableQueryable<Construction, object>> includes = null);
    }
}