using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageContact.Models
{
    public class CustomerModel
    {
        [Key]
        public int IDCustomer { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please choose gender")]
        public bool Gender { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter phone number")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter address email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }

        [Display(Name = "Quantity")]
        public int ContactQuantity { get; set; }

    }
}