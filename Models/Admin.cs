using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication13.Models
{
    public class Admin
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Enter Your Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the Password")]
        public string Password { get; set; }    
        public string UserRole { get; set; }
    }

    //Admin Dashboard

    public class Changepassword
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        internal bool ChangePassword(string name, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
