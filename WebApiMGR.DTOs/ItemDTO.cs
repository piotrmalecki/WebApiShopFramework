using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiMGR.DTOs
{
    public class ItemDTO
    {
        public int ID { get; set; }
        public int CatagorieId { get; set; }
        public string CatagoryName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ItemPictureUrl { get; set; }
        public byte[] InternalImage { get; set; }
        
        //public ICollection<CartDTO> Carts { get; set; }
        //public CatagoryDTO Catagory { get; set; }
       // public ICollection<OrderDetailDTO> OrderDetails { get; set; }

    }
}
