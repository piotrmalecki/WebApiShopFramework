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
    internal class OrderDetailsRepository : GenericRepository<OrderDetail, ShopFramework2>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ShopFramework2 context)
            : base(context)
        {

        }

        public override bool Exists(int entityId)
        {
            return _context.OrderDetails.Any(x => x.OrderDetailId == entityId);
        }
    }
}
