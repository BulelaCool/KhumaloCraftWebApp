using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KhumaloCraftWebApp.Models
{
    public class ProductTable
    {
        public static string con_string = "Server=tcp:prada420-server.database.windows.net,1433;Initial Catalog=icespice420-servers;Persist Security Info=False;User ID=BulelaMhlana;Password=Wantonly41!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public int ProductID { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Availability { get; set; }

        public int InsertProduct(ProductTable p)
        {
            try
            {
                string sql = "INSERT INTO ProductTable (Item, Description, Price, Category, Availability) VALUES (@Item, @Description, @Price, @Category, @Availability)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Item", p.Item);
                    cmd.Parameters.AddWithValue("@Description", p.Description);
                    cmd.Parameters.AddWithValue("@Price", p.Price);
                    cmd.Parameters.AddWithValue("@Category", p.Category);
                    cmd.Parameters.AddWithValue("@Availability", p.Availability);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw ex;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public static List<ProductTable> GetAllProducts()
        {
            List<ProductTable> Products = new List<ProductTable>();
            try
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    string sql = "SELECT * FROM ProductTable";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ProductTable Product = new ProductTable
                            {
                                ProductID = Convert.ToInt32(rdr["ProductID"]),
                                Item = rdr["Item"].ToString(),
                                Description = rdr["Description"].ToString(),
                                Price = rdr["Price"].ToString(),
                                Category = rdr["Category"].ToString(),
                                Availability = rdr["Availability"].ToString()
                            };
                            Products.Add(Product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw ex;
            }
            return Products;
        }
    }
}