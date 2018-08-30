using Microsoft.AspNet.Identity.EntityFramework;
using ShopFashion.Model.Classes;
using ShopFashion.Model.NotMapping;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopFashion.Model
{
    public class ShopFashionContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ShopFashionContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
            //this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<DemoTable> DemoTable { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //set schema
            modelBuilder.HasDefaultSchema("dbo");


            //call base function
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetModifiedInformation();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetModifiedInformation();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetModifiedInformation()
        {
            var identityName = Thread.CurrentPrincipal.Identity.Name;

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IEntity
                            && x.State != EntityState.Unchanged);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is IEntity entity)
                {
                    var currentDateTime = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.InsertedBy = identityName;
                        entity.InsertedAt = currentDateTime;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.InsertedBy).IsModified = false;
                        Entry(entity).Property(x => x.InsertedAt).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedAt = currentDateTime;
                }
            }
        }
    }
}
