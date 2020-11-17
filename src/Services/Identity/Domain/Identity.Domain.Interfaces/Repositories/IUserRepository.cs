using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories.Generic;

namespace Identity.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        //Task<UserEntity> CreateAsync(UserEntity user, Func<IQueryable<UserEntity>, IIncludableQueryable<UserEntity, object>> includes = null);
    }
}
