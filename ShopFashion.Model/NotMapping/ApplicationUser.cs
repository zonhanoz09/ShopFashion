using Microsoft.AspNet.Identity.EntityFramework;
using System;
using ShopFashion.Model.Base;

namespace ShopFashion.Model.NotMapping
{
    public class ApplicationUserRole : IdentityUserRole<Guid> { }
    public class ApplicationUserLogin : IdentityUserLogin<Guid> { }
    public class ApplicationUserClaim : IdentityUserClaim<Guid> { }
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole> { }

    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IEntity
    {
        public bool IsActive { get; set; }

        public DateTime ExpireDate { get; set; }

        public DateTime InsertedAt { get; set; }

        public string InsertedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }

}
