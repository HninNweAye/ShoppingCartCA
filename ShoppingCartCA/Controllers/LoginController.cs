using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCartCA.Models;
using ShoppingCartCA.DB;
using System.Diagnostics;
using System.Data.SqlClient;

namespace ShoppingCartCA.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult LoginUser(string Username, string Password)
        {
            if (Username == null)
            return View();

            User user = UsersData.GetUserByUserName(Username);
            if (user.Password != Password)
            {
                return View();
            }

            string sessionId = SessionData.CreateSession(Username);
            return RedirectToAction("ProductList", "Gallery", new { sessionId });
            
        }
    }
}