using System.Net;

namespace Task2_DEPI
{
    internal class Program
    {
        public class BankAccount
        {
            /// fields
            public const string BankCode = "BNK001";
            public readonly DateTime CreatedDate;

            private int _accountNumber;
            private string _fullName;
            private string _nationalID;
            private string _phoneNumber;
            private string _address;
            private decimal _balance;
            /////////////////////////////////////// Properties //////////////////////////////////////////
            public string FullName
            {
                get
                {
                    return _fullName;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    { Console.WriteLine("Full Name cannot be null or empty."); }
                    else { _fullName = value; }
                    
                }
            }
            
            public string NationalID
            {
                get
                {
                    return _nationalID;
                }
                set
                {
                    if (value.Length != 14)
                    { Console.WriteLine("National ID must be exactly 14 digits."); }
                    else { _nationalID = value; }
                }
            }
            
            public string PhoneNumber
            {
                get
                {
                    return _phoneNumber;
                }
                set
                {
                    if (value.Length != 11 || !value.StartsWith("01"))
                    { Console.WriteLine("Phone number must start with '01' and be exactly 11 digits."); }
                    { _phoneNumber = value; }
                       
                }
            }
          
           public string Address
            {
                get
                {
                    return _address;
                }
                set
                {
                    _address = value;
                }
            }
          
            public decimal Balance
            {
                get
                {
                    return _balance;
                }
                set
                {
                    if (value < 0)
                    { Console.WriteLine("Balance cannot be negative."); }
                    else { _balance = value; }
                     
                }
            }
            ////////////////////////////////////// constructor ///////////////////////////////////////////
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

            ////////////////////////////////////// Methods //////////////////////////////////////////
            public void ShowAccountDetails()
            {
                Console.WriteLine("========== Account Details ==========");
                Console.WriteLine($"{BankCode}");
                Console.WriteLine($"{_accountNumber}");
                Console.WriteLine($"{FullName}");
                Console.WriteLine($"{NationalID}");
                Console.WriteLine($"{PhoneNumber}");
                Console.WriteLine($"{Address}");
                Console.WriteLine($"{Balance:C}");
                Console.WriteLine($"{CreatedDate}");
                Console.WriteLine("=====================================\n");
            }
            public bool IsValidNationalID()
            {
                return NationalID.Length == 14 && ulong.TryParse(NationalID, out _);
            }
            public bool IsValidPhoneNumber()
            {
                return PhoneNumber.Length == 11 && PhoneNumber.StartsWith("01");
            }
        }
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount( 1001,"Alyaa Gamal","29905231234567", "01012345678","Cairo");
            BankAccount account2 = new BankAccount(1002, "Maryam Tarek","30001011234567","01123456789","Alexandria",5000);
            
            account1.ShowAccountDetails();
            
            account2.ShowAccountDetails();
        }
    }
}
