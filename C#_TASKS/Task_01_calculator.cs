namespace Task1_DEPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Calculater task
            Console.WriteLine("Hello!\r\nInput the first number:\n");
            int num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("\nInput the second number:\n");
            int num2 = int.Parse(Console.ReadLine());
            Console.WriteLine("\nWhat do you want to do with those numbers?\r\n[A]dd\r\n[S]ubtract\r\n[M]ultiply\r\n");
            char ch = char.Parse(Console.ReadLine());
            switch (ch)
            {
                case 'A':
                case 'a':
                    Console.WriteLine("\n" + num1 + " + " + num2 + " = " + (num1 + num2));
                    break;

                case 'S':
                case 's':
                    Console.WriteLine("\n" + num1 + " - " + num2 + " = " + (num1 - num2));
                    break;

                case 'M':
                case 'm':
                    Console.WriteLine("\n" + num1 + " * " + num2 + " = " + (num1 * num2));
                    break;

                default:
                    Console.WriteLine("\nInvalid option");
                    break;
            }
            Console.WriteLine("\nPress any key to close\r\n");
            Console.ReadKey();


            #endregion

        }
    }
}
