using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories;
using Identity.Infrastructure.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Data.Repositories
{
    public class RoleRepository : GenericRepository<RoleEntity>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {

        }
    }
}