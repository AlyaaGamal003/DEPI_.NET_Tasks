using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Create a comprehensive set of extension methods for int type including IsEven(), IsOdd(), IsPrime(), ToRoman(), and Factorial().
    internal static class IntExtensions
    {
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }

        public static bool IsOdd(this int number)
        {
            return number % 2 != 0;
        }

        public static bool IsPrime(this int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
        public static long Factorial(this int number)
        {
            if (number < 0)
                throw new ArgumentException("Factorial is not defined for negative numbers.");

            long result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }

        public static string ToRoman(this int number)
        {
            if (number < 1 || number > 3999)
                throw new ArgumentOutOfRangeException("number", "Value must be between 1 and 3999.");

            var romanNumerals = new[]
            {
            new { Value = 1000, Symbol = "M" },
            new { Value = 900, Symbol = "CM" },
            new { Value = 500, Symbol = "D" },
            new { Value = 400, Symbol = "CD" },
            new { Value = 100, Symbol = "C" },
            new { Value = 90, Symbol = "XC" },
            new { Value = 50, Symbol = "L" },
            new { Value = 40, Symbol = "XL" },
            new { Value = 10, Symbol = "X" },
            new { Value = 9, Symbol = "IX" },
            new { Value = 5, Symbol = "V" },
            new { Value = 4, Symbol = "IV" },
            new { Value = 1, Symbol = "I" }
        };

            string result = "";
            int remaining = number;

            foreach (var item in romanNumerals)
            {
                while (remaining >= item.Value)
                {
                    result += item.Symbol;
                    remaining -= item.Value;
                }
            }

            return result;
        }
    }
}
