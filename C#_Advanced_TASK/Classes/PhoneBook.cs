using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Create a PhoneBook class with an indexer that takes a person's name and returns their phone number.
    internal class PhoneBook
    {
        private Dictionary<string, string> phoneBook = new Dictionary<string, string>();
        public string this[string name]
        {
            get
            {
                if (phoneBook.ContainsKey(name))
                {
                    return phoneBook[name];
                }
                else
                {
                    return "Not Found";
                }
            }
            set
            {
                phoneBook[name] = value;
            }
        }
    }
}
