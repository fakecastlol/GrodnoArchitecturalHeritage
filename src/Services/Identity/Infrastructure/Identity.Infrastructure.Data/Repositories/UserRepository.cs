using System.Threading.Tasks;
using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories;
using Identity.Infrastructure.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }
    }
}
