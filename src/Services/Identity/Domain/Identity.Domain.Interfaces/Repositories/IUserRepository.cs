using System;
using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories.Generic;
using Microsoft.EntityFrameworkCore.Query;

namespace Identity.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        //Task<UserEntity> CreateAsync(UserEntity user, Func<IQueryable<UserEntity>, IIncludableQueryable<UserEntity, object>> includes = null);
    }
}
