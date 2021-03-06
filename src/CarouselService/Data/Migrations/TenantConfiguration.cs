using System.Data.Entity.Migrations;
using CarouselService.Data;
using CarouselService.Data.Model;
using System;

namespace CarouselService.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(CarouselServiceContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default",
                UniqueId = new Guid("50848e1d-f3ec-486a-b25c-7f6cf1ef7c93")
            });

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Olivia Mitchell-Brown",
                UniqueId = new Guid("1c3dae2e-bad0-4958-8e7d-160eb0c470c8")
            });

            context.SaveChanges();
        }
    }
}
