using System.Data.Entity.Migrations;
using CarouselService.Data;
using CarouselService.Data.Model;
using CarouselService.Features.Users;

namespace CarouselService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(CarouselServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.ACCOUNT_HOLDER
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
