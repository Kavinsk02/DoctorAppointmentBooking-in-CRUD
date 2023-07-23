using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication13.Models
{
    public class Doctor
    {

        //    public int DoctorID { get; set; }
        //    public string Doctorname { get; set; }
        //    public string Email { get; set; }
        //public string Phonenumber { get; set; }
        //public string Password { get; set; }
        //public string Specialization { get; set; }
        //    public byte[] Image { get; set; }
        //    public string ImageMimeType { get; set; }
        //}




        [Key]
        public int DoctorID { get; set; }


        [Required(ErrorMessage = "Enter Your Name")]
        [Display(Name = "Doctorname")]
        public string Doctorname { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Your EmailID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]

        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Phonenumber { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Your Specialization")]
        [Display(Name = "Specialization")]
        public string Specialization { get; set; }


    }

        //}

        //Doctor login

        public class DoctorSignin
    {
        [Display(Name = "DoctorID")]
        [Required(ErrorMessage = "Enter Your DoctorID")]

        public int DoctorID { get; set; }



        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]

        public string Password { get; set; }

    }



}