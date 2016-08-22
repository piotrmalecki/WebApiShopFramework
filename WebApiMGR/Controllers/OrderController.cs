using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiMGR.Controllers.BaseController;
using WebApiMGR.Data;
using WebApiMGR.Data.Models.Generated;
using WebApiMGR.DTOs;

namespace WebApiMGR.Controllers
{

    [RoutePrefix("api/orders")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    public class OrderController : BaseApiController
    {

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetOrderById(int id)
        {
            IList<Order> orders = this.shopFrameworkDomain.GetOrderById(x =>
            {
                return x.OrderId.Equals(id);
            });

            if (orders != null)
            {
                List<OrderDTO> result = AutoMapper.Mapper.Map<List<OrderDTO>>(orders);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("{sessionId:guid}/add")]
        public IHttpActionResult CreateOrder([FromUri] string sessionId, OrderDTO order)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                IList<Cart> carts  = repository.CartRepository.AsQueryable().Where(x=>x.CartId.Equals(sessionId)).ToList();
                    
               

                if (carts.Count>0)
                {
                    // Create a new cart item if no cart item exists
                    OrderDTO orderDTO = order;
                    orderDTO.Experation = DateTime.Now.AddYears(1);
                    orderDTO.OrderDate = DateTime.Now;
                    orderDTO.Username = order.Email;
                    decimal total = 0;
                    foreach (var item in carts)
                    {
                        total += item.Count * item.Item.Price;
                        orderDTO.OrderDetails = new List<OrderDetailDTO>();
                        orderDTO.OrderDetails.Add(new OrderDetailDTO
                        {
                            ItemId = item.ItemId,
                            Quantity = item.Count,
                            UnitPrice = item.Item.Price
                        });
                    }
                    orderDTO.Total = total;
                    orderDTO.OrderDate = DateTime.Now.AddYears(1);
                    orderDTO.SaveInfo = true;
                    repository.OrderRepository.Add(AutoMapper.Mapper.Map<Order>(orderDTO));
                    return Ok();
                }
                return BadRequest();
            }
        }
    }
}