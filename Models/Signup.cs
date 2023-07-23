using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication13.Models
{
    public class Signup
    {
        public int PatientId { get; set; }
        
        [Required(ErrorMessage = "Enter Your Full name")]
        [Display(Name = "Full name")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Enter Your Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Your EmailID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]

        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter Your DOB.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateofBirth { get; set; }
        
       
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        
        public string Password { get; set; }
        
      
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please Enter Your Address")]
        public string Address { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please Enter Your State")]
        public string State { get; set; }
        [Display(Name="District")]
        [Required(ErrorMessage = "Please Enter Your District")]
        public string District { get; set; }
    }


    //Contact


    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Your EmailID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter Your Date.")]
        public DateTime Date { get; set; }


        [Display(Name = "Message")]
        [Required(ErrorMessage = "Enter Your Meassage.")]
        public string Message { get; set; }
    }



    // Signin

    public class Signin
    {

        [Required(ErrorMessage = "Enter Your Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]

        public string Password { get; set; }

        public string UserRole { get; set; }
       

    }
    //Forgot Password
    public class Forgotpassword
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Your EmailID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]

        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Your Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the Password")]
        
        public string Password { get; set; }
        [Display(Name = "UserRole")]
        [Required(ErrorMessage = "Please Select the required field")]
        public string UserRole { get; set; }
    }
}