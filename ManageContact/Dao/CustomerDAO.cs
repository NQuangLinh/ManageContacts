using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Security.Cryptography;
using System.Text;

namespace ManageContact.Dao
{
    public class CustomerDAO
    {
        DBManageContactDataContext db = null;
        public CustomerDAO()
        {
            db = new DBManageContactDataContext();
        }
        public bool CheckUserName(string userName)
        {
            return db.Accounts.Count(p => p.Username == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.Customers.Count(p => p.Email == email) > 0;
        }


        public bool CheckEmailCustomer(CustomerModel model)
        {
            return db.Customers.Where(p => p.IDCustomer == model.IDCustomer).Count(p => p.Email == model.Email) > 0;
        }

        public bool CheckPhoneNumber(int phoneNumber)
        {
            return db.Customers.Count(p => p.PhoneNumber == phoneNumber) > 0;
        }

        public bool CheckPhoneNumberCustomer(CustomerModel model)
        {
            return db.Customers.Where(p => p.IDCustomer == model.IDCustomer).Count(p => p.PhoneNumber == model.PhoneNumber) > 0;
        }

        public void insertAccount(Users model)
        {
            model.Password = GetMD5(model.Password);
            Account account = new Account
            {
                Username = model.Username,
                Password = model.Password
            };
            db.Accounts.InsertOnSubmit(account);
            db.SubmitChanges();
        }

        public void insertCustomer(Users model)
        {
            Customer customer = new Customer
            {
                CustomerName = model.CustomerName,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address,
                IDAccount = db.Accounts.Where(p => p.Username == model.Username).Select(p => p.IDAccount).FirstOrDefault()
            };
            db.Customers.InsertOnSubmit(customer);
            db.SubmitChanges();
        }


        public IPagedList<ContactsModel> getAllContact(int page,int pageSize,int idCustomer)
        {

           var contact = from a in db.Contacts
                          where a.IDCustomer == idCustomer
                          select new ContactsModel()
                          {
                              IDContact = a.IDContact,
                              ContactName = a.ContactName,
                              PhoneNumber = a.PhoneNumber,
                              NetworkOperatorName = db.NetworkOperators.Where(p => p.IDNetworkOperator == a.IDNetworkOperator).Select(p => p.NetworkOperatorName).SingleOrDefault(),
                              AddressName = db.Addresses.Where(p => p.IDAddress == a.IDAddress).Select(p => p.AddressName).SingleOrDefault(),
                              PositionName = db.Positions.Where(p => p.IDPosition == a.IDPosition).Select(p => p.PositionName).SingleOrDefault(),
                              UpdateTime = a.UpdateTime
                          };

            return contact.OrderByDescending(p => p.UpdateTime).ToPagedList(page,pageSize);
        }

        public IPagedList<ContactsModel> getContactforAddress(int? idAddress, int idCustomer, int page, int pageSize)
        {

            var contact = from a in db.Contacts
                          where a.IDAddress == idAddress && a.IDCustomer == idCustomer
                          select new ContactsModel()
                          {
                              IDContact = a.IDContact,
                              ContactName = a.ContactName,
                              PhoneNumber = a.PhoneNumber,
                              NetworkOperatorName = db.NetworkOperators.Where(p => p.IDNetworkOperator == a.IDNetworkOperator).Select(p => p.NetworkOperatorName).SingleOrDefault(),
                              AddressName =  db.Addresses.Where(p => p.IDAddress == a.IDAddress).Select(p => p.AddressName).SingleOrDefault(),
                              PositionName =  db.Positions.Where(p => p.IDPosition == a.IDPosition).Select(p => p.PositionName).SingleOrDefault(),
                              UpdateTime = a.UpdateTime
                          };

            return contact.OrderByDescending(p => p.UpdateTime).ToPagedList(page, pageSize);
        }




        public IPagedList<ContactsModel> getContactforNetWorkOperator(int? idNetWorkOperator, int idCustomer, int page, int pageSize)
        {

            var contact = from a in db.Contacts
                          where a.IDNetworkOperator == idNetWorkOperator && a.IDCustomer == idCustomer
                          select new ContactsModel()
                          {
                              IDContact = a.IDContact,
                              ContactName = a.ContactName,
                              PhoneNumber = a.PhoneNumber,
                              NetworkOperatorName = db.NetworkOperators.Where(p => p.IDNetworkOperator == a.IDNetworkOperator).Select(p => p.NetworkOperatorName).SingleOrDefault(),
                              AddressName =  db.Addresses.Where(p => p.IDAddress == a.IDAddress).Select(p => p.AddressName).SingleOrDefault(),
                              PositionName =  db.Positions.Where(p => p.IDPosition == a.IDPosition).Select(p => p.PositionName).SingleOrDefault(),
                              UpdateTime = a.UpdateTime
                          };

            return contact.OrderByDescending(p => p.UpdateTime).ToPagedList(page, pageSize);
        }



        public IPagedList<ContactsModel> getContactUnknow(int idCustomer, int page, int pageSize)
        {

            var contact = from a in db.Contacts
                          where a.IDAddress == null && a.IDCustomer == idCustomer || a.IDNetworkOperator == null && a.IDCustomer == idCustomer || a.IDPosition == null && a.IDCustomer == idCustomer
                          select new ContactsModel()
                          {
                              IDContact = a.IDContact,
                              ContactName = a.ContactName,
                              PhoneNumber = a.PhoneNumber,
                              NetworkOperatorName =db.NetworkOperators.Where(p => p.IDNetworkOperator == a.IDNetworkOperator).Select(p => p.NetworkOperatorName).SingleOrDefault(),
                              AddressName = db.Addresses.Where(p => p.IDAddress == a.IDAddress).Select(p => p.AddressName).SingleOrDefault(),
                              PositionName =  db.Positions.Where(p => p.IDPosition == a.IDPosition).Select(p => p.PositionName).SingleOrDefault(),
                              UpdateTime = a.UpdateTime
                          };

            return contact.OrderByDescending(p => p.UpdateTime).ToPagedList(page, pageSize);
        }


        public IPagedList<ContactsModel> getContactforSearch(string idSearch,int idCustomer, int page, int pageSize)
        {

            var resultSearchContactName = from a in db.Contacts
                          where a.ContactName.Contains(idSearch) && a.IDCustomer == idCustomer
                          select new ContactsModel()
                          {
                              IDContact = a.IDContact,
                              ContactName = a.ContactName,
                              PhoneNumber = a.PhoneNumber,
                              NetworkOperatorName = db.NetworkOperators.Where(p => p.IDNetworkOperator == a.IDNetworkOperator).Select(p => p.NetworkOperatorName).SingleOrDefault(),
                              AddressName = db.Addresses.Where(p => p.IDAddress == a.IDAddress).Select(p => p.AddressName).SingleOrDefault(),
                              PositionName = db.Positions.Where(p => p.IDPosition == a.IDPosition).Select(p => p.PositionName).SingleOrDefault(),
                              UpdateTime = a.UpdateTime
                          };
            return resultSearchContactName.OrderByDescending(p => p.UpdateTime).ToPagedList(page, pageSize);
        }

        public void insertContact(ContactActionModel model, int idCustomer)
        {
            Contact contact = new Contact
            {
                IDCustomer = idCustomer,
                ContactName = model.ContactName,
                PhoneNumber = model.PhoneNumber,
                IDNetworkOperator = model.IDNetworkOperator,
                IDPosition = model.IDPosition,
                IDAddress = model.IDAddress,
                UpdateTime = DateTime.Now
                
            };
            db.Contacts.InsertOnSubmit(contact);
            db.SubmitChanges();
        }

        public void updateContact(ContactActionModel model, int idCustomer)
        {
            var update = db.Contacts.Where(p => p.IDContact == model.IDContact && p.IDCustomer == idCustomer).SingleOrDefault();
            update.ContactName = model.ContactName;
            update.PhoneNumber = model.PhoneNumber;
            update.IDNetworkOperator = model.IDNetworkOperator;
            update.IDAddress = model.IDAddress;
            update.IDPosition = model.IDPosition;
            db.SubmitChanges();
        }

        public void deleteContacts(int[] deletelist)
        {
            foreach (int idContact in deletelist)
            {
                Contact contact = db.Contacts.Where(p => p.IDContact == idContact).SingleOrDefault();
                db.Contacts.DeleteOnSubmit(contact);
            }
            db.SubmitChanges();
        }


        public ContactActionModel getContact(int idContact)
        {
            ContactActionModel contact = (from a in db.Contacts
                           where a.IDContact == idContact
                           select new ContactActionModel()
                           {
                               IDContact = a.IDContact,
                               ContactName = a.ContactName,
                               PhoneNumber = a.PhoneNumber,
                               IDNetworkOperator = a.IDNetworkOperator,
                               IDAddress = a.IDAddress,
                               IDPosition = a.IDPosition
                           }).Single();
            return contact;
        }


        public static string GetMD5(string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }

            return sbHash.ToString();

        }



    }
}