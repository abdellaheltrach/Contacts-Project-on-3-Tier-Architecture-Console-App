﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName,
    ref string Email, ref string Phone, ref string Address,
    ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];
                    ImagePath = (string)reader["ImagePath"];

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewContact(string FirstName,string LastName,
 string Email,  string Phone,  string Address,
 DateTime DateOfBirth,  int CountryID,  string ImagePath)
        {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Contacts (FirstName ,LastName ,Email  ,Phone ,Address ,DateOfBirth ,CountryID ,ImagePath)" +
                "  VALUES (@FirstName ,@LastName  ,@Email ,@Phone  ,@Address  ,@DateOfBirth ,@CountryID ,@ImagePath) SELECT SCOPE_IDENTITY()";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath == "")
            {
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }
            else
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);


            try
            {
                connection.Open();
                object obj = Command.ExecuteScalar();
                if (obj != null & int.TryParse(obj.ToString(), out int ID))
                {
                    ContactID = ID;
                }

            } 
            catch (Exception ex) {  } 
            finally 
            { 
                connection.Close();
            }
            return ContactID;
        }

        public static bool UpdateContact(int ID, string FirstName, string LastName,
     string Email,  string Phone,  string Address,
     DateTime DateOfBirth,  int CountryID,  string ImagePath)
        {
            bool IsUpdated= false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Contacts SET FirstName = @FirstName ,LastName = @LastName 
              ,Email = @Email ,Phone = @Phone ,Address = @Address ,DateOfBirth = @DateOfBirth 
              ,CountryID = @CountryID ,ImagePath = @ImagePath WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@ContactID", ID);


            if (ImagePath=="")
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            else
                command.Parameters.AddWithValue("@ImagePath", ImagePath);


            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    IsUpdated = true;
                }
                else
                {
                    IsUpdated = false;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return IsUpdated;


        }

        public static bool DeleteContact(int ContactID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Contacts 
                                where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", ContactID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }

        public static DataTable GetAllContacts()
        { 
            DataTable dataTable = new DataTable();

            SqlConnection connection= new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Contacts";

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                
                
                
                if (reader.HasRows) 
                {
                    dataTable.Load(reader);

                }
                reader.Close();


            }
            catch
            { 

            }
            finally 
            {
                connection.Close() ;
            }

            return dataTable;

        }

        public static bool IsExist(int ID)
        {
            bool IsFound= false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT IsFound = 1 FROM   Contacts where ContactID = @ContactID";

            SqlCommand Command = new SqlCommand( query, connection);


            Command.Parameters.AddWithValue ("@ContactID", ID);


            connection.Open();
            SqlDataReader reader = Command.ExecuteReader();



            IsFound = reader.HasRows;


            reader.Close();
            connection.Close ();


            return IsFound;






        }

        public static DataTable FindContactsByCountryName(string countryName)
        { 
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection (clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Contacts.ContactID, Contacts.FirstName, Contacts.LastName, Contacts.Email, Contacts.Phone, Contacts.Address, Contacts.DateOfBirth, Countries.CountryName, Contacts.ImagePath\r\nFROM     Contacts INNER JOIN\r\n                  Countries ON Contacts.CountryID = Countries.CountryID where  CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", countryName);

            try 
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                { 
                    dataTable.Load(reader);
                }

                reader .Close();

            }
            catch { }
            finally { 
                connection.Close();
            }

            return dataTable ;
        }
        public static bool IsCountryExist(string CountryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }



    }
}
