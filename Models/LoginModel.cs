using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace KhumaloCraftWebApp.Models
{
    public class LoginModel : Controller
    {
        public static string con_string = "Server=tcp:prada420-server.database.windows.net,1433;Initial Catalog=icespice420-servers;Persist Security Info=False;User ID=BulelaMhlana;Password=Wantonly41!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public int SelectUser(string Name, string Surname, string Email, string Password)
        {
            int UserID = -1; // Default value if user is not found
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT UserID FROM UserTable WHERE Name = @Name AND Surname = @Surname AND Email = @Email";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Surname", Surname);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        UserID = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    // For now, rethrow the exception
                    throw ex;
                }
            }
            return UserID;
        }
    }
}
