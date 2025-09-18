using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Build a Person class with nullable properties (MiddleName, DateOfBirth) and safe string representations.
    internal class Person
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }  
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; } 

        public Person(string firstName, string lastName, string? middleName = null, DateTime? dob = null)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            DateOfBirth = dob;
        }

        public override string ToString()
        {
            string fullName = string.IsNullOrEmpty(MiddleName)
                ? $"{FirstName} {LastName}"
                : $"{FirstName} {MiddleName} {LastName}";

            string dobText = DateOfBirth.HasValue
                ? DateOfBirth.Value.ToShortDateString()
                : "Unknown DOB";

            return $"{fullName} (DOB: {dobText})";
        }
    }
}
