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
    internal class CategoryRepository : GenericRepository<Catagory, ShopFramework2>, ICategoryRepository
    {
        public CategoryRepository(ShopFramework2 context)
            : base(context)
        {

        }

        public override bool Exists(int entityId)
        {
            return _context.Catagories.Any(x => x.ID == entityId);
        }
    }
}
