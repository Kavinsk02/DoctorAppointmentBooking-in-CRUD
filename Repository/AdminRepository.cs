using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Web;
using WebApplication13.Models;

namespace WebApplication13.Repository
{
    public class AdminRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();



        //Admin 
        public Admin GetAdminByUsername(string Username,string Password)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Admin WHERE Username = @Username and Password=@Password", connection);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    Admin user = new Admin
                    {
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        UserRole = reader["UserRole"].ToString()
                    };

                    return user;
                }

                return null;
            }

        }

        public bool UpdateAdmin(Admin admin)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("Update_Admin", connection);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@Username", admin.Username);
                command.Parameters.AddWithValue("@Password", admin.Password);
                



                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}