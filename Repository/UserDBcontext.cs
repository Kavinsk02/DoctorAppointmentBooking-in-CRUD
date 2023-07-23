using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication13.Models;
using System.Dynamic;
using System.Web.Mvc;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Collections;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;

using System.Security.Cryptography;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography.Xml;

namespace WebApplication13.Repository
{
    public class UserDBcontext
    {

        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();

        public bool IsUsernameExists(Signup signup)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("Select * From Signup where Usernmae=@Username", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username",signup.Username);
   

                    connection.Open();

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    

        //Get all the User Details
        public List<Signup> GetallUser()
        {
            List<Signup> signuplist = new List<Signup>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_allPatient";
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                connection.Open();
                da.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                    signuplist.Add(
                        new Signup
                        {
                            PatientId = Convert.ToInt32(dr["PatientId"]),
                            Fullname = Convert.ToString(dr["Fullname"]),
                           
                            Username = Convert.ToString(dr["Username"]),
                            Email = Convert.ToString(dr["Email"]),
                            Phonenumber = Convert.ToString(dr["Phonenumber"]),
                            Gender = Convert.ToString(dr["Gender"]),
                            DateofBirth = Convert.ToDateTime(dr["DateofBirth"]),
                            Password = Convert.ToString(dr["Password"]),
                           
                            Address = Convert.ToString(dr["Address"]),
                            State = Convert.ToString(dr["State"]),
                            District = Convert.ToString(dr["District"])

                        });

                return signuplist;
            }
        }


        public bool IsDuplicateAppointment(string Username,string Phonenumber)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Patient WHERE Username=@Username And Phonenumber=@Phonenumber", connection))
                {
                    command.Parameters.AddWithValue("@Username",Username);
                    command.Parameters.AddWithValue("@Phonenumber", Phonenumber);


                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        // Create a New user

        public bool AddUser(Signup sign)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                Password EncryptData = new Password();
                SqlCommand command = new SqlCommand("Add_Patient1", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PatientId", sign.PatientId);
                command.Parameters.AddWithValue("@Fullname", sign.Fullname);

                command.Parameters.AddWithValue("@Username", sign.Username);
                command.Parameters.AddWithValue("@Email", sign.Email);
                command.Parameters.AddWithValue("@Phonenumber", sign.Phonenumber);
                command.Parameters.AddWithValue("@Gender", sign.Gender);
                command.Parameters.AddWithValue("@DateofBirth", sign.DateofBirth);
                command.Parameters.AddWithValue("@Password",EncryptData.Encode(sign.Password));
                
                command.Parameters.AddWithValue("@Address", sign.Address);
                command.Parameters.AddWithValue("@State", sign.State);
                command.Parameters.AddWithValue("@District", sign.District);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id >0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

      

        // Get a Selected User Details


        public List<Signup> GetaUserById(int PatientId)
        {
            List<Signup> signuplist = new List<Signup>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_Patientbyid";
                command.Parameters.AddWithValue("@PatientId", PatientId);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                connection.Open();
                da.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                    signuplist.Add(
                        new Signup
                        {
                            PatientId = Convert.ToInt32(dr["PatientId"]),
                            Fullname = Convert.ToString(dr["Fullname"]),

                            Username = Convert.ToString(dr["Username"]),
                            Email = Convert.ToString(dr["Email"]),
                            Phonenumber = Convert.ToString(dr["Phonenumber"]),
                            Gender = Convert.ToString(dr["Gender"]),
                            DateofBirth = Convert.ToDateTime(dr["DateofBirth"]),
                            Password = Convert.ToString(dr["Password"]),

                            Address = Convert.ToString(dr["Address"]),
                            State = Convert.ToString(dr["State"]),
                            District = Convert.ToString(dr["District"])

                        });

                return signuplist;
            }
        }
        //


        public Signup GetPatientById(int PatientId)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {


                using (SqlCommand command = new SqlCommand("Get_Patientbyid", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PatientId", PatientId);
                    
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Signup patient = new Signup
                        {
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            Fullname = reader["Fullname"].ToString(),
                        
                            Email = reader["Email"].ToString(),
                            Username = reader["Username"].ToString(),
                            

                        };

                        return patient;
                    }
                }
            }

            return null;
        }


        // Update the user Details


        public bool UpdateUser(Signup sign)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("Update_Patient", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PatientId", sign.PatientId);
                command.Parameters.AddWithValue("@Fullname", sign.Fullname);

                command.Parameters.AddWithValue("@Username", sign.Username);
                command.Parameters.AddWithValue("@Email", sign.Email);
                command.Parameters.AddWithValue("@Phonenumber", sign.Phonenumber);
                command.Parameters.AddWithValue("@Gender", sign.Gender);
                command.Parameters.AddWithValue("@DateofBirth", sign.DateofBirth);
                command.Parameters.AddWithValue("@Password",sign.Password);

                command.Parameters.AddWithValue("@Address", sign.Address);
                command.Parameters.AddWithValue("@State", sign.State);
                command.Parameters.AddWithValue("@District", sign.District);

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

        public string DeleteUser(int id)
        {

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("Delete_Patient", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PatientId", id);

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


       







        //Encrypt

        /* public class EncryptionHelper
         {
             public static string Encrypt(string plainText)
             {
                 StringBuilder encryptedText = new StringBuilder();
                 foreach (char c in plainText)
                 {
                     char encryptedChar = (char)(c + 3);
                     encryptedText.Append(encryptedChar);
                 }
                 return encryptedText.ToString();
             }

             //Decrypt
             public static string Decrypt(string plainText)
             {
                 StringBuilder decryptedText = new StringBuilder();
                 foreach (char c in plainText)
                 {
                     char decryptedChar = (char)(c - 3);
                     decryptedText.Append(decryptedChar);
                 }
                 return decryptedText.ToString();
             }
         }



         private static string Encrypt(string clearText)
         {
             string encryptionKey = "MAKV2SPBNI99212";
             byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
             using (Aes encryptor = Aes.Create())
             {
                 Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                 encryptor.Key = pdb.GetBytes(32);
                 encryptor.IV = pdb.GetBytes(16);
                 using (MemoryStream ms = new MemoryStream())
                 {
                     using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                     {
                         cs.Write(clearBytes, 0, clearBytes.Length);
                         cs.Close();
                     }
                     clearText = Convert.ToBase64String(ms.ToArray());
                 }
             }
             return clearText;
         }

         private static string Decrypt(string cipherText)
         {
             string encryptionKey = "MAKV2SPBNI99212";
             byte[] cipherBytes = Convert.FromBase64String(cipherText);
             using (Aes encryptor = Aes.Create())
             {
                 Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                 encryptor.Key = pdb.GetBytes(32);
                 encryptor.IV = pdb.GetBytes(16);
                 using (MemoryStream ms = new MemoryStream())
                 {
                     using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                     {
                         cs.Write(cipherBytes, 0, cipherBytes.Length);
                         cs.Close();
                     }
                     cipherText = Encoding.Unicode.GetString(ms.ToArray());
                 }
             }
             return cipherText;
         }


         */



    }
}

