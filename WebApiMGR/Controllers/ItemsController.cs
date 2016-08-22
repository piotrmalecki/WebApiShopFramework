using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiMGR.Constants;
using WebApiMGR.Controllers.BaseController;
using WebApiMGR.DTOs;


namespace WebApiMGR.Controllers
{

    [RoutePrefix("api/items")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ItemsController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var items = this.shopFrameworkDomain.GetAllItems();
            if (items != null)
            {
                List<ItemDTO> result = AutoMapper.Mapper.Map<List<ItemDTO>>(items);

                return OkCollectionResult(result.ToList());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult GetItemById(SearchDTO search)
        {
            IList<ItemDTO> item = null;
            if (search.Search == null) {
                  item = this.shopFrameworkDomain.GetAllItems();
            }
            else
            {
                  item = this.shopFrameworkDomain.GetItem(x =>
               x.Catagory.Name.ToLower().Contains(search.Search.ToLower()) || x.Name.ToLower().Contains(search.Search.ToLower())
             );
            }

            if (item.Count>0)
            {
                List<ItemDTO> result = AutoMapper.Mapper.Map<List<ItemDTO>>(item);
                return OkCollectionResult(result.ToList());
            }
            else
            {
                return NotFound();
            }
        }
    }
}
