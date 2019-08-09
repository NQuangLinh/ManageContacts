using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ManageContact.Models
{
    public class ContactsModel
    {
        [Key]
        public int IDContact { set; get; }

        [Display(Name = "Contact Name")]
        public string ContactName { set; get; }

        [Display(Name = "Phone Number")]
        public int PhoneNumber { set; get; }



        [Display(Name = "Network")]
        public string NetworkOperatorName { set; get; }

        [Display(Name = "Address")]
        public string AddressName { set; get; }

        [Display(Name = "Position")]
        public string PositionName { set; get; }

        [Display(Name = "Update Time")]
        public DateTime? UpdateTime { set; get; }
    }
}