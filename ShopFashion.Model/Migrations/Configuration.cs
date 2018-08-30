using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ShopFashion.Model.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ShopFashionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; //for client team
            //AutomaticMigrationsEnabled = false; //for server team
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ShopFashionContext context)
        {
            //This method will be called after migrating to the latest version.
            //build initial master data
            //if (context.Users.Count() == 0)
            //    foreach (var user in BuildUsers.Build())
            //    {
            //        context.Users.AddOrUpdate(u => u.UserName, user);
            //    }
            //if (context.AuditClients.Count() == 0)
            //{
            //    context.AuditClients.AddRange(BuildClientsList());
            //}
            //if (context.Navigations.Count() == 0)
            //{
            //    BuildNavigationList(context);
            //}
            context.SaveChanges();
        }
    }
}