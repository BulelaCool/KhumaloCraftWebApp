using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace KhumaloCraftWebApp.Models
{
    public class UserTable
    {

        public static string con_string = "Server=tcp:prada420-server.database.windows.net,1433;Initial Catalog=icespice420-servers;Persist Security Info=False;User ID=BulelaMhlana;Password=Wantonly41!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int InsertUser(UserTable m)
        {
            try
            {
                string sql = "INSERT INTO UserTable (Name, Surname, Email, Password) VALUES (@Name, @Surname, @Email, @Password)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", m.Name);
                cmd.Parameters.AddWithValue("@Surname", m.Surname);
                cmd.Parameters.AddWithValue("@Email", m.Email);
                cmd.Parameters.AddWithValue("@Password", m.Password);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, rethrow the exception
                throw ex;
            }


        }

    }
}
