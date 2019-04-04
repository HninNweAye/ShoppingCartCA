using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartCA.Models;
using ShoppingCartCA.DB;
using ShoppingCartCA.Util;

namespace ShoppingCartCA.Controllers
{
    public class CartController : Controller
    {
        public ActionResult ViewCart()
        {

            List<CartCookie> cartCookies = CookieUtil.GetCartCookie(Request.Cookies);
            List<Cart> cartList = ProductsData.GetProductDetails(cartCookies);
            ViewData["cartList"] = cartList;
            
            return View();
        }
    }
}