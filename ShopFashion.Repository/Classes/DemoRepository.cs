using ShopFashion.Model;
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
    public class DemoRepository : Repository<DemoTable>
    {
        internal ShopFashionContext context;
        internal DbSet<DemoTable> dbSet;

        public DemoRepository(ShopFashionContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<DemoTable>();
        }
    }
}
