using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiMGR.DTOs
{
    public class CartDTO
    {
        public int ID { get; set; }
        public string CartId { get; set; }
        public int ItemId { get; set; }
        public int Count { get; set; }
        //public static string ShoppingCartId { get; set; }
        public System.DateTime DateCreated { get; set; }

        public  ItemDTO Item { get; set; }
    }
}
