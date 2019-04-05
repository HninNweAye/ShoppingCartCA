using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShoppingCartCA.Models;
using System.Diagnostics;
using ShoppingCartCA.Util;

namespace ShoppingCartCA.DB
{
    public class PurchaseData : Data
    {
        public static List<PurchaseHistory> GetPurchasedList(string userName)
        {
            List<PurchaseHistory> purchasedList = new List<PurchaseHistory>();
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
                string sql = @"SELECT P.ProductID as ProductID,P.Name ,P.Price as Price,P.Description as Description,P.ImagePath as ImagePath,
                                PH.ActivationCode as ActivationCode,PH.PurchasedDate
                                FROM Products as P , PurchaseHistory as PH WHERE P.ProductID = PH.ProductID AND PH.UserName='" + userName
                                + "' ORDER BY PH.ProductID,PurchasedDate";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DateTime date1 =new DateTime() ,date2;
                string productName1 = "",productName2;
                List<string> activateCodeList = new List<string>();
                PurchaseHistory purchase = new PurchaseHistory();
                while (reader != null && reader.Read()) {
                    date2 = (DateTime)reader["PurchasedDate"];
                    productName2 = (string)reader["Name"];
                    if (date1.CompareTo(date2)==0 && productName1 == productName2)
                    {
                        purchase.Quantity += 1;
                        purchase.ActivationCode.Add((string)reader["ActivationCode"]);
            
                        purchasedList[purchasedList.Count-1] = purchase;
                    }
                    else {

                        activateCodeList = new List<string>();
                        activateCodeList.Add((string)reader["ActivationCode"]);
                         purchase = new PurchaseHistory
                        {
                            Name = (string)reader["Name"],
                            Price = (double)reader["Price"],
                            Description = (string)reader["Description"],
                            Image = (string)reader["ImagePath"],
                            PurchasedDate = (DateTime)reader["PurchasedDate"],
                            Quantity = 1,
                            ActivationCode = activateCodeList
                        };
                       // Debug.WriteLine(purchase.ActivationCode[0])
                        purchasedList.Add(purchase);
                    }
                    date1 = date2;
                    productName1 = productName2;
           

                }


            }

            return purchasedList;
        }

        public static Boolean CheckOut(List<CartCookie> cartCookies,string userName) {
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction()) {

                    int rowsAffected = 0;
                    int totalCartCount = 0;
                    using (SqlCommand cmd = new SqlCommand("", conn, transaction)) {
                        cmd.CommandText = @"INSERT INTO PurchaseHistory (UserName,ProductID,ActivationCode,PurchasedDate)
                                            VALUES (@UserName,@ProductID,@ActivationCode,@PurchasedDate)";
                        SqlParameter paramUserName = new SqlParameter();
                        paramUserName.ParameterName = "@UserName";
                        paramUserName.Value = userName;
                        SqlParameter paramDate = new SqlParameter();
                        paramDate.ParameterName = "@PurchasedDate";
                        paramDate.Value = DateTime.Today;
                        Debug.WriteLine("Date " + paramDate.Value);
                        SqlParameter param1 = null, param2 = null;
                        foreach (CartCookie cookie in cartCookies) {
                            for (int i = 1; i <= cookie.Quantity; i++) {
                                //for productId
                                param1 = new SqlParameter();
                                param1.ParameterName = "@ProductID";
                                param1.Value = cookie.ProductId;
                                //for ActivationCode
                                param2 = new SqlParameter();
                                param2.ParameterName = "@ActivationCode";
                                param2.Value = GenerateActivationCode();

                                cmd.Parameters.Clear();
                                cmd.Parameters.Add(paramUserName);
                                cmd.Parameters.Add(param1);
                                cmd.Parameters.Add(param2);
                                cmd.Parameters.Add(paramDate);

                                totalCartCount += 1;
                                rowsAffected += cmd.ExecuteNonQuery();
                            }
                           
                        }
                        if (rowsAffected == totalCartCount)
                        {
                            transaction.Commit();
                            return true;
                        }
                        else {
                            transaction.Rollback();
                            return false;
                        }
                    }
                   
                }

            }
        }
        private static string GenerateActivationCode() {
            return Guid.NewGuid().ToString();
        }
    }
}