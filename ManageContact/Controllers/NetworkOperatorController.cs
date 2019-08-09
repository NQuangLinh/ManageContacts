using ManageContact.Dao;
using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageContact.Controllers
{
    public class NetworkOperatorController : BaseController
    {
        public ActionResult NetworkOperator()
        {

            int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
            var networkOperatorDAO = new NetworkOperatorDAO();
            IEnumerable<NetworkOperator> listNetworkOperator = networkOperatorDAO.getNetworkOperator(idCustomer);
            IEnumerable<NetworkOperator> listNetworkOperatorDisplay = networkOperatorDAO.getNetworkOperatorDisplay(listNetworkOperator, idCustomer);
            return View(listNetworkOperatorDisplay);
        }


        [HttpGet]
        public ActionResult CreateNetworkOperator()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateNetworkOperator(NetworkOperator model)
        {
            if (ModelState.IsValid)
            {
                var networkOperatorDAO = new NetworkOperatorDAO();
                networkOperatorDAO.insertNetworkOperator(model, Int32.Parse(Session["IDCustomer"].ToString()));
                return RedirectToAction("NetworkOperator", "NetworkOperator");
            }
            ModelState.AddModelError("", "Please enter the Network operator name");
            return View();
        }



                [HttpGet]
        public ActionResult EditNetworkOperator(int id)
        {
            var networkOperatorDAO = new NetworkOperatorDAO();
            NetworkOperator networkOperator = networkOperatorDAO.getNetworkOperatorEdit(id);
            return View(networkOperator);
        }


        [HttpPost]
        public ActionResult EditNetworkOperator(NetworkOperator model)
        {
            if (!ModelState.IsValid)
            {
                var networkOperatorDAO = new NetworkOperatorDAO();
                networkOperatorDAO.updateNetworkOperator(model);
                return RedirectToAction("NetworkOperator", "NetworkOperator");
            }
            ModelState.AddModelError("", "Please enter the Network operator name");
            return View();
        }

        


        public ActionResult DeleteNetworkOperator(int id)
        {
            var networkOperatorDAO = new NetworkOperatorDAO();
            networkOperatorDAO.deleteIDNetworkOperatorinContact(id);
            networkOperatorDAO.deleteNetworkOperator(id);
            return RedirectToAction("NetworkOperator", "NetworkOperator");
        }

    }
}