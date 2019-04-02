using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartCA.Models
{
    public class Products
    {
        public string PurchaseID { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ActivationCode { get; set; }

        public long PurchasedDate { get; set; }
    }
}