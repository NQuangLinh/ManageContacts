using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageContact.Models
{
    public class AdminModel
    {
        [Key]
        public int IDAccount { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Please choose gender")]
        public bool Gender { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your phone number")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your address email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }


        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length is at least 6 characters.")]
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm password is incorrect")]
        public string ConfirmPassword { get; set; }
    }
}