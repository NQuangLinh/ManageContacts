using ManageContact.Dao;
using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ManageContact.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        [HttpGet]
        public ActionResult CustomerHome(string idSearch,int? idAddress,int? idNetWorkOperator,string idUnknow,int page =1,int pageSize = 10)
        {
            int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
            CustomerDAO customerDao = new CustomerDAO();
            IPagedList<ContactsModel> listAllContact;
            if (idSearch != null)
            {
                listAllContact = customerDao.getContactforSearch(idSearch, idCustomer, page, pageSize);
            }
            else if (idUnknow != null)
            {
                listAllContact = customerDao.getContactUnknow(page,pageSize, idCustomer);
            }
            else if (idAddress != null)
            {
                listAllContact = customerDao.getContactforAddress( idAddress, idCustomer, page, pageSize);
            }
            else if (idNetWorkOperator != null)
            {
                listAllContact = customerDao.getContactforNetWorkOperator(idNetWorkOperator, idCustomer, page, pageSize);
            }
            else
            {
                listAllContact = customerDao.getAllContact(page, pageSize, idCustomer);
            }
            return View(listAllContact);

        }

        public ActionResult AddressPartial()
        {
            int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
            var addressDAO = new AddressDAO();
            IEnumerable<Address> listAddress = addressDAO.getAddress(idCustomer);
            return PartialView(listAddress);
        }

        public ActionResult NetworkOperatorPartial()
        {
            int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
            var networkOperatorDAO = new NetworkOperatorDAO();
            IEnumerable<NetworkOperator> listNetworkOperators = networkOperatorDAO.getNetworkOperator(idCustomer);
            return PartialView(listNetworkOperators);
        }

        [HttpPost]
        public ActionResult DeleteContacts(int[] deletelist)
        {
            if(deletelist == null)
            {
                return RedirectToAction("CustomerHome", "Customer");
            }
            var customerDao = new CustomerDAO();
            customerDao.deleteContacts(deletelist);
            string message = "Delete contact successfully.";
            return RedirectToAction("CustomerHome", "Customer", new { message });
        }
        [HttpGet]
        public ActionResult CreateContact()
        {
            SetViewBagNetworkOperator();
            SetViewBagAddress();
            SetViewBagPosition();
            return View();
        }

        [HttpPost]
        public ActionResult CreateContact(ContactActionModel model )
        {
            if (ModelState.IsValid)
            {
                int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
                var customerDao = new CustomerDAO();
                customerDao.insertContact(model, idCustomer);
                string message = "Create contact successfully.";
                return RedirectToAction("CustomerHome", "Customer", new { message });
            }
            SetViewBagNetworkOperator();
            SetViewBagAddress();
            SetViewBagPosition();
            return View(model);

        }
        [HttpGet]
        public ActionResult EditContact(int idContact)
        {
            var customerDao = new CustomerDAO();
            ContactActionModel contact = customerDao.getContact(idContact);
            SetViewBagNetworkOperator(contact.IDNetworkOperator);
            SetViewBagAddress(contact.IDAddress);
            SetViewBagPosition(contact.IDPosition);
            return View(contact);
        }

        [HttpPost]
        public ActionResult EditContact(ContactActionModel model)
        {
            if (ModelState.IsValid)
            {
                int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
                var customerDao = new CustomerDAO();
                customerDao.updateContact(model, idCustomer);
                string message = "Update contact successfully.";
                return RedirectToAction("CustomerHome", "Customer", new { message });
            }
            SetViewBagNetworkOperator(model.IDNetworkOperator);
            SetViewBagAddress(model.IDAddress);
            SetViewBagPosition(model.IDPosition);
            return View(model);
        }



        public void SetViewBagNetworkOperator(int? selectedIDNetworkOperator = null)
        {
            int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
            var networkOperatorDAO = new NetworkOperatorDAO();
            var listNetworkOperator = networkOperatorDAO.getNetworkOperator(idCustomer);
            ViewBag.IDNetworkOperator = new SelectList(listNetworkOperator, "IDNetworkOperator", "NetworkOperatorName", selectedIDNetworkOperator);
        }

        public void SetViewBagAddress(int? selectedIDAddress = null)
        {
            int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
            var addressDAO = new AddressDAO();
            var listAddress = addressDAO.getAddress(idCustomer);
            ViewBag.IDAddress = new SelectList(listAddress, "IDAddress", "AddressName", selectedIDAddress);
        }

        public void SetViewBagPosition(int? selectedIDPosition = null)
        {
            int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
            var positionDAO = new PositionDAO();
            var listPosition = positionDAO.getPosition(idCustomer);
            ViewBag.IDPosition = new SelectList(listPosition, "IDPosition", "PositionName", selectedIDPosition);
        }

    }
}