using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    //Create a contact manager using Dictionary<string, string> to store name-phone pairs with add, remove, and search functionality.
    internal class ContactManager
    {
        private Dictionary<string, string> contacts = new Dictionary<string, string>();
        public  void AddContact(string name, string phone)
        {
            if (contacts.ContainsKey(name))
                throw new Exception ("Contact already exists.");
                
            contacts.Add(name, phone);
        }

        public bool RemoveContact(string name)
        {
            if (!contacts.ContainsKey(name))
                throw new Exception("Contact not found.");
            return contacts.Remove(name);
        }
        public string SearchContact(string name)
        {
            if (contacts.ContainsKey(name))
            {
                return contacts[name];
            }
            else
            {
                return "Not Found";
            }
        }
        public void DisplayAllContacts()
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Name: {contact.Key}, Phone: {contact.Value}");
            }
        }
    }
}
