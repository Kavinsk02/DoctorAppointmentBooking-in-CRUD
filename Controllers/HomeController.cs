using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication13.Models;
using WebApplication13.Repository;

namespace WebApplication13.Controllers
{
    public class HomeController : Controller
    {
        string conString = ConfigurationManager.ConnectionStrings["DBconnect"].ToString();
        UserDBcontext dbcontext = new UserDBcontext();
        HomeRepository hr = new HomeRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
     
        

        // GET: Contact/Create
        public ActionResult Contact()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                hr.AddContactUsMessage(contact);
                return RedirectToAction("ThankYou");
            }

            return View(contact);
        }
    
        public ActionResult ThankYou()
        {
            return View();
        }
        public ActionResult Messages()
        {
            var messages = hr.GetAllContactUsMessages();
            return View(messages);
        }

        // GET: Contact/ThankYou



        public ActionResult AdminHomepage()
        {
            return View();
        }
        public ActionResult DoctorHomepage()
        {
            return View();
        }
        public ActionResult PatientHomepage()
        {
            return View();
        }
    }
}