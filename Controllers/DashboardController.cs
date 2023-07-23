using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Repository;

namespace WebApplication13.Controllers
{
    public class DashboardController : Controller
    {
        DashboardRepository dr = new DashboardRepository();
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult GetUserCount()
        {
            int userCount = dr.GetRegisteredUserCount();
            return Json(userCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDoctorCount()
        {
            int doctorCount = dr.GetRegisteredDoctorCount();
            return Json(doctorCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAppointmentCount()
        {
            int appointmentCount = dr.GetAppointmentCount();
            return Json(appointmentCount, JsonRequestBehavior.AllowGet);
        }

    }
}
