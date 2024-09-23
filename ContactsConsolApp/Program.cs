using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Security.Policy;
using ContactBusinessLayer;
using ContactsBusinessLayer;


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


        static void IsExist(int ID)

        {

            if (clsContact.IsExist(ID))

                Console.WriteLine("Contact whit ID  = " + ID + " is founded Successfully.");
            else
                Console.WriteLine("Failed to find the contact.");

        }

        //---Test Country Business

        static void testFindCountryByID(int ID)

        {
            clsCountry Country1 = clsCountry.Find(ID);

            if (Country1 != null)
            {
                Console.WriteLine(Country1.CountryName);

            }

            else
            {
                Console.WriteLine("Country [" + ID + "] Not found!");
            }
        }


        static void testFindCountryByName(string CountryName)

        {
            clsCountry Country1 = clsCountry.Find(CountryName);

            if (Country1 != null)
            {
                Console.WriteLine("Country [" + CountryName + "] isFound with ID = " + Country1.ID);

            }

            else
            {
                Console.WriteLine("Country [" + CountryName + "] Is Not found!");
            }
        }


        static void testIsCountryExistByID(int ID)

        {

            if (clsCountry.isCountryExist(ID))

                Console.WriteLine("Yes, Country is there.");

            else
                Console.WriteLine("No, Country Is not there.");

        }

        static void testIsCountryExistByName(string CountryName)

        {

            if (clsCountry.isCountryExist(CountryName))

                Console.WriteLine("Yes, Country is there.");

            else
                Console.WriteLine("No, Country Is not there.");

        }


        static void testAddNewCountry()


        {
            clsCountry Country1 = new clsCountry();

            Country1.CountryName = "Lebanon";


            if (Country1.Save())
            {

                Console.WriteLine("Country Added Successfully with id=" + Country1.ID);
            }

        }

        static void testUpdateCountry(int ID)

        {
            clsCountry Country1 = clsCountry.Find(ID);

            if (Country1 != null)
            {
                //update whatever info you want
                Country1.CountryName = "Lebanon2";


                if (Country1.Save())
                {

                    Console.WriteLine("Country updated Successfully ");
                }

            }
            else
            {
                Console.WriteLine("Country is you want to update is Not found!");
            }
        }

        static void testDeleteCountry(int ID)

        {

            if (clsCountry.isCountryExist(ID))

                if (clsCountry.DeleteCountry(ID))

                    Console.WriteLine("Country Deleted Successfully.");
                else
                    Console.WriteLine("Faild to delete Country.");

            else
                Console.WriteLine("Faild to delete: The Country with id = " + ID + " is not found");

        }

        static void ListCountries()
        {

            DataTable dataTable = clsCountry.GetAllCountries();

            Console.WriteLine("Coutries Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["CountryID"]},  {row["CountryName"]}");
            }

        }




        static void Main(string[] args)
        {

            //FindContact(1);
            //addNewContact();
            //UpdateContact(15);
            //DeleteContact(16);
            //ListContacts();
            //IsExist(10);




            testFindCountryByID(1);
            testFindCountryByID(100);
            testFindCountryByName("United States");
            testFindCountryByName("UK");

            testIsCountryExistByID(1);
            testIsCountryExistByID(100);

            testIsCountryExistByName("United States");
            testIsCountryExistByName("UK");



            Console.ReadKey();

        }
    }
}
