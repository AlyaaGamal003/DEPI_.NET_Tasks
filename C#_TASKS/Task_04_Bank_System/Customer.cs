using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DEPI_OOP_Bank_Project
{
    internal class Customer
    {
        public static int ID_AutoCount { get; set; }
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string NationalID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Accounts> accounts { get; private set; }

        #region Constructor
        public Customer(string FullName, string NationalID, DateTime DateOfBirth)
        {
            CustomerID = ID_AutoCount++;
            this.FullName = FullName ?? throw new ArgumentNullException(nameof(FullName), "FullName can't be null"); ;
            this.NationalID = NationalID ?? throw new ArgumentNullException(nameof(NationalID), "NationalID can't be null"); ;
            this.DateOfBirth = DateOfBirth;
            accounts = new List<Accounts>();
        }
        #endregion

        // method to calc the balance
        public decimal totalBalance()
        {
            decimal AllBalance = 0;
            foreach (Accounts acc in accounts)
            {
                AllBalance += acc.balance; 
            }
            return AllBalance;
        }
        // customer info
        #region customer info
        public void displayInfo()
        {
            Console.WriteLine($"Customer ID: {CustomerID}, Customer Name: {FullName}, NationalID: {NationalID}, Date Of Birth: {DateOfBirth.ToShortDateString()}");
        }
        #endregion

        #region remove 

        #endregion

    }
}
