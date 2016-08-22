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
    internal class ItemRepository : GenericRepository<Item, ShopFramework2>, IItemRepository
    {
        public ItemRepository(ShopFramework2 context)
            : base(context)
        {

        }

        public override bool Exists(int entityId)
        {
            return _context.Items.Any(x => x.ID == entityId);
        }
    }
}
