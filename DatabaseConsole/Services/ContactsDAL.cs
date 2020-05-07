using DatabaseConsole.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConsole.Services
{
    public class ContactsDAL
    {
        private const string connectionString = @"
            Data Source=(localdb)\MSSQLLocalDB; 
            Initial Catalog=Contacts;
            Integrated Security=True;
            Connect Timeout=30;";

        public List<Contact> GetContacts()
        {
            string query = "SELECT * FROM Contacts";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();

            SqlDataReader dataReader = cmd.ExecuteReader();

            List<Contact> contacts = new List<Contact>();

            while (dataReader.Read())
            {
                var temp = new Contact();
                temp.Id = Convert.ToInt32(dataReader["Id"]);
                temp.Name = dataReader["Name"].ToString();
                temp.Surname = dataReader["Surname"].ToString();
                temp.PhoneNumber = dataReader["PhoneNumber"].ToString();
                temp.Sex = (SexDictionary)Convert.ToInt32(dataReader["SexId"]);

                contacts.Add(temp);
            }
            conn.Close();

            return contacts;
        }

        public Contact GetContactById(int contactId)
        {
            string query = $@"SELECT * FROM 
                             Contacts
                             WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", contactId);

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var result = new Contact();

                    result.Name = reader["Name"].ToString();
                    result.Surname = reader["Surname"].ToString();
                    result.PhoneNumber = reader["PhoneNumber"].ToString();
                    result.Sex = (SexDictionary)Convert.ToInt32(reader["SexId"]);

                    return result;
                }

                return null;
            }
        }


        public void InsertContact(Contact contact)
        {
            string query = @"INSERT INTO Contacts (Name,Surname,PhoneNumber,SexId)
                             VALUES (@Name,@Surname,@PhoneNumber,@SexId)";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Name", contact.Name);
                command.Parameters.AddWithValue("@Surname", contact.Surname);
                command.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                command.Parameters.AddWithValue("@SexId", (int)contact.Sex);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateContact(Contact contact)
        {
            string query = @"UPDATE Contacts 
                             SET Name = @Name,
                             Surname = @Surname,
                             PhoneNumber = @PhoneNumber,
                             SexId = @SexId
                             WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Name", contact.Name);
                command.Parameters.AddWithValue("@Surname", contact.Surname);
                command.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                command.Parameters.AddWithValue("@SexId", (int)contact.Sex);
                command.Parameters.AddWithValue("@Id", contact.Id);

                conn.Open();

                command.ExecuteNonQuery();
            }
        }

        public void DeleteContact(int contactId)
        {
            string query = "DELETE FROM Contacts WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", contactId);

                conn.Open();

                command.ExecuteNonQuery();
            }
        }


        public List<Contact> GetContactsByName(string contactName)
        {
            string query = $@"SELECT * FROM Contacts
                            WHERE Name = @Name";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Name", contactName);

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                var result = new List<Contact>();

                while (reader.Read())
                {
                    var contact = new Contact();

                    contact.Name = reader["Name"].ToString();
                    contact.Surname = reader["Surname"].ToString();
                    contact.PhoneNumber = reader["PhoneNumber"].ToString();
                    contact.Sex = (SexDictionary)Convert.ToInt32(reader["SexId"]);

                    result.Add(contact);
                }

                return result;
            }
        }
    }
}
