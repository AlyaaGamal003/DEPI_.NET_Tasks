using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_OOP_Bank_Project
{
    
    internal class CurrentAccount : Accounts
    {
        public decimal OverdraftLimit { get; private set; }

        public CurrentAccount(decimal initBalance, decimal overdraftLimit)
            : base(initBalance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal must be positive!");
                return;
            }

            if (balance + OverdraftLimit >= amount)
            {
                balance -= amount;
                Transactions.Add(new Transaction("WITHDRAW", amount, balance,
                    $"Withdrew {amount} (Overdraft allowed)"));
            }
            else
            {
                Console.WriteLine("Withdrawal failed: exceeds overdraft limit.");
            }
        }

        public override void AccountInfo()
        {
            Console.WriteLine($"Current Account #{AccountNumber}, Balance: {balance}, Opened: {DateOpened}, Overdraft Limit: {OverdraftLimit}");
        }
    }
}
