using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;
using static WebApplication13.Models.Appointment;

namespace WebApplication13.Repository
{
    public class AppointmentRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();


        public List<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            List<Doctor> doctors = new List<Doctor>();

            using (SqlConnection connection = new SqlConnection(conString))
            {


                using (SqlCommand command = new SqlCommand("SearchDoctorBySpecialization", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(specialization))
                    {
                        command.Parameters.AddWithValue("@Specialization", specialization);
                    }
                    else
                    {
                        return doctors;
                    }


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Doctor doctor = new Doctor
                        {
                            DoctorID = (int)reader["DoctorID"],
                            Doctorname = (string)reader["Doctorname"],
                            Specialization = (string)reader["Specialization"]
                        };

                        doctors.Add(doctor);
                    }
                }
            }


            return doctors;
        }

        public bool IsDuplicateAppointment(int PatientId)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM App WHERE PatientId = @PatientId", connection))
                {
                    command.Parameters.AddWithValue("@PatientId", PatientId);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        //public void ConfirmAppointment(int appointmentId)
        //{
        //    using (SqlConnection connection = new SqlConnection(conString))
        //    {
        //        connection.Open();

        //        string updateQuery = "UPDATE App SET Status = 'Confirmed' WHERE App_id = @AppID";

        //        using (SqlCommand command = new SqlCommand(updateQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@AppID", appointmentId);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        public void CancelAppointment(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                string updateQuery = "UPDATE App SET Status = 'Canceled' WHERE App_id = @AppID";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@AppID", appointmentId);
                    command.ExecuteNonQuery();
                }
            }
        }

        //PAtientId
        public List<Appointment> GetAppointmentsByPatientId(int PatientId)
        {

            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                string query = "SELECT * FROM App WHERE PatientId = @PatientId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientId", PatientId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment
                            {
                                DoctorID = Convert.ToInt32(reader["DoctorID"]),
                                PatientId = Convert.ToInt32(reader["PatientId"]),
                                Username = reader["Username"].ToString(),
                                Fullname = reader["Fullname"].ToString(),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                AppointmentTime = TimeSpan.TryParse(reader["AppointmentTime"].ToString(), out var appointmentTime)
    ? appointmentTime
    : TimeSpan.Zero

                            };


                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
        }



        public bool BookAppointment(Appointment appointment)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("BookAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppID", appointment.AppID);
                    command.Parameters.AddWithValue("@DoctorID", appointment.DoctorID);
                    // command.Parameters.AddWithValue("@Doctorname", appointment.Doctorname);
                    command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                    command.Parameters.AddWithValue("@Username", appointment.Username);
                    command.Parameters.AddWithValue("@Fullname", appointment.Fullname);
                    command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                    command.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);
                    //command.Parameters.AddWithValue("@IsConfirmed", appointment.IsConfirmed);

                    connection.Open();
                    int id = command.ExecuteNonQuery();
                    connection.Close();
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



            public List<Appointment> GetOldAppointments(int doctorId)
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    string query = "SELECT * FROM App WHERE DoctorID = @DoctorID AND AppointmentDate < @Today";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    command.Parameters.AddWithValue("@Today", DateTime.Today);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    List<Appointment> oldAppointments = new List<Appointment>();

                    while (reader.Read())
                    {
                        Appointment appointment = new Appointment
                        {
                            DoctorID = Convert.ToInt32(reader["DoctorID"]),
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            Username = reader["Username"].ToString(),
                            Fullname = reader["Fullname"].ToString(),
                            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                            AppointmentTime = TimeSpan.TryParse(reader["AppointmentTime"].ToString(), out var appointmentTime)
    ? appointmentTime
    : TimeSpan.Zero
                        };

                        oldAppointments.Add(appointment);
                    }

                    return oldAppointments;
                }
            }
        

            // Retrieve the doctor's today's appointments from the database
            public List<Appointment> GetTodaysAppointments(int doctorId)
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    string query = "SELECT * FROM App WHERE DoctorID = @DoctorID AND AppointmentDate = @Today";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    command.Parameters.AddWithValue("@Today", DateTime.Today);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    List<Appointment> todaysAppointments = new List<Appointment>();

                    while (reader.Read())
                    {
                        Appointment appointment = new Appointment
                        {
                            DoctorID = Convert.ToInt32(reader["DoctorID"]),
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            Username = reader["Username"].ToString(),
                            Fullname = reader["Fullname"].ToString(),
                            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                            AppointmentTime = TimeSpan.TryParse(reader["AppointmentTime"].ToString(), out var appointmentTime)
    ? appointmentTime
    : TimeSpan.Zero
                        };

                        todaysAppointments.Add(appointment);
                    }

                    return todaysAppointments;
                }
            }
        




            public List<Appointment> GetAllAppointments()
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("GetallAppointments", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();

                        List<Appointment> appointments = new List<Appointment>();

                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment
                            {
                               //AppID = Convert.ToInt32(reader["AppID"]),
                                DoctorID = Convert.ToInt32(reader["DoctorID"]),
                                PatientId = Convert.ToInt32(reader["PatientId"]),
                                Username = reader["Username"].ToString(),
                                Fullname = reader["Fullname"].ToString(),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                //IsConfirmed = Convert.ToBoolean(reader["IsConfirmed"]),
                                AppointmentTime = TimeSpan.TryParse(reader["AppointmentTime"].ToString(), out var appointmentTime)
        ? appointmentTime
        : TimeSpan.Zero

                            };

                            appointments.Add(appointment);
                        }

                        return appointments;
                    }
                }
            }

        public void Confirm(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                string query = "UPDATE App SET IsConfirmed = 1 WHERE App_id = @AppID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AppID", appointmentId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //Delete
        public void DeleteAppointment(int appointmentId)
        {
            using (var connection = new SqlConnection(conString))
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteAppointment", connection))
                {
                    command.Parameters.AddWithValue("@AppID", appointmentId);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Get Appointment by id

        public Appointment GetAppointmentById(int appointmentId)
        {
            using (var connection = new SqlConnection(conString))
            {
                connection.Open();

                

                using (var command = new SqlCommand("GetAppointmentbyid", connection))
                {
                    command.Parameters.AddWithValue("@AppID", appointmentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Create an appointment object based on the retrieved data
                            Appointment appointment = new Appointment
                            {
                                AppID = Convert.ToInt32(reader ["AppID"]),
                                DoctorID = Convert.ToInt32(reader["DoctorID"]),
                                PatientId = Convert.ToInt32(reader["PatientId"]),
                                Username = reader["Username"].ToString(),
                                Fullname = reader["Fullname"].ToString(),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                //IsConfirmed = Convert.ToBoolean(reader["IsConfirmed"]),
                                AppointmentTime = TimeSpan.TryParse(reader["AppointmentTime"].ToString(), out var appointmentTime)
        ? appointmentTime
        : TimeSpan.Zero
                            };

                            return appointment;
                        }
                    }
                }
            }

            return null; // Return null if the appointment is not found
        }

        public List<Appointment> GetDoctorAppointments(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                string query = "SELECT * FROM App WHERE DoctorID = @DoctorID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DoctorID", doctorId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Appointment appointment = new Appointment
                        {
                            AppID = Convert.ToInt32(reader["AppID"]),
                            DoctorID = Convert.ToInt32(reader["DoctorID"]),
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            Username = reader["Username"].ToString(),
                            Fullname = reader["Fullname"].ToString(),
                            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                            IsConfirmed = Convert.ToBoolean(reader["IsConfirmed"]),
                            AppointmentTime = TimeSpan.TryParse(reader["AppointmentTime"].ToString(), out var appointmentTime)
        ? appointmentTime
        : TimeSpan.Zero
                          
                        };

                        appointments.Add(appointment);
                    }
                }
            }

            return appointments;
        }

        public void ConfirmAppointment(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                string query = "UPDATE App SET IsConfirmed = 1 WHERE App_id = @AppID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AppID", appointmentId);

                command.ExecuteNonQuery();
            }
        }

        public AppointmentViewModel GetAppointmentViewModel(int appointmentId)
        {
            AppointmentViewModel appointmentViewModel = null;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                string query = "SELECT * FROM App WHERE App_id = @AppID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        appointmentViewModel = new AppointmentViewModel
                        {
                            AppID = Convert.ToInt32(reader["AppID"]),
                            DoctorID = Convert.ToInt32(reader["DoctorID"]),
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            Username = reader["Username"].ToString(),
                            Fullname = reader["Fullname"].ToString(),
                            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                            IsConfirmed = Convert.ToBoolean(reader["IsConfirmed"]),
                            AppointmentTime = TimeSpan.TryParse(reader["AppointmentTime"].ToString(), out var appointmentTime)
        ? appointmentTime
        : TimeSpan.Zero
                        };
                    }
                }
            }

            return appointmentViewModel;
        }
    }
}
































        /*  public bool CreateAppointment(Appointment appointment)
          {
              using (SqlConnection connection = new SqlConnection(conString))
              {
                  SqlCommand command = new SqlCommand("sp_AddAppointment", connection);
                  command.CommandType = CommandType.StoredProcedure;
                  command.Parameters.AddWithValue("@DoctorID", appointment.DoctorID);
                  command.Parameters.AddWithValue("@Patientname", appointment.Patientname);
                  command.Parameters.AddWithValue("@Registernumber", DateTime.Now);
                  command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);

                  connection.Open();
                  int Id = command.ExecuteNonQuery();
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

          public List<Appointment> GetAppointmentsByDoctorId(int doctorId)
          {
              using (SqlConnection connection = new SqlConnection(conString))
              {
                  connection.Open();


                  SqlCommand command = new SqlCommand("sp_GetallAppointment", connection);
                  command.CommandType = CommandType.StoredProcedure;
                  command.Parameters.AddWithValue("@DoctorID", doctorId);

                  SqlDataReader reader = command.ExecuteReader();

                  List<Appointment> appointments = new List<Appointment>();

                  while (reader.Read())
                  {
                      Appointment appointment = new Appointment()
                      {
                          AppointmentID = (int)reader["AppointmentID"],
                          DoctorID = (int)reader["DoctorID"],
                          Patientname = (string)reader["Patientname"],
                          Registernumber = (string)reader["Registernumber"],
                          AppointmentDate = (DateTime)reader["AppointmentDate"]
                      };

                      appointments.Add(appointment);
                  }

                  return appointments;
              }
          }

     /*     public void UpdateAppointment(Appointment appointment)
          {
              using (SqlConnection connection = new SqlConnection(conString))
              {

                  SqlCommand command = new SqlCommand("sp_UpdateApppointment", connection);
                  command.Parameters.AddWithValue("@DoctorID", appointment.DoctorID);
                  command.Parameters.AddWithValue("@Patientname", appointment.Patientname);
                  command.Parameters.AddWithValue("@Registernumber", appointment.Registernumber);
                  command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                  command.Parameters.AddWithValue("@AppointmentID", appointment.AppointmentID);

                  connection.Open();
                  command.ExecuteNonQuery();
              }
          }

          public void DeleteAppointment(int appointmentID)
          {
              using (SqlConnection connection = new SqlConnection(conString))
              {

                  SqlCommand command = new SqlCommand("sp_DeleteAppointment", connection);
                  command.Parameters.AddWithValue("@AppointmentID", appointmentID);

                  connection.Open();
                  command.ExecuteNonQuery();
              }
          }
        ============================================================================

          public List<Appointment> GetAllAppointments()
          {
              using (SqlConnection connection = new SqlConnection(conString))
              {
                  connection.Open();



                  SqlCommand command = new SqlCommand("sp_GetallAppointment", connection);
                  command.CommandType = CommandType.StoredProcedure;
                  SqlDataReader reader = command.ExecuteReader();

                  List<Appointment> appointments = new List<Appointment>();

                  while (reader.Read())
                  {
                      Appointment appointment = new Appointment()
                      {
                          AppointmentID = (int)reader["AppointmentID"],
                          DoctorID = (int)reader["DoctorID"],
                          Patientname = (string)reader["Patientname"],
                          Registernumber = (string)reader["Registernumber"],
                          AppointmentDate = (DateTime)reader["AppointmentDate"]
                      };

                      appointments.Add(appointment);
                  }

                  return appointments;
              }
          }
        */


    




