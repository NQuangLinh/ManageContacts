using ManageContact.Dao;
using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageContact.Controllers
{
    public class AddressController : BaseController
    {
        //Address
        public ActionResult Address()
        {
            int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
            var addressDAO = new AddressDAO();
            IEnumerable<Address> listAddress = addressDAO.getAddress(idCustomer);
            IEnumerable<Address> listAddressDisplay = addressDAO.getAddressDisplay(listAddress, idCustomer); 
            return View(listAddressDisplay);
        }


        [HttpGet]
        public ActionResult CreateAddress()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateAddress(Address model)
        {
            if (ModelState.IsValid)
            {
                var addressDAO = new AddressDAO();
                addressDAO.insertAddress(model,Int32.Parse(Session["IDCustomer"].ToString()));
                return RedirectToAction("Address", "Address");
            }
            ModelState.AddModelError("", "Please enter the address name");
            return View();
        }


        [HttpGet]
        public ActionResult EditAddress(int id)
        {
                var addressDAO = new AddressDAO();
                Address address = addressDAO.getAddressEdit(id);
                return View(address);


        }


        [HttpPost]
        public ActionResult EditAddress(Address model)
        {
            if (!ModelState.IsValid)
            {
                var addressDAO = new AddressDAO();
            addressDAO.updateAddress(model);
            return RedirectToAction("Address", "Address");
            }
            ModelState.AddModelError("", "Please enter the new address name");
            return View();
        }


        public ActionResult DeleteAddress(int id)
        {
            var addressDAO = new AddressDAO();
            addressDAO.deleteIDAddressinContact(id);
            addressDAO.deleteAddress(id);
            
            return RedirectToAction("Address", "Address");
        }
    }
}