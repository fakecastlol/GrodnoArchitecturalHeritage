using System;
using System.Linq;
using Identity.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Data.EFContext
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<UserContext>();

            context.Database.Migrate();

            var superAdmin =  context.Users.FirstOrDefault(u => u.Role.Equals(Roles.SuperAdmin));

            if (superAdmin != null) return;
            
            superAdmin = new UserEntity
            {
                Email = "superadmin@gmail.com",
                Id = Guid.NewGuid(),
                Role = Roles.SuperAdmin
            };

            context.Users.Add(superAdmin);
            context.SaveChanges();
        }
    }
}
