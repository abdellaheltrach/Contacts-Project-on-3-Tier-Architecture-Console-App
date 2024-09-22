using System;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using ContactsDataAccessLayer;

namespace ContactBusinessLayer
{
    public class clsContact
    {
        public int ID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string ImagePath { set; get; }
        public int CountryID { set; get; }

        private enum enMode
        {
            UpdateMode, AddNewMode
        }

        static private enMode _enCurrantMode ;

        public clsContact()

        {
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";

            _enCurrantMode = enMode.AddNewMode;

        }


        private clsContact(int ID, string FirstName, string LastName,
    string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)

        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

            _enCurrantMode = enMode.UpdateMode;

        }

        public static clsContact Find(int ID)
        {
            _enCurrantMode = enMode.UpdateMode;
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;

            if (clsContactDataAccess.GetContactInfoByID(ID, ref FirstName, ref LastName,
                          ref Email, ref Phone, ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))

                return new clsContact(ID, FirstName, LastName,
                           Email, Phone, Address, DateOfBirth, CountryID, ImagePath);
            else
                return null;
            
        }

        private bool _addNewContact()
        {
            this.ID = clsContactDataAccess.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
            return this.ID != -1;

        }
        private bool _UpdateContact()
        {
            return clsContactDataAccess.UpdateContact(this.ID, this.FirstName, this.LastName,
     this.Email, this.Phone, this.Address,
     this.DateOfBirth, this.CountryID, this.ImagePath);
        }

        public static bool DeleteContact(int ID)
        {
            return clsContactDataAccess.DeleteContact(ID);
        }



        public bool save()
        {

            if (_enCurrantMode == enMode.AddNewMode)
            {
                if (_addNewContact())
                {
                    _enCurrantMode = enMode.UpdateMode;
                    return true;

                }
                else
                    return false;
            }
            else 
            {

                return _UpdateContact();



            }
                

        }

        public static DataTable GetAllContacts()
        {
            return clsContactDataAccess.GetAllContacts();

        }

        public static bool IsExist(int ID)
        {
        return clsContactDataAccess.IsExist(ID);
        }


    }
}
