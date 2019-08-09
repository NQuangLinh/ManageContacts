using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageContact.Dao
{
    public class AddressDAO
    {
        DBManageContactDataContext db = null;
        public AddressDAO()
        {
            db = new DBManageContactDataContext();
        }
        public IEnumerable<Address> getAddress(int idCustomer)
        {
            IEnumerable<Address> listAddress = db.Addresses.Where(p => p.IndexCustomer == idCustomer);
            return listAddress;
        }

        public IEnumerable<Address> getAddressDisplay(IEnumerable<Address> listAddress, int idCustomer)
        {
            foreach (Address address in listAddress)
            {
                address.AddressAmount = db.Contacts.Count(p => p.IDAddress == address.IDAddress);
            }
            db.SubmitChanges();

            IEnumerable<Address> listAddressDisplay = db.Addresses.Where(p => p.IndexCustomer == idCustomer);
            return listAddressDisplay;
        }

        public Address getAddressEdit(int idAddress)
        {
            Address address = db.Addresses.Where(p => p.IDAddress == idAddress).SingleOrDefault();
            return address;
        }

        public void insertAddress(Address model,int idCustomer)
        {
            Address address = new Address
            {
                AddressName = model.AddressName,
                AddressAmount = 0,
                IndexCustomer = idCustomer,
            };
            db.Addresses.InsertOnSubmit(address);
            db.SubmitChanges();
        }

        public void deleteAddress(int id)
        {

            var delete = db.Addresses.Where(p => p.IDAddress == id).SingleOrDefault();
            db.Addresses.DeleteOnSubmit(delete);
            db.SubmitChanges();
        }

        public void updateAddress(Address model)
        {
            var update = db.Addresses.Where(p => p.IDAddress == model.IDAddress).SingleOrDefault();
            update.AddressName = model.AddressName;
            db.SubmitChanges();
        }

        public void deleteIDAddressinContact(int id)
        {

            var listIDAddress = db.Contacts.Where(p => p.IDAddress == id);
            foreach(Contact contact in listIDAddress)
            {
                contact.IDAddress = null;
            }
            db.SubmitChanges();
        }
    }
}