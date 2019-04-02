using ShoppingCartCA.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingCartCA.DB
{
    public class ProductsData
    {
        public static Products GetProductsByName(string username)
        {
            Products products = null;

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
            }
            return products;
        }
    }
}