using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace KhumaloCraftWebApp.Models
{
    public class ProductDisplay : Controller
    {

        public int ProductID { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool Availability { get; set; }

        public ProductDisplay() { }

        //Parameterized Constructor: This constructor takes five parameters (id, name, price, category, availability) and initializes the corresponding properties of ProductDisplayModel with the provided values.
        public ProductDisplay(int id, string item, string description, decimal price, string category, bool availability)
        {
            ProductID = id;
            Item = item;
            Description = description;
            Price = price;
            Category = category;
            Availability = availability;
        }

        public static List<ProductDisplay> SelectProducts()
        {
            List<ProductDisplay> products = new List<ProductDisplay>();

            string con_string = "Integrated Security=SSPI;Persist Security Info=False;User ID=\"\";Initial Catalog=test;Data Source=labVMH8OX\\SQLEXPRESS";
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT ProductID, Item, Description, Price, Category, Availability FROM ProductTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductDisplay Product = new ProductDisplay();
                    Product.ProductID = Convert.ToInt32(reader["ProductID"]);
                    Product.Item = Convert.ToString(reader["Item"]);
                    Product.Description = Convert.ToString(reader["Description"]);
                    Product.Price = Convert.ToDecimal(reader["Price"]);
                    Product.Category = Convert.ToString(reader["Category"]);
                    Product.Availability = Convert.ToBoolean(reader["Availability"]);
                    products.Add(Product);
                }
                reader.Close();
            }
            return products;
        }

            /*public IActionResult Index()
            {
                return View();
            }*/
        }
    }
