using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMGR.Data.Interfaces.Base;
using WebApiMGR.Data.Models.Generated;

namespace WebApiMGR.Data.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Catagory, ShopFramework2>
    {
    }
}
