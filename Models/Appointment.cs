using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication13.Models
{



    public class Appointment
    {
        public int AppID { get; set; }
        public int DoctorID { get; set; }
        public string Doctorname { get; set; }
        public int PatientId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Time is required")]
        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        public TimeSpan AppointmentTime { get; set; }

        public string Status { get; set; }
        public bool IsConfirmed { get; set; }

    }
    public class AppointmentConfirmation
    {
        
        public int DoctorID { get; set; }
        public string Doctorname { get; set; }
        public string Specialization { get; set; }
        public int PatientId { get; set; }
        
        public string Fullname { get; set; }
        public string Username { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        
    }

    public class AppointmentViewModel
    { 
        public int AppID { get; set; }
        public int DoctorID { get; set; }
        public string Doctorname { get; set; }
        public string Specialization { get; set; }
        public int PatientId { get; set; }

        public string Fullname { get; set; }
        public string Username { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        public bool IsConfirmed { get; set; }
    }




    /*[Key]
    public int AppointmentID { get; set; }
   public int Doctorname { get; set; }
    [Required(ErrorMessage = "Enter Your Name")]
    [Display(Name = "Patientname")]
    public string Patientname { get; set; }

    [DataType(DataType.DateTime)]
    [Required(ErrorMessage = "Enter Your Date.")]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime AppointmentDate { get; set; }
    */






}