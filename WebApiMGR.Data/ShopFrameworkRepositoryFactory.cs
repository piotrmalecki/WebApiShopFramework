using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiMGR.Data
{
    public static class ShopFrameworkRepositoryFactory
    {
        public static IShopFrameworkRepository CreateShopFrameworkRepository()
        {
            return new ShopFrameworkRepository() as IShopFrameworkRepository;
        }
    }
}
