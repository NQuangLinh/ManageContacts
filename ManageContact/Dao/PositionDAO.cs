using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageContact.Dao
{
    public class PositionDAO
    {
        DBManageContactDataContext db = null;
        public PositionDAO()
        {
            db = new DBManageContactDataContext();
        }

        public IEnumerable<Position> getPosition(int idCustomer)
        {
            IEnumerable<Position> listPosition = db.Positions.Where(p => p.IndexCustomer == idCustomer);
            return listPosition;
        }

        public IEnumerable<Position> getPositionDisplay(IEnumerable<Position> listPosition, int idCustomer)
        {
            foreach (Position position in listPosition)
            {
                position.PositionAmount = db.Contacts.Count(p => p.IDPosition == position.IDPosition);
            }
            db.SubmitChanges();

            IEnumerable<Position> listPositionDisplay = db.Positions.Where(p => p.IndexCustomer == idCustomer);
            return listPositionDisplay;
        }

        public Position getPositionEdit(int idPosition)
        {
            Position position = db.Positions.Where(p => p.IDPosition == idPosition).SingleOrDefault();
            return position;
        }


        public void insertPosition(Position model, int idCustomer)
        {
            Position position = new Position
            {
                PositionName = model.PositionName,
                PositionAmount = 0,
                IndexCustomer = idCustomer,
            };
            db.Positions.InsertOnSubmit(position);
            db.SubmitChanges();
        }

        public void updatePosition(Position model)
        {
            var update = db.Positions.Where(p => p.IDPosition == model.IDPosition).SingleOrDefault();
            update.PositionName = model.PositionName;
            db.SubmitChanges();
        }

        public void deletePosition(int id)
        {

            var delete = db.Positions.Where(p => p.IDPosition == id).SingleOrDefault();
            db.Positions.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }


        public void deleteIDPositioninContact(int id)
        {

            var listIDPosition = db.Contacts.Where(p => p.IDPosition == id);
            foreach (Contact contact in listIDPosition)
            {
                contact.IDPosition = null;
            }
            db.SubmitChanges();
        }
    }
}