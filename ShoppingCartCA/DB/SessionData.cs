using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace ShoppingCartCA.DB
{
    public class SessionData : Data
    {
        public static bool IsActiveSessionId(string sessionID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) From Users
                where SessionID = '" + sessionID + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int count = (int)cmd.ExecuteScalar();
                return (count == 1);
            }
        }
        public static string CreateSession (string username)
        {
            string sessionID = Guid.NewGuid().ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Users SET SessionID ='" 
                + sessionID + "'" + "Where UserName ='" + username+"'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            return sessionID;
        }

        public static void RemoveSession(string sessionId)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE Users SET SessionID = NULL";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}