using System;
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
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            string adminEmail = "admin@gmail.ru";
            string adminPassword = "admin";

            // add role
            UserEntity superAdmin = new UserEntity { Id = Guid.NewGuid(), Email = adminEmail, Password = adminPassword, Role = Roles.SuperAdmin };

            //modelBuilder.Entity<RoleEntity>().HasData(new RoleEntity[] { adminRole, userRole });
            modelBuilder.Entity<UserEntity>().HasData(new UserEntity[] { superAdmin });
            base.OnModelCreating(modelBuilder);
        }
    }
}
