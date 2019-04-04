using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartCA.DB;
using ShoppingCartCA.Models;

namespace ShoppingCartCA.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult MyPurchase(string username,string sessionId)
        {
            User user = UsersData.GetUserBySessionId("abc123");
            if (user == null) RedirectToAction("Login", "LoginUser");
            ViewData["purchasedList"] = PurchaseData.GetPurchasedList("akzin");
            return View();
        }
    }
}