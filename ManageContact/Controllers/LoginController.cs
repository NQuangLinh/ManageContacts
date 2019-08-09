
using ManageContact.Dao;
using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageContact.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users model)
        {
            if (!ModelState.IsValid)
            {
                var userDao = new UserDAO();
                if (userDao.CheckAccount(model.Username,model.Password))
                {
                    int idAccount = userDao.getIDAccount(model.Username, model.Password);
                    if (userDao.CheckCustomer(idAccount))
                    {
                        Session["IDCustomer"] = userDao.getIDCustomer(idAccount);
                        Session["CustomerName"] = userDao.getCustomerName(idAccount);
                        Session["Username"] = model.Username;
                        return RedirectToAction("CustomerHome", "Customer");
                    }
                    else
                    {
                        Session["IDAdmin"] = userDao.getIDAdmin(idAccount);
                        Session["AdminName"] = userDao.getAdminName(idAccount);
                        Session["Username"] = model.Username;
                        return RedirectToRoute(new {controller ="Admin",action ="AdminHome",area ="Admin" });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Account name or password is incorrect.");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","Login");
        }
    }
}