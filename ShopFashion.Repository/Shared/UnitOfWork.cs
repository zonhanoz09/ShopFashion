using ShopFashion.Model;
using ShopFashion.Model.Classes;
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

        Repository<DemoTable> DemoRepository { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        private ShopFashionContext context = new ShopFashionContext();
        private Repository<DemoTable> demoRepository;

        public Repository<DemoTable> DemoRepository
        {
            get
            {
                if (this.demoRepository == null)
                {
                    this.demoRepository = new Repository<DemoTable>(context);
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
