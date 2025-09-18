using System;


namespace DEPI_C_AdvancedTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Note: Uncomment the Console.WriteLine statements to see the output when running the program.

            // Q1: Test the PhoneBook class with indexer
            //PhoneBook phoneBook = new PhoneBook();
            //phoneBook["Alyaa"] = "123-456-7890";
            //phoneBook["Gamal"] = "987-654-3210";
            //phoneBook["Ahmed"] = "555-555-5555";
            //Console.WriteLine(phoneBook["Alyaa"]); // Output: 123-456-7890
            /*******************************************************************************************/

            // Q2: Test the WeeklySchedule class with indexer
            //WeeklySchedule schedule = new WeeklySchedule();
            //schedule["Monday"] = "C# Class at 10 AM";
            //schedule["Tuesday"] = "SQL Class at 11 AM";
            //schedule["Wednesday"] = "API Class at 1 PM";
            //Console.WriteLine(schedule["Monday"]); // Output: Math Class at 10 AM
            /*******************************************************************************************/

            // Q3: Test the Matrix class with indexer

            //Matrix A = new Matrix(2, 2);
            //A[0, 0] = 1; A[0, 1] = 2;
            //A[1, 0] = 3; A[1, 1] = 4;
            //Matrix B = new Matrix(2, 2);
            //B[0, 0] = 5; B[0, 1] = 6;
            //B[1, 0] = 7; B[1, 1] = 8;
            //Console.WriteLine("Matrix A:");
            //A.Print();
            //Console.WriteLine("Matrix B:");
            //B.Print();
            //Console.WriteLine("After Addition Matrix C = A + B:");
            //var C = A + B;
            //C.Print();
            //var D = C - B;
            //Console.WriteLine("After Subtraction Matrix D = C - B:");
            //D.Print();
            //var E = D * B;
            //Console.WriteLine("After Manpulatin Matrix E = D x B:");
            //E.Print();
            /*******************************************************************************************/

            // Q4: Test Stack Class
            //var list = new Stack<int>();
            //list.Push(10);
            //list.Push(20);
            //list.Push(30);
            //Console.WriteLine("Stack Items:");
            //list.PrintItems();
            //Console.WriteLine("Peek Item:");
            //Console.WriteLine(list.Peek()); // Output: 30
            //Console.WriteLine("Pop Items:");
            //Console.WriteLine(list.Pop());  // Output: 30
            //Console.WriteLine(list.Pop());  // Output: 20
            //Console.WriteLine(list.Pop());  // Output: 10
            //list.PrintItems();  // nothing to print
            /*******************************************************************************************/

            // Q5: Test Generic Pair Class
            //var pair = new Pair<string, int>("Alyaa", 25);
            //Console.WriteLine($"Name: {pair.Value1}, Age: {pair.Value2}"); // Output: Name: Alyaa, Age: 25
            /*******************************************************************************************/

            //Q6: Test Generic Constraint
            //var cache = new Cache<string, string>();


            //cache.Add("token", "Ag74", TimeSpan.FromSeconds(3));
            //cache.Add("user", "Alyaa", TimeSpan.FromSeconds(5));


            //if (cache.TryGet("token", out var value1))
            //    Console.WriteLine($"Token found: {value1}");   // abc123
            //else
            //    Console.WriteLine("Token not found");

            //if (cache.TryGet("user", out var value2))
            //    Console.WriteLine($"User found: {value2}");    // Alyaa
            //else
            //    Console.WriteLine("User not found");

            //Console.WriteLine("Wait 4 seconds");
            //Thread.Sleep(4000);

            //if (cache.TryGet("token", out var expiredValue))
            //    Console.WriteLine($"Token found again: {expiredValue}");
            //else
            //    Console.WriteLine("Token expired");   // expired

            //if (cache.TryGet("user", out var stillValid))
            //    Console.WriteLine($"User still valid: {stillValid}"); // Alyaa
            //else
            //    Console.WriteLine("User expired");

            /*******************************************************************************************/

            // Q7: Test Generic Method to Convert List Types
            //var words = new List<string> { "Hello", "World", "CSharp" };
            //var lengths = Converter.ConvertList(words, w => w.Length);
            //Console.WriteLine(string.Join(", ", lengths));

            /*******************************************************************************************/

            // Q9: Test Contact Manager
            //ContactManager cm = new ContactManager();
            //cm.AddContact("Alyaa", "123-456-7890");
            //cm.AddContact("Gamal", "987-654-3210");
            //Console.WriteLine(cm.SearchContact("Alyaa")); // Output: 123-456-7890
            //cm.DisplayAllContacts();

            /*******************************************************************************************/
            // Q10: Test the ShoppingCart class
            //ShoppingCart cart = new ShoppingCart();

            //cart.AddItem("Apple", 3);
            //cart.AddItem("Banana", 2);
            //cart.AddItem("Apple", 1); // for increase quantity

            //cart.ViewCart();

            //cart.ApplyDiscount("SUMMER10");
            //cart.ApplyDiscount("SUMMER10"); // applyed once 

            //cart.ViewCart();

            //cart.RemoveItem("Banana");
            //cart.ViewCart();

            /*******************************************************************************************/

            // Q11: Test the nullable integers class
            //var numbers1 = new List<int?> { 1, 2, 3, null, 4 };
            //var numbers2 = new List<int?> { null, null, null };

            //Console.WriteLine("Average 1: " + (NullableIntegers.AverageNullable(numbers1) ?? double.NaN));
            //Console.WriteLine("Average 2: " + (NullableIntegers.AverageNullable(numbers2)?.ToString() ?? "No values"));

            /*******************************************************************************************/
            // Q12
            //var person1 = new Person("Alyaa", "Gamal", dob: new DateTime(1998, 5, 20));
            //var person2 = new Person("Maryam", "Tarek", "Saeed");
            //var person3 = new Person("Yasmin", "Raef");

            //Console.WriteLine(person1);
            //Console.WriteLine(person2);
            //Console.WriteLine(person3);

            /*******************************************************************************************/

            // Q13
            //int num = 7;

            //Console.WriteLine($"{num} IsEven? {num.IsEven()}");
            //Console.WriteLine($"{num} IsOdd? {num.IsOdd()}");
            //Console.WriteLine($"{num} IsPrime? {num.IsPrime()}");
            //Console.WriteLine($"5 Factorial: {5.Factorial()}");
            //Console.WriteLine(num.ToRoman());

            /*******************************************************************************************/
            // Q14: 
            //DateTime today = DateTime.Now;

            //Console.WriteLine("Start of Week: " + today.StartOfWeek());
            //Console.WriteLine("End of Week: " + today.EndOfWeek());
            //Console.WriteLine("Start of Month: " + today.StartOfMonth());
            //Console.WriteLine("End of Month: " + today.EndOfMonth());
            //Console.WriteLine("Start of Year: " + today.StartOfYear());
            //Console.WriteLine("End of Year: " + today.EndOfYear());

            //DateTime birthDate = new DateTime(2000, 5, 10);
            //Console.WriteLine("Age: " + birthDate.CalculateAge());

            //Console.WriteLine("Is Business Day? " + today.IsBusinessDay());
            //Console.WriteLine("Add 5 Business Days: " + today.AddBusinessDays(5));

            /*******************************************************************************************/

            // Q16: Test the Calculator with Delegates
            //Calculator calc = new Calculator();
            //MathOperation add = (x, y) => x + y;
            //MathOperation subtract = (x, y) => x - y;
            //MathOperation multiply = (x, y) => x * y;
            //MathOperation divide = (x, y) => y != 0 ? x / y : double.NaN;

            //Console.WriteLine("Add: " + calc.Execute(10, 5, add));
            //Console.WriteLine("Subtract: " + calc.Execute(10, 5, subtract));
            //Console.WriteLine("Multiply: " + calc.Execute(10, 5, multiply));
            //Console.WriteLine("Divide: " + calc.Execute(10, 5, divide));

            /*******************************************************************************************/

            // Q17: Test the Notification System with Events
            //Notification notifier = new Notification();

            //notifier.OnNotify += SendEmail;
            //notifier.OnNotify += SendSMS;
            //notifier.OnNotify += SendPushNotification;

            //notifier.SendNotification("Your order has been shipped!");


            /*******************************************************************************************/

            // Q19: Test the Data Processing Pipeline with Delegates
            //DataProcessorPipeline pipeline = new DataProcessorPipeline();

            //pipeline.AddStep(ToUpperCase);
            //pipeline.AddStep(RemoveSpaces);
            //pipeline.AddStep(AppendSignature);
            //string input = "   hello world   ";
            //string result = pipeline.Execute(input);

            //Console.WriteLine("Final Result: " + result);


            /*******************************************************************************************/

            // Q20: Create lambda expressions to filter, transform, and aggregate a list of student grades.
            //List<int> grades = new List<int> { 55, 70, 85, 90, 40, 100, 65 };

            //// Filter
            //var passing = grades.Where(g => g >= 60).ToList();
            //Console.WriteLine("Passing Grades: " + string.Join(", ", passing));

            //// Transform
            //var curve = grades.Select(g => Math.Min(g + 5, 100)).ToList();
            //Console.WriteLine("Curved Grades: " + string.Join(", ", curve));

            //// Aggregate
            //double average = grades.Average();
            //Console.WriteLine("Average Grade: " + average);

            //// max & min
            //int max = grades.Max();
            //int min = grades.Min();
            //Console.WriteLine($"Max: {max}, Min: {min}");

            /*******************************************************************************************/
            //Q22:
            //var timer = new SimpleTimer(1000, 5); 

            //timer.OnTick += tick => Console.WriteLine($"Tick: {tick}");
            //timer.OnCompleted += () => Console.WriteLine("Timer Completed!");

            //timer.Start();

            //Console.WriteLine("Timer started... Press Enter to exit.");
            //Console.ReadLine();

            /*******************************************************************************************/
            ////Q29: Test the TransactionalDatabase
            //var db = new TransactionalDatabase();


            //db.ExecuteTransaction(data => data.Add("Item1"));
            //db.PrintData(); 
            //db.ExecuteTransaction(data =>
            //{
            //    data.Add("Item2");
            //    data.Add("Item3");
            //    throw new Exception("Something went wrong!");
            //});
            //db.PrintData(); 

            /*******************************************************************************************/

            //q30:
            //string username = "";
            //int age = 25;
            //var validator = new Validator();
            //validator.AddRule(() =>
            //{
            //    if (string.IsNullOrEmpty(username))
            //        throw new RequiredFieldException("Username");
            //});
            //validator.AddRule(() =>
            //{
            //    if (age < 18 || age > 60)
            //        throw new RangeException("Age", 18, 60);
            //});
            //try
            //{
            //    validator.Validate();
            //}
            //catch (ValidationException ex)
            //{
            //    Console.WriteLine("Validation Error: " + ex.Message);
            //}
        }

        static async Task Main()
        {
            // Q23: Test Async File Operations
            //string filePath = "example.txt";
            //string content = "Hello Async World!";

            //await FileHelper.WriteTextAsync(filePath, content);

            //string readContent = await FileHelper.ReadTextAsync(filePath);
            //Console.WriteLine("Read Content: " + readContent);
            /*******************************************************************************************/
            // Q24: Test Async API Calls
            //ApiService api = new ApiService();

            //Console.WriteLine("Fetching dashboard data...");
            //string result = await api.GetDashboardDataAsync();

            //Console.WriteLine("Dashboard Data:");
            //Console.WriteLine(result);
            /*******************************************************************************************/
            // Q25: 
            //var service = new BackgroundService();
            //service.Enqueue("Item1");
            //service.Enqueue("Item2");
            //service.Enqueue("Item3");      
            //var processingTask = service.StartProcessingAsync();
            //Console.WriteLine("Processing started. Press Enter to stop...");
            //Console.ReadLine();
            //service.Stop();
            //await processingTask;
            //Console.WriteLine("Processing stopped.");
            /*******************************************************************************************/

            //Q26: Test the ThreadSafeCounter

            //ThreadSafeCounter counter = new ThreadSafeCounter();
            //Task[] tasks = new Task[10];
            //for (int i = 0; i < 5; i++)
            //{
            //    tasks[i] = Task.Run(() => {
            //        for (int j = 0; j < 1000; j++) counter.Increment();
            //    });
            //    tasks[i + 5] = Task.Run(() => {
            //        for (int j = 0; j < 1000; j++) counter.Decrement();
            //    });
            //}

            //await Task.WhenAll(tasks);

            //Console.WriteLine("Final Counter Value: " + counter.GetCount());
            /*******************************************************************************************/

            // Q27:
            //var downloader = new FileDownloader();

            //var files = new Dictionary<string, string>
            //{
            //    { "https://picsum.photos/200/300?random=1", "file1.jpg" },
            //    { "https://picsum.photos/200/300?random=2", "file2.jpg" }
            //};

            //await downloader.DownloadFilesAsync(files);
            //string imageUrl = "https://picsum.photos/200/300";
            //string destinationPath = "downloaded_image.jpg";

            //await downloader.DownloadFileAsync(imageUrl, destinationPath);

            /*******************************************************************************************/
            // Q28: Test the EmailSender with Async and Retry Logic
            //var emails = new List<(string From, string To, string Subject, string Body)>
            //{
            //     ("sender@example.com", "recipient1@example.com", "Hello 1", "This is test email 1"),
            //     ("sender@example.com", "recipient2@example.com", "Hello 2", "This is test email 2")
            //};

            //var sender = new EmailSender("smtp.example.com", 587);
            //await sender.SendEmailsAsync(emails);

        }

        static void SendEmail(string message)
        {
            Console.WriteLine("Email: " + message);
        }

        static void SendSMS(string message)
        {
            Console.WriteLine("SMS: " + message);
        }

        static void SendPushNotification(string message)
        {
            Console.WriteLine("Push Notification: " + message);
        }
        // pipeline 
        static string ToUpperCase(string input) => input.ToUpper();
        static string RemoveSpaces(string input) => input.Replace(" ", "");
        static string AppendSignature(string input) => input + "_PROCESSED";


    }
}

