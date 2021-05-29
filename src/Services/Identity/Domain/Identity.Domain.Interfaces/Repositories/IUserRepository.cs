using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories.Generic;

namespace Identity.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
