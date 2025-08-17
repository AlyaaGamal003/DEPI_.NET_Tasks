namespace DEPI_OOP_Bank_Project
{
    internal class Program
    {

        static void Main(string[] args)
        {
            #region simple start
            Console.WriteLine("=== Welcome to Alyaa Bank System ===");
            Console.Write("Enter Bank Name: ");
            string bankName = Console.ReadLine();
            Console.Write("Enter Branch Code: ");
            int branchCode = int.Parse(Console.ReadLine());
            Bank bank = new Bank(bankName, branchCode);
            #endregion
            #region menu
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n=== Main Menu ===");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Update Customer");
                Console.WriteLine("3. Search Customer");
                Console.WriteLine("4. Remove Account");
                Console.WriteLine("5. Create Account");
                Console.WriteLine("6. Deposit");
                Console.WriteLine("7. Withdraw");
                Console.WriteLine("8. Transfer");
                Console.WriteLine("9. Show Transactions");
                Console.WriteLine("10. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                #endregion
                #region Switch
                switch (choice)
                {
                    // add customer
                    case "1":
                        Console.Write("Enter Full Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter National ID: ");
                        string nid = Console.ReadLine();
                        Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                        DateTime dob = DateTime.Parse(Console.ReadLine());
                        bank.AddCustomer(name, nid, dob);
                        break;

                    case "2": // Update Customer
                        Console.Write("Enter National ID to Update: ");
                        string updNid = Console.ReadLine();
                        Console.Write("Enter New Name (if u will not just click enter): ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter New Date of Birth (yyyy-mm-dd, enter to skip): ");
                        string dobInput = Console.ReadLine();

                        if (!string.IsNullOrEmpty(newName) && !string.IsNullOrEmpty(dobInput))
                        {
                            bank.Update_Name_DateOfBirth(updNid, newName, DateTime.Parse(dobInput));
                        }
                        else if (!string.IsNullOrEmpty(newName))
                        {
                            bank.UpdateName(updNid, newName);
                        }
                        else if (!string.IsNullOrEmpty(dobInput))
                        {
                            bank.UpdateDateOfBirth(updNid, DateTime.Parse(dobInput));
                        }
                        else
                        {
                            Console.WriteLine("No updates provided 👀");
                        }
                        break;

                    case "3": // Search Customer
                        Console.WriteLine("Search by: \n 1. National ID \n 2. Full Name");
                        string searchOption = Console.ReadLine();
                        if (searchOption == "1")
                        {
                            Console.Write("Enter National ID: ");
                            bank.searchCustomerBynationalID(Console.ReadLine());
                        }
                        else
                        {
                            Console.Write("Enter Full Name: ");
                            bank.searchCustomerByfullName(Console.ReadLine());
                        }
                        break;

                    // Remove Account
                    case "4":
                        Console.Write("Enter Account Number to Remove: ");
                        int accNum = int.Parse(Console.ReadLine());
                        bank.RemoveAccount(accNum);
                        break;

                    case "5": // Create Account
                        Console.Write("Enter Customer National ID: ");
                        string custNid = Console.ReadLine();
                        Console.WriteLine("Choose Account Type:\n 1. Savings \n 2. Current");
                        string accType = Console.ReadLine();
                        Console.Write("Enter Initial Balance: ");
                        decimal initBalance = decimal.Parse(Console.ReadLine());

                        Accounts newAccount = null;

                        if (accType == "1")
                        {
                            Console.Write("Enter Interest Rate (%): ");
                            decimal rate = decimal.Parse(Console.ReadLine());
                            newAccount = new SavingAccountcs(initBalance, rate);
                        }
                        else if (accType == "2")
                        {
                            Console.Write("Enter Overdraft Limit: ");
                            decimal overdraft = decimal.Parse(Console.ReadLine());
                            newAccount = new CurrentAccount(initBalance, overdraft);
                        }

                        if (newAccount != null)
                        {
                            // give account to the customer
                            foreach (var customer in bank._customers)
                            {
                                if (customer.NationalID == custNid)
                                {
                                    customer.accounts.Add(newAccount);
                                    bank.accounts.Add(newAccount);
                                    Console.WriteLine($"Account #{newAccount.AccountNumber} created for {customer.FullName}");
                                    break;
                                }
                            }
                        }
                        break;

                    // Deposit
                    case "6":
                        Console.Write("Enter Account Number: ");
                        int depAccNo = int.Parse(Console.ReadLine());
                        Console.Write("Enter Amount: ");
                        decimal depAmount = decimal.Parse(Console.ReadLine());

                        foreach (var acc in bank.accounts)
                        {
                            if (acc.AccountNumber == depAccNo)
                            {
                                acc.Deposit(depAmount);
                                Console.WriteLine("Deposit successful!");
                                break;
                            }
                        }
                        break;

                    case "7": // Withdraw
                        Console.Write("Enter Account Number: ");
                        int wAccNo = int.Parse(Console.ReadLine());
                        Console.Write("Enter Amount: ");
                        decimal wAmount = decimal.Parse(Console.ReadLine());

                        foreach (var acc in bank.accounts)
                        {
                            if (acc.AccountNumber == wAccNo)
                            {
                                acc.Withdraw(wAmount);
                                Console.WriteLine("Withdraw successful!");
                                break;
                            }
                        }
                        break;

                    case "8": // Transfer
                        Console.Write("Enter From Account Number: ");
                        int fromAcc = int.Parse(Console.ReadLine());
                        Console.Write("Enter To Account Number: ");
                        int toAcc = int.Parse(Console.ReadLine());
                        Console.Write("Enter Amount: ");
                        decimal tAmount = decimal.Parse(Console.ReadLine());

                        Accounts from = null, to = null;
                        foreach (var acc in bank.accounts)
                        {
                            if (acc.AccountNumber == fromAcc) from = acc;
                            if (acc.AccountNumber == toAcc) to = acc;
                        }
                        if (from != null && to != null) { from.Transfer(to, tAmount); }
                        else { Console.WriteLine("Invalid accounts!"); }
                        break;

                    // Show Transactions
                    case "9":
                        Console.Write("Enter Account Number: ");
                        int tAccNo = int.Parse(Console.ReadLine());
                        foreach (var acc in bank.accounts)
                        {
                            if (acc.AccountNumber == tAccNo)
                            {
                                acc.ShowTransactions();
                                break;
                            }
                        }
                        break;

                    case "10":
                        exit = true;
                        Console.WriteLine("Exiting system... Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again!");
                        break;
                }

            }
                #endregion 

        }
    }
}
