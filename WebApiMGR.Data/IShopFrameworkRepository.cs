using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMGR.Data.Interfaces;
using WebApiMGR.Data.Interfaces.Base;

namespace WebApiMGR.Data
{
    public interface IShopFrameworkRepository : IDisposable
    {
        ICartRepository CartRepository
        {
            get;
        }
        ICategoryRepository CategoryRepository
        {
            get;
        }

        IItemRepository ItemRepository
        {
            get;
        }

        IOrderDetailsRepository OrderDetailsRepository
        {
            get;
        }

        IOrderRepository OrderRepository
        {
            get;
        }

        IGenericRepository<TEntity, TContenxt> ResolveRepositoryForType<TEntity, TContenxt>()
            where TEntity : class
            where TContenxt : DbContext;
    }
}
