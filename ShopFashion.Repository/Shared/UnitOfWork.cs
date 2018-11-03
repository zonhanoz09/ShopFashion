using ShopFashion.Model;
using ShopFashion.Model.Classes;
using ShopFashion.Repository.Classes;
using System;
using System.Data.Entity;

namespace ShopFashion.Repository.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        void Save();

        void Dispose();

        DemoRepository DemoRepository { get; }

    }
    public class UnitOfWork : IUnitOfWork
    {
        private ShopFashionContext context = new ShopFashionContext();
        private DemoRepository demoRepository;

        public DemoRepository DemoRepository {
            get
            {
                if (this.demoRepository == null)
                {
                    this.demoRepository = new DemoRepository(context);
                }
                return demoRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
