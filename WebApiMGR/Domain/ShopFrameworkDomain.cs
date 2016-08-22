using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebApiMGR.Data;
using WebApiMGR.Data.Models.Generated;
using WebApiMGR.DTOs;

namespace WebApiMGR.Domain
{

    public class ShopFrameworkDomain
    {
        public IList<Order> GetOrderById(Func<Order, bool> where, params Expression<Func<Cart, object>>[] includedNavigationProperties)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                return repository.OrderRepository.AsQueryable().Where(where).ToList();

            }
        }

        public IList<CartDTO> GetCartDTOList(Func<Cart, bool> where, params Expression<Func<Cart, object>>[] includedNavigationProperties)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                var cartList = repository.CartRepository.AsQueryable().Where(where).ToList();
                List <CartDTO> result = AutoMapper.Mapper.Map<List<Cart>, List<CartDTO>>(cartList);
                return result;
            }
        }

        public IList<Cart> GetCartList(Func<Cart, bool> where, params Expression<Func<Cart, object>>[] includedNavigationProperties)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                return repository.CartRepository.AsQueryable().Where(where).ToList();
                
            }
        }

        public IList<Cart> GetAllCarts()
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                return repository.CartRepository.GetAll();
            }
        }

        public void UpdateMonthlyApplication(Cart input)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                repository.CartRepository.Update(input);
            }
        }


        public IList<ItemDTO> GetAllItems()
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                return repository.ItemRepository.AsQueryable().Select(q => new ItemDTO()
                {
                    Name = q.Name,
                    Price = q.Price,
                    CatagoryName = q.Catagory.Name,
                    CatagorieId = q.CatagorieId,
                    ID = q.ID,
                    InternalImage = q.InternalImage,
                    ItemPictureUrl = q.ItemPictureUrl
                }).ToList();
            }
        }


        public IList<ItemDTO> GetItem(Func<Item, bool> where, params Expression<Func<Item, object>>[] includedNavigationProperties)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                return repository.ItemRepository.AsQueryable().Where(where).Select(q => new ItemDTO()
                {
                    Name = q.Name,
                    Price = q.Price,
                    CatagoryName = q.Catagory.Name,
                    CatagorieId = q.CatagorieId,
                    ID = q.ID,
                    InternalImage = q.InternalImage,
                    ItemPictureUrl = q.ItemPictureUrl
                }).ToList();
            }
        }

       

        public Catagory GetCatagory(Func<Catagory, bool> where, params Expression<Func<Catagory, object>>[] includedNavigationProperties)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                return repository.CategoryRepository.GetSingle(where, includedNavigationProperties);
            }
        }


        public void UpdateMonthlyApplication(Catagory input)
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                repository.CategoryRepository.Update(input);
            }
        }


        public IList<Catagory> GetAllCategories()
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                return repository.CategoryRepository.GetAll();
            }
        }


        public IShopFrameworkRepository GetRepository()
        {
            using (IShopFrameworkRepository repository = ShopFrameworkRepositoryFactory.CreateShopFrameworkRepository())
            {
                return repository;
            }
        }
    }
}
