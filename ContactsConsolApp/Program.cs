using System;
using System.Data;
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



        static void Main(string[] args)
        {

            FindContact(1);


            Console.ReadKey();

        }
    }
}
