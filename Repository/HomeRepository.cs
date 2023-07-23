using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication13.Models;


namespace WebApplication13.Repository
{
    public class HomeRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();

        public bool AddContactUsMessage(Contact message)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                

                using (SqlCommand command = new SqlCommand("Add_Contact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", message.Id);
                    command.Parameters.AddWithValue("@Name", message.Name);
                    command.Parameters.AddWithValue("@Email", message.Email);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                    command.Parameters.AddWithValue("@Message", message.Message);

                    connection.Open();
                    int Id=command.ExecuteNonQuery();
                    connection.Close();
                    if(Id>=1)
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

        public List<Contact> GetAllContactUsMessages()
        {
            List<Contact> messages = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
               
                using (SqlCommand command = new SqlCommand("Select_Contact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Contact message = new Contact
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Date = (DateTime)reader["Date"],
                            Message = reader["Message"].ToString()
                        };

                        messages.Add(message);
                    }
                }
            }

            return messages;
        }

    }
}