using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartCA.Models
{
    public class PurchaseHistory : Products
    {

      //  public string PurchaseID { get; set; }
     //   public string UserName { get; set; }
        public List<string> ActivationCode { get; set; }
        public DateTime PurchasedDate { get; set; }
        public int Quantity { get; set; }

    }
}