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
        public static List<Products> GetProductList()
        {
            List<Products> productList = new List<Products>();

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
                string sql = @"SELECT ProductID,Name,Price,Description,ImagePath From Products";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader != null && reader.Read())
                {
                   
                    Products product = new Products()
                    {
                        ProductID = (string)reader["ProductID"],
                        Name = (string)reader["Name"],
                        Price = (decimal)reader["Price"],
                        Description = (string)reader["Description"],
                        Image = (string)reader["ImagePath"]
                    };
                    productList.Add(product);
                }
            }
            return productList;

        }
    }
}

    /*
       public static List<Class> GetClassesByLecturerId(int LecturerId)
        {
            List<Class> classes = new List<Class>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT Class.Id as ClassId, Course.Id as CourseId, 
                    Course.Name, Class.Startdate, Class.Enddate 
                        FROM Class, Course, Lecturer
                            WHERE Class.LecturerId = " + LecturerId +
                                @" AND Class.LecturerId = Lecturer.Id" +
                                @" AND Class.CourseId = Course.Id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Class _class = new Class()
                    {
                        Id = (int)reader["ClassId"],
                        CourseId = (int)reader["CourseId"],
                        CourseName = (string)reader["Name"],
                        StartDate = (long)reader["StartDate"],
                        EndDate = (long)reader["EndDate"]
                    };

                    classes.Add(_class);
                }
            }

            return classes;
        }
    }
     */
    //}