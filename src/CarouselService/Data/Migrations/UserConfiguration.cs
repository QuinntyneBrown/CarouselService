using System.Data.Entity.Migrations;
using CarouselService.Data;
using CarouselService.Data.Model;
using System.Linq;
using CarouselService.Security;
using System.Collections.Generic;

namespace CarouselService.Migrations
{
    public class UserConfiguration
    {
        public static void Seed(CarouselServiceContext context) {

            var systemRole = context.Roles.First(x => x.Name == Roles.SYSTEM);
            var roles = new List<Role>();
            var tenant = context.Tenants.Single(x => x.Name == "Default");

            roles.Add(systemRole);
            context.Users.AddOrUpdate(x => x.Username, new User()
            {
                Username = "system",
                Password = new EncryptionService().TransformPassword("system"),
                Roles = roles,
                TenantId = tenant.Id
            });

            context.SaveChanges();
        }
    }
}
