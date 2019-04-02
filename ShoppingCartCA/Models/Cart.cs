using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartCA.Models
{
    public class Cart
    {
       
        public string UserName { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }

    }
}