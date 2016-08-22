using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiMGR.DTOs;
using WebApiMGR.Data.Models.Generated;


namespace WebApiMGR.App_Start
{
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Initialize Automapper configuration
        /// </summary>
        public static void RegisterMappings()
        {
            // Mappings goes here:
            RegisterInternalWebAPIMappings();
        }


        /// <summary>
        /// Internal Web API mappings:
        /// </summary>
        private static void RegisterInternalWebAPIMappings()
        {
            AutoMapper.Mapper.CreateMap<CartDTO, Cart>();
            AutoMapper.Mapper.CreateMap<Cart, CartDTO>();
            AutoMapper.Mapper.CreateMap<Catagory, CatagoryDTO>().ForSourceMember(x => x.Items, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<CatagoryDTO, Catagory>().ForMember(x => x.Items, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<ItemDTO, Item>().ForMember(x => x.Carts, opt => opt.Ignore()).ForMember(x => x.OrderDetails, opt => opt.Ignore()).ForMember(x => x.Catagory, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<Item, ItemDTO>().ForSourceMember(x => x.Carts, opt => opt.Ignore()).ForSourceMember(x => x.OrderDetails, opt => opt.Ignore()).ForSourceMember(x => x.Catagory, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<OrderDetail, OrderDetailDTO>();
            AutoMapper.Mapper.CreateMap<OrderDetailDTO, OrderDetail>();
            AutoMapper.Mapper.CreateMap<Order, OrderDTO>();
            AutoMapper.Mapper.CreateMap<OrderDTO, Order>();
            //AutoMapper.Mapper.CreateMap<OrderDTO, Order>();

            AutoMapper.Mapper.AssertConfigurationIsValid();

        }
    }
}