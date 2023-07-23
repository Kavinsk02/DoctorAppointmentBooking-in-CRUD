using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication13.Models
{
    public class Attendance
    {
        public int id { get; set; }
        public string DoctorID { get; set; }
        public DateTime AttendanceDate { get; set; }
        [DefaultValue(false)]
        public bool Ispresent { get; set; }
    }
}