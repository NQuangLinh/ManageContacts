using ManageContact.Dao;
using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageContact.Controllers
{
    public class PositionController : BaseController
    {
        // GET: Position
        public ActionResult Position()
        {

            int idCustomer = Int32.Parse(Session["IDCustomer"].ToString());
            var positionDAO = new PositionDAO();
            IEnumerable<Position> listPosition = positionDAO.getPosition(idCustomer);
            IEnumerable<Position> listPositionDisplay = positionDAO.getPositionDisplay(listPosition, idCustomer);
            return View(listPositionDisplay);
        }


        [HttpGet]
        public ActionResult CreatePosition()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreatePosition(Position model)
        {
            if (ModelState.IsValid)
            {
                var positionDAO = new PositionDAO();
                positionDAO.insertPosition(model,Int32.Parse( Session["IDCustomer"].ToString()));
                return RedirectToAction("Position", "Position");
            }
            ModelState.AddModelError("", "Please enter the Position name");
            return View();
        }


        [HttpGet]
        public ActionResult EditPosition(int id)
        {
            var positionDAO = new PositionDAO();
            Position position = positionDAO.getPositionEdit(id);
            return View(position);
        }


        [HttpPost]
        public ActionResult EditPosition(Position model)
        {
            if (!ModelState.IsValid)
            {
                var positionDAO = new PositionDAO();
                positionDAO.updatePosition(model);
                return RedirectToAction("Position", "Position");
            }
            ModelState.AddModelError("", "Please enter the new position name");
            return View();
        }


        public ActionResult DeletePosition(int id)
        {
            var positionDAO = new PositionDAO();
            positionDAO.deleteIDPositioninContact(id);
            positionDAO.deletePosition(id);
            return RedirectToAction("Position", "Position");
        }
    }
}