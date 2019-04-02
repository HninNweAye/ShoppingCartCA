using ShoppingCartCA.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using ShoppingCartCA.Models;


namespace ShoppingCartCA.DB
{
    public class UsersData
    {
        public static User GetUserByUserName(string username)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
                string sql = @"SELECT UserName,Password from Users
                WHERE UserName ='" + username + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        Password = (string)reader["Password"]
                    };
                }
            }
            return user;
        }
        public static User GetUserBySessionId(string sessionId)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string q = @"SELECT  User.Firstname, User.Lastname
                        User WHERE.SessionId = '" + sessionId + "'";

                SqlCommand cmd = new SqlCommand(q, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"]
                    };
                }
            }
            return user;
        }
    }
}