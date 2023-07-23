using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;
using WebApplication13.Repository;

namespace WebApplication13.Controllers
{
    public class AttendanceController : Controller
    {
        AttendanceRepository Ar = new AttendanceRepository();

        public ActionResult CreateAttendance()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAttendance(Attendance attendance)
        {
            
                if (ModelState.IsValid)
                {

                    Ar.AddAttendance(attendance);
                    return RedirectToAction("Attendancedetails");
                }
                
            
            return View();
        }

        public ActionResult Attendancedetails()
        {
            var ns = Ar.GetAttendanceDetails();
            if (ns.Count == 0)
            {
                TempData["InfoMessage"] = "Currently name is not Available in Database...";
            }
            return View(ns);
        }
       
    }
}
