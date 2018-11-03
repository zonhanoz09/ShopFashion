using ShopFashion.Model.NotMapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFashion.Model.Classes.UserRoles
{
    [Table("UserProfile")]
    public class UserProfile : BaseEntity
    {
        public UserProfile() : base()
        {

        }

        [StringLength(100)]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNumber { get; set; }

        public string Title { get; set; }

        public Guid UserId { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
