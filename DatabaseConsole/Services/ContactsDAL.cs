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

        //TODO: Method to get Contact by Id
        public Contact GetContactById(int contactId)
        {
            return null;
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
    }
}
