using ManageContact.Areas.Admin.Dao;
using ManageContact.Dao;
using ManageContact.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageContact.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin/Admin
        public ActionResult AdminHome(string idSearch, int page = 1, int pageSize = 10)
        {
            int idAdmin = Int32.Parse(Session["IDAdmin"].ToString());
            var adminDao = new AdminDAO();
            IPagedList<CustomerModel> listCustomer;
            if (idSearch != null)
            {
                listCustomer = adminDao.getCustomerforSearch(idSearch, page, pageSize);
            }
            else
            {
                listCustomer = adminDao.getAllCustomer(page, pageSize);
            } 
            return View(listCustomer);
        }

        [HttpGet]
        public ActionResult SignUpAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUpAdmin(AdminModel model)
        {
            if (ModelState.IsValid)
            {
                var adminDao = new AdminDAO();
                if (adminDao.CheckUserName(model.Username))
                {
                    ModelState.AddModelError("", "This account has already existed.");
                }
                else if (adminDao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "This email has already existed.");
                }
                else if (adminDao.CheckPhoneNumber(model.PhoneNumber))
                {
                    ModelState.AddModelError("", "This phone number has already existed.");
                }
                else
                {
                    
                    adminDao.insertAccount(model);
                    adminDao.insertAdmin(model);
                    string message = "Sign up successfully.";
                    return RedirectToAction("AdminHome", "Admin", new { message });
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            var adminDao = new AdminDAO();
            CustomerModel customer = adminDao.getCustomer(id);
            return View(customer);


        }


        [HttpPost]
        public ActionResult EditCustomer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var customerDao = new CustomerDAO();
                if (customerDao.CheckEmail(model.Email) && !customerDao.CheckEmailCustomer(model))
                {
                    ModelState.AddModelError("", "This email has already existed.");
                }
                else if (customerDao.CheckPhoneNumber(model.PhoneNumber) && !customerDao.CheckPhoneNumberCustomer(model))
                {
                    ModelState.AddModelError("", "This phone number has already existed.");
                }
                else
                {
                    var adminDao = new AdminDAO();
                    adminDao.updateCustomer(model);
                    string message = "Update customer successfully.";
                    return RedirectToAction("AdminHome", "Admin", new { message });
                }
            }
            return View(model);
        }


        public ActionResult DeleteCustomer(int id)
        {
            var adminDao = new AdminDAO();
            Account account = adminDao.getAccountCustomer(id);
            adminDao.deleteContactCustomer(id);
            adminDao.deleteAddressCustomer(id);
            adminDao.deleteNetworkOperatorCustomer(id);
            adminDao.deletePositionCustomer(id);
            adminDao.deleteCustomer(id);
            adminDao.deleteAccountCustomer(account);
            string message = "Delete customer successfully.";
            return RedirectToAction("AdminHome", "Admin", new { message });
        }

        public ActionResult MyProfile()
        {
            var adminDao = new AdminDAO();
            int idAdmin = Int32.Parse(Session["IDAdmin"].ToString());
            Models.Admin admin = adminDao.getAdmin(idAdmin);
            return View(admin);
        }
    }
}