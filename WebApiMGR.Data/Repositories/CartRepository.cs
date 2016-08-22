using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMGR.Data.Interfaces;
using WebApiMGR.Data.Models.Generated;
using WebApiMGR.Data.Repositories.Base;

namespace WebApiMGR.Data.Repositories
{
    internal class CartRepository : GenericRepository<Cart, ShopFramework2>, ICartRepository
        {
            public CartRepository(ShopFramework2 context)
                : base(context)
            {

            }

            public override bool Exists(string entityId)
            {
                return _context.Carts.Any(x => x.CartId == entityId);
            }
        }
    
}
