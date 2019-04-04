using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartCA.Models
{
    public class Cart : Products
    {
        public int Quantity { get; set; }

    }
}