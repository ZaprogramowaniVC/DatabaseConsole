using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConsole.Model;
using DatabaseConsole.Services;

namespace DatabaseConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var contactsDAL = new ContactsDAL();

            contactsDAL.DeleteContact(7002);
          
            var contacts = contactsDAL.GetContacts();

            foreach(Contact contact in contacts)
            {
                Console.WriteLine(contact.ToString());
            }

            Console.ReadKey();
        }

    }
}
