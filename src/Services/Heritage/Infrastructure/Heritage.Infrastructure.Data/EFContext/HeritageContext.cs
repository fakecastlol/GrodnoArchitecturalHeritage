using Heritage.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Heritage.Infrastructure.Data.EFContext
{
    public class HeritageContext : DbContext
    {
        public DbSet<Construction> Constructions { get; set; }
        public DbSet<Image> Images { get; set; }

        public HeritageContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
