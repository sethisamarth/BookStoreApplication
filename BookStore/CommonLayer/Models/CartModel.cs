using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class CartModel
    {
        public int CartID { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
       // public int AddressId { get; set; }
    }
}
