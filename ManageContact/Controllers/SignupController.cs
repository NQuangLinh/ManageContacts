using ManageContact.Dao;
using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageContact.Controllers
{
    public class SignupController : Controller
    {
        //GET: Signup
       [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Users model)
        {
            if (ModelState.IsValid)
            {
                var customerDao = new CustomerDAO();
                if (customerDao.CheckUserName(model.Username))
                {
                    ModelState.AddModelError("", "This account has already existed.");
                }
                else if (customerDao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "This email has already existed.");
                }
                else if (customerDao.CheckPhoneNumber(model.PhoneNumber))
                {
                    ModelState.AddModelError("", "This phone number has already existed.");
                }
                else
                {
                    customerDao.insertAccount(model);
                    customerDao.insertCustomer(model);
                    string message = "Sign up successfully, invite login.";
                    return RedirectToAction("Login","Login", new { message });
                }
        }
            return View(model);
    } }
    }