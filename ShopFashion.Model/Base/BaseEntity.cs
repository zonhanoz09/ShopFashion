using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFashion.Model
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; set; }

        DateTime InsertedAt { get; set; }

        string InsertedBy { get; set; }

        DateTime UpdatedAt { get; set; }

        string UpdatedBy { get; set; }

        bool IsDeleted { get; set; }
    }
    public abstract class BaseEntity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        public DateTime InsertedAt { get; set; }

        public string InsertedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
