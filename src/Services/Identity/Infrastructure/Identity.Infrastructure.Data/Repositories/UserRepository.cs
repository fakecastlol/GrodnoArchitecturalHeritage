using System;
using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories;
using Identity.Infrastructure.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Identity.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        //public async Task<UserEntity> CreateAsync(UserEntity user, Func<IQueryable<UserEntity>, IIncludable<UserEntity, object>> includes = null)
        //{
        //    var entityEntry = _dbSet.Add(user);
        //    await SaveAsync();

        //    return entityEntry.Entity;
        //}
    }
}
