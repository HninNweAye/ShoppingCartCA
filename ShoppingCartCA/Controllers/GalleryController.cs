using ShoppingCartCA.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCartCA.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult ProductList()
        {
            ViewData["productList"] = ProductsData.GetProductList();
            Debug.WriteLine(ViewData["productList"]);
            return View();
        }
        public ActionResult AddToCard(string ProductID) {

            return View("ProductList");
        }
    }
}