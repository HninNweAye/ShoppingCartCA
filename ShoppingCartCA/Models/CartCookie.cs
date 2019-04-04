using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartCA.Models
{
    public class CartCookie
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        public CartCookie(string productId, int quantity) {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}