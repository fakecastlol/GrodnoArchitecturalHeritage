using Identity.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Data.EFContext
{
    public class UserContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public UserContext(DbContextOptions options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
