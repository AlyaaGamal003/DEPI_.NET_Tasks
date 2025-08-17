using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_OOP_Bank_Project
{
    internal class Transaction
    {
        private static int _autoTransactionId = 1;

        public int TransactionId { get; private set; }
        public DateTime Date { get; private set; }
        public string Type { get; private set; }   // Deposit, Withdraw, Transfer
        public decimal Amount { get; private set; }
        public decimal BalanceAfter { get; private set; }
        public string Description { get; private set; }

        // Constructor
        public Transaction(string type, decimal amount, decimal balanceAfter, string description)
        {
            TransactionId = _autoTransactionId++;
            Date = DateTime.Now;
            Type = type;
            Amount = amount;
            BalanceAfter = balanceAfter;
            Description = description;
        }

        public void ShowTransaction()
        {
            Console.WriteLine($"[{Date}] #{TransactionId} {Type}: {Amount} | Balance After: {BalanceAfter} | {Description}");
        }
    }
}
