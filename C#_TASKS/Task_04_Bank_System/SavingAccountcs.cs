using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_OOP_Bank_Project
{
    internal class SavingAccountcs: Accounts
    {
        public decimal InterestRate { get; private set; }

        public SavingAccountcs(decimal initialBalance, decimal interestRate)
            : base(initialBalance)
        {
            InterestRate = interestRate;
        }

        public void CalculateInterest()
        {
            decimal interest = balance * InterestRate / 100;
            balance += interest;
            Transactions.Add(new Transaction("INTEREST", interest, balance, $"Interest applied at {InterestRate}%"));
        }

        public override void AccountInfo()
        {
            Console.WriteLine($"Savings Account #{AccountNumber}, Balance: {balance}, Opened: {DateOpened}, Interest Rate: {InterestRate}%");
        }
    }
}
