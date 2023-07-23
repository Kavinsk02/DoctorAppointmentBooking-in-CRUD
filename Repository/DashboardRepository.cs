using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication13.Repository
{
    public class DashboardRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();
        public int GetRegisteredUserCount()
        {
            using (var connection = new SqlConnection(conString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM Patient", connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }


        //Doctor
        public int GetRegisteredDoctorCount()
        {
            using (var connection = new SqlConnection(conString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM Doctor_details", connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }


        //Appointments

        public int GetAppointmentCount()
        {
            using (var connection = new SqlConnection(conString))
            {
                connection.Open();

                DateTime today = DateTime.Today;
                string query = "SELECT COUNT(*) FROM App WHERE AppointmentDate = @Today";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Today", today);
                    return (int)command.ExecuteScalar();
                }
            }
        }





    }
}