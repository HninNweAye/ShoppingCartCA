using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCartCA.Models;

namespace ShoppingCartCA.Util
{
    public class CookieUtil
    {
        public static List<CartCookie>  GetCartCookie(HttpCookieCollection cookieCollection) {
            List<CartCookie> cartCookieList = new List<CartCookie>();
            String[] keysArray = cookieCollection.AllKeys;
            HttpCookie httpCookie;
            string productId;
            int quantity;
            
            for (int i = 0; i < keysArray.Length; i++)
            {
                httpCookie = cookieCollection[keysArray[i]];
                productId = httpCookie.Name;
                quantity = int.Parse(httpCookie.Value);

                cartCookieList.Add(new CartCookie (productId,quantity));

            }
            return cartCookieList; 
        }
    }
}