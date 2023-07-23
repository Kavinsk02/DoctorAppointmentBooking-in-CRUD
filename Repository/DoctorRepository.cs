using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using WebApplication13.Models;
using System.Security.Cryptography.Xml;

namespace WebApplication13.Repository
{
    public class DoctorRepository
    {
        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();
        //        public void AddDoctor(Doctor doctor)
        //        {
        //            using (SqlConnection connection = new SqlConnection(conString))
        //            {
        //                SqlCommand command = new SqlCommand("sp1_AddDoctor", connection);
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@DoctorID", doctor.DoctorID);
        //                command.Parameters.AddWithValue("@Doctorname", doctor.Doctorname);
        //                command.Parameters.AddWithValue("@Email", doctor.Email);
        //                command.Parameters.AddWithValue("@Phonenumber", doctor.Phonenumber);
        //                command.Parameters.AddWithValue("@Password", doctor.Password);
        //                command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
        //                command.Parameters.AddWithValue("@Image", doctor.Image);
        //               command.Parameters.AddWithValue("@ImageMimeType", doctor.ImageMimeType);

        //                connection.Open();
        //                command.ExecuteNonQuery();
        //            }
        //        }

        //        public void UpdateDoctor(Doctor doctor)
        //        {
        //            using (SqlConnection connection = new SqlConnection(conString))
        //            {
        //                SqlCommand command = new SqlCommand("sp1_UpdateDoctor", connection);
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@DoctorID", doctor.DoctorID);
        //                command.Parameters.AddWithValue("@Doctorname", doctor.Doctorname);
        //                command.Parameters.AddWithValue("@Email", doctor.Email);
        //                command.Parameters.AddWithValue("@Phonenumber", doctor.Phonenumber);
        //                command.Parameters.AddWithValue("@Password", doctor.Password);
        //                command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
        //                command.Parameters.AddWithValue("@Image", doctor.Image);
        //               command.Parameters.AddWithValue("@ImageMimeType", doctor.ImageMimeType);

        //                connection.Open();
        //                command.ExecuteNonQuery();
        //            }
        //        }

        //        public void DeleteDoctor(int doctorID)
        //        {
        //            using (SqlConnection connection = new SqlConnection(conString))
        //            {
        //                SqlCommand command = new SqlCommand("sp1_DeleteDoctor", connection);
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.Parameters.AddWithValue("@DoctorID", doctorID);

        //                connection.Open();
        //                command.ExecuteNonQuery();
        //            }
        //        }

        //        public Doctor GetDoctorById(int doctorID)
        //        {
        //            using (SqlConnection connection = new SqlConnection(conString))
        //            {
        //                SqlCommand command = new SqlCommand("sp1_SelectbyidDoctor", connection);
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.Parameters.AddWithValue("@DoctorID", doctorID);

        //                connection.Open();
        //                SqlDataReader reader = command.ExecuteReader();

        //                if (reader.Read())
        //                {
        //                    Doctor doctor = new Doctor();
        //                    doctor.DoctorID = (int)reader["DoctorID"];
        //                    doctor.Doctorname = (string)reader["Name"];
        //                    doctor.Email = (string)reader["Email"];
        //                    doctor.Phonenumber = (string)reader["Phonenumber"];
        //                    doctor.Password = (string)reader["Password"];
        //                    doctor.Image = (byte[])reader["Image"];
        //                    doctor.ImageMimeType = (string)reader["ImageMimeType"];
        //                    return doctor;
        //                }

        //                return null;
        //            }

        //        }
        //        public List<Doctor> GetAllDoctors()
        //        {
        //            List<Doctor> doctors = new List<Doctor>();

        //            using (SqlConnection connection = new SqlConnection(conString))
        //            {
        //                SqlCommand command = new SqlCommand("sp1_SelectDoctor", connection);
        //                command.CommandType = CommandType.StoredProcedure;

        //                connection.Open();
        //                SqlDataReader reader = command.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    Doctor doctor = new Doctor();
        //                    doctor.DoctorID = (int)reader["DoctorID"];
        //                    doctor.Doctorname = (string)reader["Name"];
        //                    doctor.Email = (string)reader["Email"];
        //                    doctor.Phonenumber = (string)reader["Phonenumber"];
        //                    doctor.Password = (string)reader["Password"];
        //                    doctor.Image = (byte[])reader["Image"];
        //                    doctor.ImageMimeType = (string)reader["ImageMimeType"];

        //                    doctors.Add(doctor);
        //                }
        //            }

        //            return doctors;
        //        }
        //    }
        //}

        public bool IsDuplicateAppointment(int DoctorID)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Doctor_details WHERE DoctorID=@DoctorID", connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", DoctorID);
                    


                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }


        //Get all the User Details
        public List<Doctor> GetallDoctor()
        {
            List<Doctor> doctorlist = new List<Doctor>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_SelectDoctor";
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                connection.Open();
                da.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                    doctorlist.Add(
                        new Doctor
                        {
                            DoctorID = Convert.ToInt32(dr["DoctorID"]),
                            Doctorname = Convert.ToString(dr["Doctorname"]),
                            Email = Convert.ToString(dr["Email"]),
                            Phonenumber = Convert.ToString(dr["Phonenumber"]),
                            Password = Convert.ToString(dr["Password"]),
                            Specialization = Convert.ToString(dr["Specialization"])

                        });

                return doctorlist;
            }
        }

        // Create a New user

        public bool AddDoctor(Doctor doctor)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                Password EncryptData = new Password();
                SqlCommand command = new SqlCommand("sp_CreateDoctor", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DoctorID", doctor.DoctorID);
                command.Parameters.AddWithValue("@Doctorname", doctor.Doctorname);
                command.Parameters.AddWithValue("@Email", doctor.Email);
                command.Parameters.AddWithValue("@Phonenumber", doctor.Phonenumber);
                command.Parameters.AddWithValue("@Password", EncryptData.Encode(doctor.Password));
                command.Parameters.AddWithValue("@Specialization", doctor.Specialization);


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



        // Get a Selected User Details


        public Doctor GetDoctorById(int id)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {


                using (SqlCommand command = new SqlCommand("sp_SelectDoctorbyid", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DoctorID", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Doctor doctor = new Doctor
                        {
                            DoctorID = Convert.ToInt32(reader["DoctorID"]),
                            Doctorname = reader["Doctorname"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phonenumber = reader["Phonenumber"].ToString(),
                            Password = reader["Password"].ToString(),
                            Specialization = reader["Specialization"].ToString()

                        };

                        return doctor;
                    }
                }
            }

            return null;
        }





        public bool UpdateDoctor(Doctor doctor)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateDoctor", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Doctorname", doctor.Doctorname);
                command.Parameters.AddWithValue("@Email", doctor.Email);
                command.Parameters.AddWithValue("@Phonenumber", doctor.Phonenumber);
                command.Parameters.AddWithValue("@Password", doctor.Password);
                command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        // Delete the user details

        public string DeleteDoctor(int id)
        {

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteDoctor", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DoctorID", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                return connection.ConnectionString;
            }

        }

        internal bool IsUsernameExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}