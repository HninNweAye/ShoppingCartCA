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

            List<CartCookie> cartCookies = CookieFile.GetCartCookie(Request.Cookies);
            List<Cart> cartList = ProductsData.GetProductDetails(cartCookies);
            ViewData["cartList"] = cartList;
            ViewData["sessionId"] = "abc123";

            return View();
        }
        public ActionResult CheckOut(string userName , string sessionId) {
            List<CartCookie> cartCookies = CookieFile.GetCartCookie(Request.Cookies);
            Boolean success = PurchaseData.CheckOut(cartCookies,"akzin");
            if (success)
            {
                RemoveCookieData();
                return RedirectToAction("MyPurchase", "Purchase", new { sessionId });
            }
            else {
                return View();
            }
        }

        public void RemoveCookieData() {
            HttpCookieCollection cookieCollection = Request.Cookies;
            String[] keysArray = cookieCollection.AllKeys;
            HttpCookie httpCookie;

            for (int i = 0; i < keysArray.Length; i++)
            {
                httpCookie = cookieCollection[keysArray[i]];
                httpCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(httpCookie);
            }
        }
    }
}