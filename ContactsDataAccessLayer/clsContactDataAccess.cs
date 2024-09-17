using System;
using System.Collections.Generic;
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




    }
}
