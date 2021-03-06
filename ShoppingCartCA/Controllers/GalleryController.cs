﻿using ShoppingCartCA.DB;
using ShoppingCartCA.Models;
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
        public ActionResult ProductList(string sessionId)
        {
            ViewData["productList"] = ProductsData.GetProductList("");
            ViewData["sessionId"] = sessionId;
           

            return View();
        }
        public PartialViewResult SearchResult(string search)
        {
            List<Products> products = ProductsData.GetProductList(search);
            ViewData["listSize"] = products.Count;
            return PartialView(products);
        }
    }
}