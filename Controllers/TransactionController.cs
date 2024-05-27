using KhumaloCraftWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KhumaloCraftWebApp.Controllers
{
    public class TransactionController : Controller
    {
        private static string con_string = "Server=tcp:prada420-server.database.windows.net,1433;Initial Catalog=icespice420-servers;Persist Security Info=False;User ID=BulelaMhlana;Password=Wantonly41!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        [HttpPost]
        public ActionResult PlaceOrder(int UserID, int ProductID)
        {
            try
            {
                if (UserID <= 0 || ProductID <= 0)
                {
                    ViewBag.ErrorMessage = "Invalid UserID or ProductID.";
                    return View("Error");
                }

                // Insert the order into the database
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    string sql = "INSERT INTO TransactionTable (UserID, ProductID) VALUES (@UserID, @ProductID)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@ProductID", ProductID);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            // Retrieve the user's orders after placing the order
                            List<Order> userOrders = GetUserOrders(UserID);
                            return View("~/Views/Home/OrderDetails.cshtml", userOrders);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Order placement failed.";
                            return View("Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        private List<Order> GetUserOrders(int UserID)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = @"
                    SELECT UserTable.Name, TransactionTable.UserID, ProductTable.ProductID, ProductTable.Item, ProductTable.Description, ProductTable.Category, ProductTable.Availability
                    FROM TransactionTable
                    INNER JOIN UserTable ON TransactionTable.UserID = UserTable.UserID
                    INNER JOIN ProductTable ON TransactionTable.ProductID = ProductTable.ProductID
                    WHERE TransactionTable.UserID = @UserID";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            Username = reader["Name"].ToString(),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            Item = reader["Item"].ToString(),
                            Description = reader["Description"].ToString(),
                            Category = reader["Category"].ToString(),
                            Availability = reader["Availability"].ToString(),
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as necessary
                    throw ex;
                }
            }

            return orders;
        }
    }
}