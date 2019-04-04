using ShoppingCartCA.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingCartCA.DB
{
    public class ProductsData : Data
    {
        public static List<Products> GetProductList(string searchKeyword)
        {
            List<Products> productList = new List<Products>();

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
                string sql = @"SELECT ProductID,Name,Price,Description,ImagePath FROM Products WHERE Description like '%"
                                            +searchKeyword+"%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader != null && reader.Read())
                {

                    Products product = new Products()
                    {
                        ProductID = (string)reader["ProductID"],
                        Name = (string)reader["Name"],
                        Price = (double)reader["Price"],
                        Description = (string)reader["Description"],
                        Image = (string)reader["ImagePath"]
                    };
                    productList.Add(product);
                }
            }
            return productList;

        }
 
        public static List<Cart> GetProductDetails(List<CartCookie> cartCookieList)
        {
        List<Cart> cartList = new List<Cart>();

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
                foreach (CartCookie cartCookie in cartCookieList) {

                    string sql = @"SELECT Name,Price,Description,ImagePath FROM Products WHERE ProductID = "+cartCookie.ProductId;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader != null && reader.Read())
                    {

                        Cart cart = new Cart()
                        {
                            ProductID = cartCookie.ProductId,
                            Quantity = cartCookie.Quantity,
                            Name = (string)reader["Name"],
                            Price = (double)reader["Price"],
                            Description = (string)reader["Description"],
                            Image = (string)reader["ImagePath"]
                        };
                        cartList.Add(cart);
                    }
                    reader.Close();
                }
            }
            return cartList;

        }
    }
}
