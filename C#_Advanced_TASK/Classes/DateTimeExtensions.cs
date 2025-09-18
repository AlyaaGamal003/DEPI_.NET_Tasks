using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Build a complete DateTime extension library with methods for getting start/end of week/month/year, age calculation, and business day operations.
    internal static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return date.StartOfWeek(startOfWeek).AddDays(6).Date;
        }
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static DateTime StartOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        
        public static DateTime EndOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        public static int CalculateAge(this DateTime birthDate, DateTime? referenceDate = null)
        {
            DateTime today = referenceDate ?? DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age;
        }

        public static bool IsBusinessDay(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

       
        public static DateTime AddBusinessDays(this DateTime date, int days)
        {
            DateTime result = date;
            int addedDays = 0;
            while (addedDays < days)
            {
                result = result.AddDays(1);
                if (result.IsBusinessDay())
                    addedDays++;
            }
            return result;
        }
    }
}
