using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;
using WebApplication13.Repository;

namespace WebApplication13.Controllers
{
    public class DoctorController : Controller
    {
        DoctorRepository dr = new DoctorRepository();
        //public ActionResult DCreate()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult DCreate(Doctor doctor, HttpPostedFileBase imageFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (imageFile != null && imageFile.ContentLength > 0)
        //        {
        //            doctor.ImageMimeType = imageFile.ContentType;
        //            doctor.Image = new byte[imageFile.ContentLength];
        //            imageFile.InputStream.Read(doctor.Image, 0, imageFile.ContentLength);
        //        }

        //        dr.AddDoctor(doctor);
        //        return RedirectToAction("DoctorHomepage", "Home");
        //    }

        //    return View(doctor);
        //}

        //[HttpPost]
        //public ActionResult Edit(Doctor doctor, HttpPostedFileBase imageFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (imageFile != null && imageFile.ContentLength > 0)
        //        {
        //            doctor.ImageMimeType = imageFile.ContentType;
        //            doctor.Image = new byte[imageFile.ContentLength];
        //            imageFile.InputStream.Read(doctor.Image, 0, imageFile.ContentLength);
        //        }

        //        dr.UpdateDoctor(doctor);
        //        return RedirectToAction("DoctorHomepage","Home");
        //    }

        //    return View(doctor);
        //}


        public ActionResult Index()
        {
            var ns = dr.GetallDoctor();
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
        public ActionResult Create(Doctor doctor,int DoctorID)
        {
            bool IsInserted = false;
            // string filepath = @"E:\\Logfile\\log.txt";
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {

                if (ModelState.IsValid)
                {

                    
                    if (dr.IsDuplicateAppointment(DoctorID))
                    {
                        TempData["ErrorMessage"] = "Appointment with the same ID already exists.";
                        return View(DoctorID);
                    }
                    IsInserted = dr.AddDoctor(doctor);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Save Data";
                    }
                }
                return RedirectToAction("DoctorLogin", "Signin");

            }
            catch (Exception ex)
            {
                Errorlog.LogError(ex);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public ActionResult AdminCreate()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AdminCreate(Doctor doctor)
        {
            bool IsInserted = false;
            // string filepath = @"E:\\Logfile\\log.txt";
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {

                if (ModelState.IsValid)
                {

                    IsInserted = dr.AddDoctor(doctor);
                   
                   if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Save Data";
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

        public ActionResult Edit(int id)
        {

            
            return View(dr.GetallDoctor().Find(User => User.DoctorID == id));
        }
        [HttpPost]
        public ActionResult Edit(int id,Doctor doctor)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(dr.UpdateDoctor(doctor))
                    {
                        ViewBag.Message = "Doctor record updated successfully";
                    }
                    
                }
                return RedirectToAction("DoctorHomepage", "Home");
            }
            catch(Exception ex)
            {
                Errorlog.LogError(ex);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult AdminEdit(int id)
        {


            return View(dr.GetallDoctor().Find(User => User.DoctorID == id));
        }
        [HttpPost]
        public ActionResult AdminEdit(int id, Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dr.UpdateDoctor(doctor))
                    {
                        ViewBag.Message = "Doctor record updated successfully";
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

        public ActionResult Delete(int id)
        {
            try
            {
                var signup = dr.GetDoctorById(id);
                if (signup == null)
                {
                    TempData["InfoMessage"] = "Name not available with Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(signup);
            }
            catch (Exception ex)
            {

                Errorlog.LogError(ex);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = dr.DeleteDoctor(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = "Details Deleted Successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Details not availabel";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                Errorlog.LogError(ex);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        public ActionResult AdminDelete(int id)
        {
            try
            {
                var signup = dr.GetDoctorById(id);
                if (signup == null)
                {
                    TempData["InfoMessage"] = "Name not available with Id" + id.ToString();
                    return RedirectToAction("AdminHomepage","Home");
                }
                return View(signup);
            }
            catch (Exception ex)
            {

                Errorlog.LogError(ex);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult AdminDeleteConfirmation(int id)
        {
            try
            {
                string result = dr.DeleteDoctor(id);
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

                Errorlog.LogError(ex);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        //dashboard


    }
}