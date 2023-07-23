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
    public class AttendanceRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();
        public void AddAttendance(Attendance ad)
        {
            using(SqlConnection connection=new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_Attendance", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@DoctorID", ad.DoctorID);
                command.Parameters.AddWithValue("@AttendanceDate", ad.AttendanceDate);
                command.Parameters.AddWithValue("@Ispresent", ad.Ispresent);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Attendance> GetAttendanceDetails()
        {
            List<Attendance> attendances = new List<Attendance>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_SelectAttendance", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                connection.Open();
                da.Fill(dt);
                connection.Close();
                foreach (DataRow dr in dt.Rows)
                    attendances.Add(
                        new Attendance
                        {
                            id = Convert.ToInt32(dr["id"]),

                            
                            DoctorID = Convert.ToString(dr["DoctorID"]),
                            AttendanceDate= Convert.ToDateTime(dr["AttendanceDate"]),
                            Ispresent = Convert.ToBoolean(dr["Ispresent"])

                        });

                return attendances;
            }
        }
    }
}