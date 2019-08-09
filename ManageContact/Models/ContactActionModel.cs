using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageContact.Models
{
    public class ContactActionModel
    {
        [Key]
        public int IDContact { get; set; }


        [Display(Name = "Contact Name")]
        [Required(ErrorMessage = "Please enter contact name.")]
        public string ContactName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter contact name.")]
        public int PhoneNumber { get; set; }

        [Display(Name = "Network")]
        [Required(ErrorMessage = "Please choose network operator.")]
        public int? IDNetworkOperator { get; set; }

        [Display(Name = "Address")]
        //[Required(ErrorMessage = "Please choose address.")]
        public int? IDAddress { get; set; }

        [Display(Name = "Position")]
        //[Required(ErrorMessage = "Please choose position.")]
        public int? IDPosition { get; set; }


    }
}