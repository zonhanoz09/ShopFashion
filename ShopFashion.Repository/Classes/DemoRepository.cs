using ShopFashion.Model.Classes;
using ShopFashion.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFashion.Repository.Classes
{
    public interface IDemoRepository : IRepository<DemoTable>
    {
    }
    public class DemoRepository : Repository<DemoTable>, IDemoRepository
    {
        public DemoRepository(DbContext context) : base(context)
        {
        }
    }
}
