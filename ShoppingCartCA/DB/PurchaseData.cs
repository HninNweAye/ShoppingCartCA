using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShoppingCartCA.Models;
using System.Diagnostics;

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
                                PH.ActivationCode as ActivationCode,PH.Date
                                FROM Products as P , PurchaseHistory as PH WHERE P.ProductID = PH.ProductID AND PH.UserName='" + userName
                                +"' ORDER BY Date";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                long date1 = 0 ,date2;
                string productName1 = "",productName2;
                List<string> activateCodeList = new List<string>();
                PurchaseHistory purchase = new PurchaseHistory();
                while (reader != null && reader.Read()) {
                    date2 = (long)reader["Date"];
                    productName2 = (string)reader["Name"];
                    if (date1 == date2 && productName1 == productName2)
                    {
                        purchase.Quantity += 1;
                        purchase.ActivationCode.Add((string)reader["ActivationCode"]);
                   
                        purchasedList[purchasedList.Count-1] = purchase;
                        Debug.WriteLine("ActivationCOde : "+purchase.ActivationCode[0]);
                        Debug.WriteLine("ActivationCOde : "+purchase.ActivationCode[1]);

                    }
                    else {

                        activateCodeList = new List<string>();
                        activateCodeList.Add((string)reader["ActivationCode"]);
                         purchase = new PurchaseHistory
                        {
                            Name = (string)reader["Name"],
                            Price = (decimal)reader["Price"],
                            Description = (string)reader["Description"],
                            Image = (string)reader["ImagePath"],
                            PurchasedDate = (long)reader["Date"],
                            Quantity = 1,
                            ActivationCode = activateCodeList
                        };
                        Debug.WriteLine(purchase.ActivationCode[0]);
                        purchasedList.Add(purchase);
                    }
                    date1 = date2;
                    productName1 = productName2;
           

                }


            }

            return purchasedList;
        }
    }
}