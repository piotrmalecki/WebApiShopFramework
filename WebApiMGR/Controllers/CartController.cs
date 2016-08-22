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

    [RoutePrefix("api/carts")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize]
    public class CartController : BaseApiController
    {

        [HttpGet]
        [Route("getAll")]
        public IHttpActionResult GetAll()
        {
            var carts = this.shopFrameworkDomain.GetAllCarts();
            if (carts != null)
            {
                List<CartDTO> result = AutoMapper.Mapper.Map< IList <Cart> ,List <CartDTO>>(carts);
                return OkCollectionResult(carts.ToList());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult GetCartById(string id)
        {
            IList<CartDTO> carts = this.shopFrameworkDomain.GetCartDTOList(x =>
            {
                return x.CartId.Equals(id);
            });

            if (carts != null)
            {
                List<CartDTO> result = AutoMapper.Mapper.Map<List<CartDTO>>(carts);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        

        [HttpPost]
        [Route("{sessionId:guid}/{id:int}/add")]
        public IHttpActionResult AddToCart(int id, string sessionId)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                // Get the matching cart and item instances
                var cartItem = repository.CartRepository.GetSingle(x =>
            {
                return x.CartId == sessionId && x.ItemId == id;
            });

                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists
                    cartItem = new Cart
                    {
                        ItemId = id,
                        CartId = sessionId,
                        Count = 1,
                        DateCreated = DateTime.Now
                    };
                    repository.CartRepository.Add(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, 
                    // then add one to the quantity
                    cartItem.Count++;
                }
                // Save changes
                repository.CartRepository.Update(cartItem);

                return Ok();
            }
        }


        [HttpDelete]
        [Route("{sessionId:guid}/{id:int}/remove")]
        public IHttpActionResult RemoveFromCart(int id, string sessionId)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                // Get the matching cart and item instances
                var cartItem = repository.CartRepository.GetSingle(x =>
                {
                    return x.CartId == sessionId && x.ItemId == id;
                });

                if (cartItem == null)
                {
                    return NotFound();
                }
                else
                {
                    repository.CartRepository.Delete(cartItem);
                }
                return Ok();
            }
        }
    }
}