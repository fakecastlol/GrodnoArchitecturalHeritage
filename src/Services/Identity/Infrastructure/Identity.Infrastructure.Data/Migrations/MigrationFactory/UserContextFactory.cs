using Identity.Infrastructure.Data.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.Infrastructure.Data.Migrations.MigrationFactory
{
    public class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost, 14330;Database=UserDB;User=sa;Password=Password_111ssassSsvc111;TrustServerCertificate=true");

            return new UserContext(optionsBuilder.Options);
        }
    }
}
