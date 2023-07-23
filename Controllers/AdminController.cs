using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication13.Models;
using WebApplication13.Repository;

namespace WebApplication13.Controllers
{
    public class AdminController : Controller
    {
       

        // GET: Admin
        public ActionResult AdminDashboard()
        {
            return View();
        }

        //Admin


        [HttpGet]

       

        public ActionResult UpdateAdmin(Admin admin)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (ModelState.IsValid)
                {
                    bool IsUpdated = adminRepository.UpdateAdmin(admin);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Details Updated Successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Name is already available/Unable to update";
                    }
                    return RedirectToAction("AdminDashboard");
                }

                return View();
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

    }
}
