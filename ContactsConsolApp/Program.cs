using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Security.Policy;
using ContactBusinessLayer;


namespace ContactsConsolApp
{
    internal class Program
    {
        static void FindContact(int ID)

        {
            clsContact Contact1 = clsContact.Find(ID);

            if (Contact1 != null)
            {
                Console.WriteLine(Contact1.FirstName + " " + Contact1.LastName);
                Console.WriteLine(Contact1.Email);
                Console.WriteLine(Contact1.Phone);
                Console.WriteLine(Contact1.Address);
                Console.WriteLine(Contact1.DateOfBirth);
                Console.WriteLine(Contact1.CountryID);
                Console.WriteLine(Contact1.ImagePath);
            }
            else
            {
                Console.WriteLine("Contact [" + ID + "] Not found!");
            }
        }


        static void UpdateContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);

            Contact1.FirstName = "Abdellah";
            Contact1.LastName = "Eltrach";
            Contact1.Email = "Abdellah@Example.com";
            Contact1.Phone = "325435346";
            Contact1.Address = "Ouled sguir";
            Contact1.DateOfBirth = new DateTime(2001, 09, 10);
            Contact1.CountryID = 1;
            Contact1.ImagePath = "";

            if (Contact1.save())
            {

                Console.WriteLine(@"contact Updated successfully and the ID is {0} ", Contact1.ID);

            }
            else { Console.WriteLine("Error 404"); }



        }


        static void addNewContact()
        {
            clsContact Contact1 = new clsContact(); ;
            Contact1.FirstName = "Abdellah";
            Contact1.LastName = "Abdellah";
            Contact1.Email = "Abdellah";
            Contact1.Phone = "Abdellah";
            Contact1.Address = "Abdellah";
            Contact1.DateOfBirth = new DateTime(2001,09,10);
            Contact1.CountryID = 2;
            Contact1.ImagePath = "Abdellah";

            if (Contact1.save())
            {

                Console.WriteLine(@"contact added successfully and the ID is {0} ",Contact1.ID);

            }
            else { Console.WriteLine("Error 404"); }
            

        }

        static void DeleteContact(int ID)

        {

            if (clsContact.DeleteContact(ID))

                Console.WriteLine("Contact Deleted Successfully.");
            else
                Console.WriteLine("Faild to delete contact.");

        }

        static void ListContacts()
        {

            DataTable dataTable = clsContact.GetAllContacts();

            Console.WriteLine("Contacts Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]},  {row["FirstName"]} {row["LastName"]}");
            }

        }

        static void Main(string[] args)
        {

            //FindContact(1);
            //addNewContact();
            //UpdateContact(15);
            //DeleteContact(16);

            ListContacts();

            Console.ReadKey();

        }
    }
}
