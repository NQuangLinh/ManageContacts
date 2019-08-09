using ManageContact.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ManageContact.Areas.Admin.Dao
{
    public class AdminDAO
    {
        DBManageContactDataContext db = null;
        public AdminDAO()
        {
            db = new DBManageContactDataContext();
        }
        public IPagedList<CustomerModel> getAllCustomer(int page, int pageSize)
        {

            var customer = from a in db.Customers
                          select new CustomerModel()
                          {
                              IDCustomer = a.IDCustomer,
                              CustomerName = a.CustomerName,
                              Gender = a.Gender,
                              PhoneNumber = a.PhoneNumber,
                              Email = a.Email,
                              Address = a.Address,
                              ContactQuantity = db.Contacts.Count(p => p.IDCustomer == a.IDCustomer)
                          };

            return customer.OrderBy(p => p.IDCustomer).ToPagedList(page, pageSize);
        }


        public bool CheckUserName(string userName)
        {
            return db.Accounts.Count(p => p.Username == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.Admins.Count(p => p.Email == email) > 0;
        }


        public bool CheckPhoneNumber(int phoneNumber)
        {
            return db.Admins.Count(p => p.PhoneNumber == phoneNumber) > 0;
        }



        public void insertAccount(AdminModel model)
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

        public void insertAdmin(AdminModel model)
        {
            ManageContact.Models.Admin admin = new Models.Admin
            {
                AdminName = model.AdminName,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address,
                IDAccount = db.Accounts.Where(p => p.Username == model.Username).Select(p => p.IDAccount).FirstOrDefault()
            };
            db.Admins.InsertOnSubmit(admin);
            db.SubmitChanges();
        }
        public CustomerModel getCustomer(int idCustomer)
        {
            CustomerModel customer = (from a in db.Customers
                                          where a.IDCustomer == idCustomer
                                      select new CustomerModel()
                                          {
                                              IDCustomer = a.IDCustomer,
                                              CustomerName = a.CustomerName,
                                              Gender = a.Gender,
                                              PhoneNumber = a.PhoneNumber,
                                              Email = a.Email,
                                              Address = a.Address
                                          }).Single();
            return customer;
        }

        public void updateCustomer(CustomerModel model)
        {
            var update = db.Customers.Where(p => p.IDCustomer == model.IDCustomer).SingleOrDefault();
            update.CustomerName = model.CustomerName;
            update.Gender = model.Gender;
            update.PhoneNumber = model.PhoneNumber;
            update.Email = model.Email;
            update.Address = model.Address;
            db.SubmitChanges();
        }

        public void deleteContactCustomer(int idCustomer)
        {

            IEnumerable<Contact>  listContacts = db.Contacts.Where(p => p.IDCustomer == idCustomer);
            foreach (Contact contact in listContacts)
            {
                db.Contacts.DeleteOnSubmit(contact);

            }
            db.SubmitChanges();
        }

        public void deleteAddressCustomer(int idCustomer)
        {

            IEnumerable<Address> listAddresses = db.Addresses.Where(p => p.IndexCustomer == idCustomer);
            foreach (Address address in listAddresses)
            {
                db.Addresses.DeleteOnSubmit(address);

            }
            db.SubmitChanges();
        }

        public void deletePositionCustomer(int idCustomer)
        {

            IEnumerable<Position> listPositions = db.Positions.Where(p => p.IndexCustomer == idCustomer);
            foreach (Position position in listPositions)
            {
                db.Positions.DeleteOnSubmit(position);

            }
            db.SubmitChanges();
        }


        public void deleteNetworkOperatorCustomer(int idCustomer)
        {

            IEnumerable<NetworkOperator> listNetworkOperators = db.NetworkOperators.Where(p => p.IndexCustomer == idCustomer);
            foreach (NetworkOperator networkOperator in listNetworkOperators)
            {
                db.NetworkOperators.DeleteOnSubmit(networkOperator);

            }
            db.SubmitChanges();
        }


        public Account getAccountCustomer(int idCustomer)
        {
            Account account = db.Accounts.Where(p => p.IDAccount == (db.Customers.Where(a => a.IDCustomer == idCustomer).Select(a => a.IDAccount).SingleOrDefault())).SingleOrDefault();
            return account;
        }

        public void deleteAccountCustomer(Account account)
        {
            db.Accounts.DeleteOnSubmit(account);
            db.SubmitChanges();
        }

        public void deleteCustomer(int idCustomer)
        {
            Customer customer = db.Customers.Where(p => p.IDCustomer == idCustomer).SingleOrDefault();
            db.Customers.DeleteOnSubmit(customer);
            db.SubmitChanges();
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

        public Models.Admin getAdmin(int idAdmin)
        {
            Models.Admin admin = db.Admins.Where(p => p.IDAdmin == idAdmin).SingleOrDefault();
            return admin;
        }


        public IPagedList<CustomerModel> getCustomerforSearch(string idSearch,int page, int pageSize)
        {

            var customer = from a in db.Customers
                           where a.CustomerName.Contains(idSearch)
                           select new CustomerModel()
                           {
                               IDCustomer = a.IDCustomer,
                               CustomerName = a.CustomerName,
                               Gender = a.Gender,
                               PhoneNumber = a.PhoneNumber,
                               Email = a.Email,
                               Address = a.Address,
                               ContactQuantity = db.Contacts.Count(p => p.IDCustomer == a.IDCustomer)
                           };

            return customer.OrderBy(p => p.IDCustomer).ToPagedList(page, pageSize);
        }
    }
}