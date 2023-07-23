using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;
using WebApplication13.Repository;


namespace WebApplication13.Controllers
{
    public class AppointmentController : Controller
    {
        AppointmentRepository ar=new AppointmentRepository();
        DoctorRepository dr = new DoctorRepository();
        UserDBcontext db = new UserDBcontext();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Search(string specialization)
        {
            if (!string.IsNullOrEmpty(specialization))
            {
                var doctors = ar.GetDoctorsBySpecialization(specialization);
                return View(doctors);
            }
            return View();
        }
        [HttpGet]
        public ActionResult BookAppointment(int doctorId)
        {
            Doctor doctor = dr.GetDoctorById(doctorId);
            if (doctor == null)
            {
                return RedirectToAction("Index");
            }

            Appointment appointment = new Appointment
            {
                DoctorID = doctorId,
                Doctorname = doctor.Doctorname
                // Other appointment properties
            };

            return View(appointment);
        }


        [HttpPost]
        public ActionResult BookAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                
                int patientId = (int)Session["PatientId"];
                string Username = (string)Session["Username"];
                appointment.PatientId = patientId;
                appointment.Username = Username;
               
                ar.BookAppointment(appointment);
                return RedirectToAction("AppointmentConfirmation", new { doctorId = appointment.DoctorID,patientId=appointment.PatientId, patientname=appointment.Fullname,username = appointment.Username, appointmentDate = appointment.AppointmentDate, appointmentTime = appointment.AppointmentTime });
            }

            // If model state is invalid, return to the appointment booking form
            return View(appointment);
        }

        public ActionResult AppointmentConfirmation(int doctorId,int patientId,string username, string patientname, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            Doctor doctor = dr.GetDoctorById(doctorId);
            if (doctor == null)
            {
                return RedirectToAction("PatientHomepage", "Home");
            }

            AppointmentConfirmation viewModel = new AppointmentConfirmation
            {
                DoctorID = doctor.DoctorID,
                Doctorname = doctor.Doctorname,
                Specialization = doctor.Specialization,
                PatientId=patientId,
                Fullname = patientname,
                Username=username,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointmentTime
            };

            return View(viewModel);
        }

        //public ActionResult GetAppointments(int doctorID)
        //{
        //   // DateTime currentDate = DateTime.Today;
        //    List<Appointment> appointments = ar.GetAppointments(doctorID);
        //        return View(appointments);

        //}
        public ActionResult OldAppointments(int doctorId)
        {
           
            List<Appointment> oldAppointments = ar.GetOldAppointments(doctorId);

            return View(oldAppointments);
        }

        public ActionResult TodaysAppointments(int doctorId)
        {
            
            List<Appointment> todaysAppointments = ar.GetTodaysAppointments(doctorId);

            return View(todaysAppointments);
        }

            public ActionResult GetAllAppointments()
        {
            List<Appointment> appointments = ar.GetAllAppointments(); 

            return View(appointments);
        }


       


        //Admin

        public ActionResult AdminSearch(string specialization)
        {
            if (!string.IsNullOrEmpty(specialization))
            {
                var doctors = ar.GetDoctorsBySpecialization(specialization);
                return View(doctors);
            }
            return View();
        }
        [HttpGet]
        public ActionResult AdminBookAppointment(int doctorId)
        {
            Doctor doctor = dr.GetDoctorById(doctorId);
            if (doctor == null)
            {
                return RedirectToAction("Index");
            }

            Appointment appointment = new Appointment
            {
                DoctorID = doctorId,
                Doctorname = doctor.Doctorname
                // Other appointment properties
            };

            return View(appointment);
        }


        [HttpPost]
        public ActionResult AdminBookAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                ar.BookAppointment(appointment);
                return RedirectToAction("AdminAppointmentConfirmation", new { doctorId = appointment.DoctorID, patientId = appointment.PatientId, patientname = appointment.Fullname, username = appointment.Username, appointmentDate = appointment.AppointmentDate, appointmentTime = appointment.AppointmentTime });
            }

            // If model state is invalid, return to the appointment booking form
            return View(appointment);
        }

        public ActionResult AdminAppointmentConfirmation(int doctorId, int patientId, string username, string patientname, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            Doctor doctor = dr.GetDoctorById(doctorId);
            if (doctor == null)
            {
                return RedirectToAction("PatientHomepage", "Home");
            }

            AppointmentConfirmation viewModel = new AppointmentConfirmation
            {
                DoctorID = doctor.DoctorID,
                Doctorname = doctor.Doctorname,
                Specialization = doctor.Specialization,
                PatientId = patientId,
                Fullname = patientname,
                Username = username,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointmentTime
            };

            return View(viewModel);
        }


        //PatientID

        public ActionResult ViewPatientappointment(int PatientId)
        {
            //int PatientId = (int)Session["PatientId"];
           
            var appointments =ar. GetAppointmentsByPatientId(PatientId);
            return View(appointments);
        }


        //Confirm and Cancel

        //public ActionResult Confirm(int appointmentId)
        //{
        //    ar.ConfirmAppointment(appointmentId);
        //    return Json(new { success = true });
        //}
        public ActionResult Confirm(int appointmentId)
        {
            ar.Confirm(appointmentId);
            return RedirectToAction("DoctorHomepage","Home");
        }

        [HttpPost]
        public ActionResult Cancel(int appointmentId)
        {
            ar.CancelAppointment(appointmentId);
            return Json(new { success = true });
        }


        //Delete

        public ActionResult Delete(int appointmentId)
        {
            
            Appointment appointment = ar.GetAppointmentById(appointmentId);

            if (appointment == null)
            {
                
                return HttpNotFound();
            }

            return View(appointment);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int appointmentId)
        {
            
            ar.DeleteAppointment(appointmentId);

           
            return RedirectToAction("AdminHomepage","Home");
        }

        //

        public ActionResult Appointments(int doctorId)
        {
            List<Appointment> appointments = ar.GetDoctorAppointments(doctorId);
            return View(appointments);
        }

        public ActionResult ConfirmAppointment(int appointmentId)
        {
            ar.ConfirmAppointment(appointmentId);
            return RedirectToAction("Appointments");
        }
    }
}
