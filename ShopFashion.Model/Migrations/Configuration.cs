using Microsoft.AspNet.Identity;
using ShopFashion.Model.NotMapping;
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

            //foreach (var user in BuildUsers())
            //{
            //    context.Users.AddOrUpdate(u => u.Id, user);
            //}

            //foreach (var role in BuidRoles())
            //{
            //    context.Roles.AddOrUpdate(u => u.Id, role);
            //}

            //BuidUserRoles(context);

            context.SaveChanges();
        }
        private ApplicationUser[] BuildUsers()
        {
            var passwordHash = new PasswordHasher();
            var password = passwordHash.HashPassword("Test@123");

            return new[]
            {
                new ApplicationUser
                {
                    Id = new Guid("70F0EF37-35F1-4C25-A57E-FA064DF47CFB"),
                    UserName = "admin@Petronas.GRnT.com",
                    PasswordHash = password,
                    PhoneNumber = "0987888999",
                    Email = "admin@Petronas.GRnT.com",
                    EmailConfirmed = true,
                    IsActive = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    ExpireDate = DateTime.UtcNow.AddDays(365),
                    InsertedAt = DateTime.UtcNow,
                    UpdatedAt  = DateTime.UtcNow
                }
            };
        }

        private ApplicationRole[] BuidRoles()
        {
            var roles = new[]
            {
                new ApplicationRole
                {
                     Id = new Guid("0EE4DC0F-A405-42CA-A42B-55B3C2DE204A"),
                     Name ="Super Admin"
                },
                new ApplicationRole
                {
                     Id = new Guid("1D631A07-9E64-464E-B850-48B7B1286179"),
                     Name ="Admin"
                },
                new ApplicationRole
                {
                     Id = new Guid("2A4E9B1E-9651-4663-B288-53E258FE0B87"),
                     Name ="Internal User"
                },
                
                new ApplicationRole
                {
                     Id = new Guid("57135453-FCC7-4620-A989-BC1277D80F45"),
                     Name ="Management User"
                } ,
                new ApplicationRole
                {
                     Id = new Guid("24EB8BBF-E7A2-4F0F-B639-DCC1AB96EECB"),
                     Name ="User"
                }
            };
            return roles;
        }

        private void BuidUserRoles(ShopFashionContext context)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == new Guid("70F0EF37-35F1-4C25-A57E-FA064DF47CFB"));
            if (user != null)
            {
                var adminRole = user.Roles.FirstOrDefault(x => x.RoleId == new Guid("1D631A07-9E64-464E-B850-48B7B1286179"));
                if (adminRole == null)
                {
                    user.Roles.Add(new ApplicationUserRole()
                    {
                        UserId = new Guid("70F0EF37-35F1-4C25-A57E-FA064DF47CFB"),
                        RoleId = new Guid("1D631A07-9E64-464E-B850-48B7B1286179")
                    });
                }
            }
        }
    }
}