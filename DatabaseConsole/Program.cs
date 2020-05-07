using System;
using DatabaseConsole.Data.Services;

namespace DatabaseConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var contactsDAL = new ContactsDAL();



            Console.WriteLine("Proszę podać imię kontaktów które mam pokazać: ");
            var name = Console.ReadLine();

            var myContacts = contactsDAL.GetContactsByName(name);

            foreach (var contact in myContacts)
            {
                Console.WriteLine(contact);
            }

            Console.ReadKey();
        }

    }
}
