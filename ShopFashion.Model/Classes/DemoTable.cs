using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFashion.Model.Classes
{
    [Table("DemoTable")]
    public class DemoTable : BaseEntity
    {
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
