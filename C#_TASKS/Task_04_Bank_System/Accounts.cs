using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DEPI_OOP_Bank_Project
{
    internal abstract class Accounts
    {

        private static int _autoAccountNumber = 1;

        public int AccountNumber { get; private set; }
        public decimal balance { get;  set; }
        public DateTime DateOpened { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        #region Constructor
        public Accounts(decimal initBalance)
        {
            AccountNumber = _autoAccountNumber++;
            if (initBalance < 0) throw new ArgumentException("Balance cannot be negative.");
            balance = initBalance;
            DateOpened = DateTime.Now;
            Transactions = new List<Transaction>();
            
            Transactions.Add(new Transaction("OPENED", initBalance, balance, $"Account #{AccountNumber} created"));
        }
        
        #endregion
        #region Operations
        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0){throw new ArgumentException("Deposit must be positive!");}
            balance += amount;
            Transactions.Add(new Transaction("DEPOSIT", amount, balance, $"Deposited {amount}"));
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0) { throw new ArgumentException("Withdraw must be positive!"); }

            if (balance >= amount)
            {
                balance -= amount;
                Transactions.Add(new Transaction("WITHDRAW", amount, balance, $"Withdrew {amount}"));
            }
            else
            {
                Console.WriteLine("Insufficient funds!");
            }
        }

        public void Transfer(Accounts toAccount, decimal amount)
        {
            if (toAccount == null)
            {
                Console.WriteLine("Target account does not exist!");
                return;
            }

            if (balance >= amount)
            {
                this.Withdraw(amount);
                toAccount.Deposit(amount);
                Transactions.Add(new Transaction("TRANSFER OUT", amount, balance,
                   $"Transferred {amount} to Account #{toAccount.AccountNumber}"));

                toAccount.Transactions.Add(new Transaction("TRANSFER IN", amount, toAccount.balance,
                    $"Received {amount} from Account #{this.AccountNumber}"));
            }
            else
            {
                Console.WriteLine("Transfer failed: insufficient funds.");
            }
        }
        #endregion

        public void ShowTransactions()
        {
            Console.WriteLine($"\n--- Transactions for Account #{AccountNumber} ---");
            foreach (Transaction trans in Transactions)
            {
                trans.ShowTransaction();
            }
        }
        public abstract void AccountInfo();
    }

}
