using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMGR.Data.Interfaces;
using WebApiMGR.Data.Interfaces.Base;
using WebApiMGR.Data.Models.Generated;
using WebApiMGR.Data.Repositories;

namespace WebApiMGR.Data
{
    internal class ShopFrameworkRepository : IShopFrameworkRepository
    {
        private ShopFramework2 _context;

        /// <summary>
        /// Repository of location entity
        /// </summary>
        public  ICartRepository CartRepository
        {
            get;
            private set;
        }
        public ICategoryRepository CategoryRepository
        {
            get;
            private set;
        }

        public IItemRepository ItemRepository
        {
            get;
            private set;
        }

        public IOrderDetailsRepository OrderDetailsRepository
        {
            get;
            private set;
        }

        public IOrderRepository OrderRepository
        {
            get;
            private set;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        internal ShopFrameworkRepository()
        {
            _context = new ShopFramework2();

            // Create business entities repositories here:
            this.CartRepository = new CartRepository(_context) as ICartRepository;
            this.CategoryRepository = new CategoryRepository(_context) as ICategoryRepository;
            this.ItemRepository = new ItemRepository(_context) as IItemRepository;
            this.OrderDetailsRepository = new OrderDetailsRepository(_context) as IOrderDetailsRepository;
            this.OrderRepository = new OrderRepository(_context) as IOrderRepository;
            
        }

        public IGenericRepository<TEntity, TContext> ResolveRepositoryForType<TEntity, TContext>()
            where TEntity : class
            where TContext : DbContext
        {
            if (typeof(TEntity) == typeof(Cart))
            {
                return this.CartRepository as IGenericRepository<TEntity, TContext>;
            }
            else if (typeof(TEntity) == typeof(Catagory))
            {
                return this.CategoryRepository as IGenericRepository<TEntity, TContext>;
            }
            else if (typeof(TEntity) == typeof(Item))
            {
                return this.ItemRepository as IGenericRepository<TEntity, TContext>;
            }
            else if (typeof(TEntity) == typeof(OrderDetail))
            {
                return this.OrderDetailsRepository as IGenericRepository<TEntity, TContext>;
            }
            else if (typeof(TEntity) == typeof(Order))
            {
                return this.OrderRepository as IGenericRepository<TEntity, TContext>;
            }

            return null;
        }

        #region IDisposable implementation:

        bool disposed = false;

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~ShopFrameworkRepository()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // free managed resources
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
            // free native resources if there are any.

            disposed = true;
        }

        #endregion
    }
}
