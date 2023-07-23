using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication13.Models;

namespace WebApplication13.Repository
{
    public class SigninRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();


       

               
            public bool ValidateAdmin(string Username, string Password)
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    string query = "SELECT COUNT(*) FROM Admin WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", Username);
                    
                        command.Parameters.AddWithValue("@Password", Password);

                        connection.Open();
                        int count = (int)command.ExecuteScalar();

                        return count >=1;
                    }
                }
            }
        
            public bool ValidateUser(int Id,string Username, string Password)
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    string query = "SELECT COUNT(*) FROM Patient WHERE PatientId=@PatientId AND  Username = @Username AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                    Password EncryptData = new Password();
                    
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@PatientId",Id);

                    command.Parameters.AddWithValue("@Password",Password);

                        connection.Open();
                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }

            // Add any additional methods for database interactions here
       }
    

}
        
    

        


        






    
