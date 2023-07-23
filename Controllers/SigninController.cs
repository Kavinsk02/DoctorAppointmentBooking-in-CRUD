using Microsoft.AspNetCore.Http.Features.Authentication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;
using WebApplication13.Repository;

namespace WebApplication13.Controllers
{
    public class SigninController : Controller
    {
        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();
        SigninRepository sr = new SigninRepository();
        AdminRepository ar = new AdminRepository();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model, string userRole)
        {
            bool isValid = false;

            if (userRole == "Admin")
            {
                Session["Username"] = model.Username;
                isValid = sr.ValidateAdmin(model.Username, model.Password);
            }
            else if (userRole == "User")
            {
                Session["Username"] = model.Username;
                Session["PatientId"] = model.Id;
                isValid = sr.ValidateUser(model.Id, model.Username, model.Password);
            }

            if (isValid)
            {
                
                if (userRole == "Admin")
                {
                    
                    return RedirectToAction("AdminHomepage", "Home");
                }
                else if (userRole == "User")
                {
                 
                    return RedirectToAction("PatientHomepage", "Home");
                }
            }

           
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }




        /*  if (ar.GetAdminByUsername(model.Username, model.Password))
          {

              return RedirectToAction("AdminHomepage","Home");
          }


          if (sr.GetUserByUsername(model.Username, model.Password))
          {

              return RedirectToAction("Patienthomepage","Home");
          }


          ModelState.AddModelError("", "Invalid username or password.");
      }


      return View(model);*/


        /* private SigninRepository signinrepository;
         private AdminRepository adminrepository;


         public SigninController()
         {
             signinrepository = new SigninRepository();
             adminrepository = new AdminRepository();

         }
         public ActionResult Signin()
         {
             return View();
         }
         [HttpPost]
         public ActionResult Signin(Signin signin)
         {
             try
             {
                 if (ModelState.IsValid)
                 {
                     bool isSigninAuthenticated = signinrepository.GetUserByUsername(signin);
                     bool isAdminAuthenticated = adminrepository.GetAdminByUsername(signin);

                     if (isSigninAuthenticated)
                     {
                         TempData["Login"] = "<script>alert('User signin Successful')</script>";
                         return RedirectToAction("Index", "Signup");
                     }
                     else if (isAdminAuthenticated)
                     {
                         TempData["Login"] = "<script>alert('Admin signin Successful')</script>";
                         return RedirectToAction("AdminHomepage", "Home");
                     }
                     else
                     {
                         ViewBag.Message = "Invalid Username and Password!";
                         return View(signin);
                     }
                 }
             }
             catch (Exception ex)
             {

                 Errorlog.LogError(ex);
             }

             return View(signin);
         }*/

        public ActionResult DoctorLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult DoctorLogin(DoctorSignin model)
        {
            Password EncryptData = new Password();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                
                connection.Open();
                string query = "Select DoctorID,Password from Doctor_details where DoctorID=@DoctorID and Password=@Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DoctorID", model.DoctorID);
                command.Parameters.AddWithValue("@Password", model.Password);

                SqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    Session["DoctorID"] = model.DoctorID;
                  
                    return RedirectToAction("DoctorHomepage", "Home");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Username and password failed";
                }
 
                connection.Close();
                return View();
            }

        }
    }
}


       
    
