


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages.Html;
using WebApplication13.Models;
using WebApplication13.Repository;
using NLog;
using System.Data.Entity;

namespace WebApplication13.Controllers
{
    public class SignupController : Controller
    {
        UserDBcontext dbcontext = new UserDBcontext();

        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Index()
        {
            var ns = dbcontext.GetallUser();
            if (ns.Count == 0)
            {
                TempData["InfoMessage"] = "Currently name is not Available in Database...";
            }
            return View(ns);

        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Signup signup, string Username, string Phonenumber)
        {
            
            
            try
            {
               
                if (ModelState.IsValid)
                {
                    if (dbcontext.IsDuplicateAppointment(Username, Phonenumber))
                    {
                        TempData["ErrorMessage"] = "Appointment with the same ID already exists.";
                        return View(Username,Phonenumber);
                    }
                    bool IsInserted = dbcontext.AddUser(signup);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Save Data";
                    }
                }
                return RedirectToAction( "Index","Signin");
               
            }
            catch(Exception ex)
            {
                Errorlog.LogError(ex);
               TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {


            return View(dbcontext.GetallUser().Find(User => User.PatientId == id));
        }
        [HttpPost]
        public ActionResult Edit(int id, Signup signup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dbcontext.UpdateUser(signup))
                    {
                        ViewBag.Message = "Doctor record updated successfully";
                    }

                }
                return RedirectToAction("PatientHomepage", "Home");
            }
            catch (Exception ex)
            {
                Errorlog.LogError(ex);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        public ActionResult AdminEdit(int id)
        {


            return View(dbcontext.GetallUser().Find(User => User.PatientId == id));
        }
        [HttpPost]
        public ActionResult AdminEdit(int id, Signup signup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dbcontext.UpdateUser(signup))
                    {
                        ViewBag.Message = "User record updated successfully";
                    }

                }
                return RedirectToAction("AdminHomepage", "Home");
            }
            catch (Exception ex)
            {
                Errorlog.LogError(ex);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

    //    public ActionResult Delete(int id)
    //    {

    //        var signup = dbcontext.GetaUserById(id).FirstOrDefault();
    //        if (signup == null)
    //        {
    //            TempData["InfoMessage"] = "Name not available with Id" + id.ToString();
    //            return RedirectToAction("Index");
    //        }
    //        return View(signup);
    //    }

    //}
    //[HttpPost, ActionName("Delete")]
    public ActionResult Delete(int id)
        {
            try
            {
                string result = dbcontext.DeleteUser(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = "Details Deleted Successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Details not availabel";
                }
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }
        //public ActionResult AdminDelete(int id)
        //{
        //    try
        //    {
        //        var signup = dbcontext.GetaUserById(id).FirstOrDefault();
        //        if (signup == null)
        //        {
        //            TempData["InfoMessage"] = "Name not available with Id" + id.ToString();
        //            return RedirectToAction("Index");
        //        }
        //        return View(signup);
        //    }
        //    catch (Exception ex)
        //    {

        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }
        //}
        [HttpPost, ActionName("Delete")]
        public ActionResult AdminDeleteConfirmation(int id)
        {
            try
            {
                string result = dbcontext.DeleteUser(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = "Details Deleted Successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Details not availabel";
                }
                return RedirectToAction("AdminHomepage","Home");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        public ActionResult Details(int id)
        {
            try
            {
                var signup = dbcontext.GetaUserById(id).FirstOrDefault();
                if (signup == null)
                {
                    TempData["InfoMessage"] = "Name not available with Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(signup);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

       

    }
}


