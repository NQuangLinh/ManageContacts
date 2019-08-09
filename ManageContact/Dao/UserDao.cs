using ManageContact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ManageContact.Dao
{
    public class UserDAO
    {
        DBManageContactDataContext db = null;
        public UserDAO()
        {
            db = new DBManageContactDataContext();
        }
        public bool CheckAccount(string userName, string password)
        {
            password = GetMD5(password);
            return db.Accounts.Count(p => p.Username == userName && p.Password == password) > 0;
        }

        public int getIDAccount(string userName, string password)
        {
            password = GetMD5(password);
            return db.Accounts.Where(p => p.Username == userName && p.Password == password).Select(p => p.IDAccount).SingleOrDefault();
        }
        public int getIDCustomer(int idAccount)
        {
            return db.Customers.Where(p => p.IDAccount == idAccount).Select(p => p.IDCustomer).SingleOrDefault();
        }

        public string getCustomerName(int idAccount)
        {
            return db.Customers.Where(p => p.IDAccount == idAccount).Select(p => p.CustomerName).SingleOrDefault();
        }

        public int getIDAdmin(int idAccount)
        {
            return db.Admins.Where(p => p.IDAccount == idAccount).Select(p => p.IDAdmin).SingleOrDefault();
        }
        public string getAdminName(int idAccount)
        {
            return db.Admins.Where(p => p.IDAccount == idAccount).Select(p => p.AdminName).SingleOrDefault();
        }

        public bool CheckCustomer(int idAccount)
        {
            return db.Customers.Count(p => p.IDAccount == idAccount) > 0;
        }

        public bool CheckAdmin(int idAccount)
        {
            return db.Customers.Count(p => p.IDAccount == idAccount) > 0;
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