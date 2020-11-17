using System;
using Identity.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Data.EFContext
{
    public class UserContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public UserContext(DbContextOptions options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            // add role
            RoleEntity adminRole = new RoleEntity { Id = Guid.NewGuid(), Name = adminRoleName };
            RoleEntity userRole = new RoleEntity { Id = Guid.NewGuid(), Name = userRoleName };
            UserEntity adminUser = new UserEntity { Id = Guid.NewGuid(), Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<RoleEntity>().HasData(new RoleEntity[] { adminRole, userRole });
            modelBuilder.Entity<UserEntity>().HasData(new UserEntity[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
