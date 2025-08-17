using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_OOP_Bank_Project
{
    internal class Bank
    {
        
        public string Name { get; set; }
        public int BranchCode  { get; set; }
        public readonly List<Customer> _customers = new List<Customer>();
        public readonly List<Accounts> accounts = new List<Accounts>();

        #region Constructors
        public Bank(string name, int branchCode)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name),"name can't be null"); 
            BranchCode = branchCode;
        }

        public Bank(string name, int branchCode, IEnumerable<Customer> customers) : this (name, branchCode)
        {
            if (customers != null) _customers.AddRange(customers);
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////////
        #region Add
        public void AddCustomer(string FullName, string nationalID, DateTime dateOfbirth)
        {
            if (string.IsNullOrEmpty(FullName)) throw new ArgumentNullException(nameof(FullName), "Name is required");
            if (string.IsNullOrEmpty(nationalID)) throw new ArgumentNullException(nameof(nationalID), "nationalID is required");
            bool exists = false;
            foreach (Customer customer in _customers){ 
                if (customer.NationalID == nationalID){
                    exists = true;
                    break; } }
            if (exists){
                Console.WriteLine("A customer with this National ID already exists!");
                return;}
            Customer newCustomer = new Customer(FullName, nationalID, dateOfbirth);
            _customers.Add(newCustomer);
            Console.WriteLine($"Customer {FullName} added successfully!");
        }
        #endregion
        #region remove
        public bool RemoveAccount(int accountNumber)
        {
            Accounts targetAccount = null;

            foreach (var acc in accounts)
            {
                if (acc.AccountNumber == accountNumber)
                {
                    targetAccount = acc;
                    break;
                }
            }

            if (targetAccount == null)
            {
                Console.WriteLine("Account not found!");
                return false;
            }

            if (targetAccount.balance != 0)
            {
                Console.WriteLine("Cannot remove account: balance must be zero.");
                return false;
            }

            accounts.Remove(targetAccount);
            Console.WriteLine($"Account #{accountNumber} removed successfully.");
            return true;
        }
        #endregion

        #region Search
        public void searchCustomerBynationalID(string nationalID)
        {
           
            bool exists = false;
            foreach (Customer customer in _customers){
                if (customer.NationalID == nationalID){
                    Console.WriteLine($"Customer found: {customer.NationalID}");
                    exists = true;
                    break;
                }
            }
            if (!exists){Console.WriteLine("Customer doesn't exist");}
        }

        public void searchCustomerByfullName(string fullName)
        {
            
            bool exists = false;
            foreach (Customer customer in _customers)
            {
                if (customer.FullName == fullName)
                {
                    Console.WriteLine($"Customer found: {customer.FullName}");
                    exists = true;
                    break;
                }
            }
            if (!exists){Console.WriteLine("Customer doesn't exist"); }
        }
        #endregion
        #region Update name and dob
        public void UpdateName(string nationalID,string newName)
        {
            foreach (Customer customer in _customers)
            {
                if (customer.NationalID == nationalID)
                {
                    customer.FullName = newName;
                    Console.WriteLine("Customer name updated successfully!");
                    return;
                }
               
            }
            Console.WriteLine("Customer not found!");
        }
        public void UpdateDateOfBirth(string nationalID, DateTime newDateOfBirth)
        {
            foreach (Customer customer in _customers)
            {
                if (customer.NationalID == nationalID)
                {
                    customer.DateOfBirth = newDateOfBirth;
                    Console.WriteLine("Customer Date Of Birth updated successfully!");
                    return;
                }
            }
            Console.WriteLine("Customer not found!");
        }
        public void Update_Name_DateOfBirth(string nationalID, string newName, DateTime newDateOfBirth)
        {
            foreach (Customer customer in _customers)
            {
                if (customer.NationalID == nationalID)
                {
                    customer.FullName = newName;
                    customer.DateOfBirth = newDateOfBirth;
                    Console.WriteLine("Customer Date Of Birth updated successfully!");
                    return;
                }
            }
            Console.WriteLine("Customer not found!");
        }

        #endregion
    }
}
