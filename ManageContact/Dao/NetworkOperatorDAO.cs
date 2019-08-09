using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageContact.Dao
{
    public class NetworkOperatorDAO
    {
        DBManageContactDataContext db = null;
        public NetworkOperatorDAO()
        {
            db = new DBManageContactDataContext();
        }

        public IEnumerable<NetworkOperator> getNetworkOperator(int idCustomer)
        {
            IEnumerable<NetworkOperator> listNetworkOperator = db.NetworkOperators.Where(p => p.IndexCustomer == idCustomer);
            return listNetworkOperator;
        }

        public IEnumerable<NetworkOperator> getNetworkOperatorDisplay(IEnumerable<NetworkOperator> listNetworkOperator, int idCustomer)
        {
            foreach (NetworkOperator networkOperator in listNetworkOperator)
            {
                networkOperator.NetworkOperatorAmount = db.Contacts.Count(p => p.IDNetworkOperator == networkOperator.IDNetworkOperator);
            }
            db.SubmitChanges();

            IEnumerable<NetworkOperator> listNetworkOperatorDisplay = db.NetworkOperators.Where(p => p.IndexCustomer == idCustomer);
            return listNetworkOperatorDisplay;
        }

        public NetworkOperator getNetworkOperatorEdit(int idNetworkOperator)
        {
            NetworkOperator networkOperator = db.NetworkOperators.Where(p => p.IDNetworkOperator == idNetworkOperator).SingleOrDefault();
            return networkOperator;
        }

        public void insertNetworkOperator(NetworkOperator model, int idCustomer)
        {
            NetworkOperator networkOperator = new NetworkOperator
            {
                NetworkOperatorName = model.NetworkOperatorName,
                NetworkOperatorAmount = 0,
                IndexCustomer = idCustomer,
            };
            db.NetworkOperators.InsertOnSubmit(networkOperator);
            db.SubmitChanges();
        }


        public void updateNetworkOperator(NetworkOperator model)
        {
            var update = db.NetworkOperators.Where(p => p.IDNetworkOperator == model.IDNetworkOperator).SingleOrDefault();
            update.NetworkOperatorName = model.NetworkOperatorName;
            db.SubmitChanges();
        }


        public void deleteNetworkOperator(int id)
        {

            var delete = db.NetworkOperators.Where(p => p.IDNetworkOperator == id).SingleOrDefault();
            db.NetworkOperators.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }

         public void deleteIDNetworkOperatorinContact(int id)
        {

            var listIDNetworkOperator = db.Contacts.Where(p => p.IDNetworkOperator == id);
            foreach (Contact contact in listIDNetworkOperator)
            {
                contact.IDNetworkOperator = null;
            }
            db.SubmitChanges();
        }
    }
}