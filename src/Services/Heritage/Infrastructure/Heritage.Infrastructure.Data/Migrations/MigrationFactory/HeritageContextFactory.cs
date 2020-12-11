using Microsoft.EntityFrameworkCore.Design;
using Heritage.Infrastructure.Data.EFContext;
using Microsoft.EntityFrameworkCore;

namespace Heritage.Infrastructure.Data.Migrations.MigrationFactory
{
    public class HeritageContextFactory : IDesignTimeDbContextFactory<HeritageContext>
    {
        public HeritageContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HeritageContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost, 14330;Database=HeritageDB;User=sa;Password=Password_111ssassSsvc111;TrustServerCertificate=true", x => x.UseNetTopologySuite());

            return new HeritageContext(optionsBuilder.Options);
        }
    }
}