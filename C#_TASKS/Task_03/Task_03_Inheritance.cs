using System.Net;

namespace Task2_DEPI
{
    internal class Program
    {
        #region parent class - BankAccount
        public class BankAccount
        {
            //--------------------------------------- fields -----------------------------------------------------------//
            public const string BankCode = "BNK001";
            public readonly DateTime CreatedDate;

            private int _accountNumber;
            private string _fullName;
            private string _nationalID;
            private string _phoneNumber;
            private string _address;
            private decimal _balance;
            //------------------------------------------ Properties ------------------------------------------------------//
            public string FullName
            {
                get { return _fullName; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    { throw new ArgumentException("Full Name cannot be null or empty."); }
                    else { _fullName = value; }
                    
                }
            }  
            public string NationalID
            {
                get{ return _nationalID;}
                set
                {
                    if (value.Length != 14)
                    { throw new ArgumentException("National ID must be exactly 14 digits."); }
                    else { _nationalID = value; }
                }
            }
            public string PhoneNumber
            {
                get { return _phoneNumber; }
                set
                {
                    if (value.Length != 11 || !value.StartsWith("01"))
                    { throw new ArgumentException("Phone number must start with '01' and be exactly 11 digits."); }
                    else { _phoneNumber = value; }

                }
            }
           public string Address
            {
                get{return _address;}
                set{_address = value;}
            }
          
            public decimal Balance
            {
                get{return _balance;}
                set
                {
                    if (value < 0)
                    { throw new ArgumentException("Balance cannot be negative."); }
                    else { _balance = value; }
                     
                }
            }
            //----------------------------------------------- constructor --------------------------------------------//
            // Default constructor
            public BankAccount()
            {
                Console.WriteLine("Default Constructor Created");
                _accountNumber = 0;
                FullName = "Unknown";
                NationalID = "00000000000000";
                PhoneNumber = "01000000000";
                Address = "Not Provided";
                Balance = 0;
                CreatedDate = DateTime.Now;
            }
            //Parameterized constructor
            // Accepts full name, national ID, phone number, address, and balance.
            public BankAccount(int accountNumber, string fullName, string nationalID, string phoneNumber, string address, decimal balance)
            {
                Console.WriteLine("Parameterized Constructor Created");
                _accountNumber = accountNumber;
                FullName = fullName;
                NationalID = nationalID;
                PhoneNumber = phoneNumber;
                Address = address;
                Balance = balance;
                CreatedDate = DateTime.Now;
            }
            //Overloaded constructor
            public BankAccount(int accountNumber, string fullName, string nationalID, string phoneNumber, string address)
            {
                Console.WriteLine("Overloaded Constructor Created");
                _accountNumber = accountNumber;
                FullName = fullName;
                NationalID = nationalID;
                PhoneNumber = phoneNumber;
                Address = address;
                Balance = 0;
                CreatedDate = DateTime.Now;
            }

            //--------------------------------------------- Methods ---------------------------------------------------//
            public virtual void ShowAccountDetails()
            {
                Console.WriteLine("========== Account Details ==========");
                Console.WriteLine($"Bank Code       : {BankCode}");
                Console.WriteLine($"Account Number  : {_accountNumber}");
                Console.WriteLine($"Full Name       : {FullName}");
                Console.WriteLine($"National ID     : {NationalID}");
                Console.WriteLine($"Phone Number    : {PhoneNumber}");
                Console.WriteLine($"Address         : {Address}");
                Console.WriteLine($"Balance         : {Balance:C}");
                Console.WriteLine($"Created Date    : {CreatedDate}");
              
            }
            public bool IsValidNationalID()
            {
                return NationalID.Length == 14;
            }
            public bool IsValidPhoneNumber()
            {
                return PhoneNumber.Length == 11 && PhoneNumber.StartsWith("01");
            }
            public virtual decimal CalculateInterest() => 0;
        }

        #endregion
        #region child class - SavingAccount
        public class SavingAccount : BankAccount
        {
            public decimal InterestRate { get; set; }
            public SavingAccount(int accountNumber, string fullName, string nationalID, string phoneNumber, string address, decimal balance, decimal interestRate)
                : base(accountNumber, fullName, nationalID, phoneNumber, address, balance)
            {
                InterestRate = interestRate;
            }

            public override decimal CalculateInterest()
            {
                return Balance * InterestRate / 100;
            }

            public override void ShowAccountDetails()
            {
                base.ShowAccountDetails();
                Console.WriteLine($"Interest Rate   : {InterestRate}%");
                Console.WriteLine("=====================================");
            }
        }
        #endregion
        #region child class - CurrentAccount
        public class CurrentAccount : BankAccount
        {
            public decimal OverdraftLimit { get; set; }

            public CurrentAccount(int accountNumber, string fullName, string nationalID, string phoneNumber, string address, decimal balance, decimal overdraftLimit)
                : base(accountNumber, fullName, nationalID, phoneNumber, address, balance)
            {
                OverdraftLimit = overdraftLimit;
            }
            public override decimal CalculateInterest() => 0;
            public override void ShowAccountDetails()
            {
                base.ShowAccountDetails();
                Console.WriteLine($"Overdraft Limit : {OverdraftLimit:C}");
                Console.WriteLine("=====================================");

            }
        }
        #endregion

        #region main
        static void Main(string[] args)
        {
            SavingAccount saving = new SavingAccount(
                accountNumber: 1001,
                fullName: "Alyaa Gamal",
                nationalID: "29905231234567",
                phoneNumber: "01012345678",
                address: "Benha",
                balance: 5000m,
                interestRate: 5m
            );
            //saving.ShowAccountDetails();

            CurrentAccount current = new CurrentAccount(
                accountNumber: 100,
                fullName: "Maryam Tarek",
                nationalID: "30001011234567",
                phoneNumber: "01023456789",
                address: "Cairo",
                balance: 3000m,
                overdraftLimit: 1000m
            );
            //current.ShowAccountDetails();

            List<BankAccount> accounts = new List<BankAccount> { saving, current };
            foreach (var account in accounts)
            {
                account.ShowAccountDetails();
                Console.WriteLine($"Calculated Interest: {account.CalculateInterest():C}");
                Console.WriteLine("-------------------------------------\n");
            }
        }
        #endregion
    }
}
