using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Create a method that calculates the average of nullable integers, handling null values appropriately.
    internal class NullableIntegers
    {
      
        public static double? AverageNullable(List<int?> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                return null;

            var validNumbers = numbers.Where(n => n.HasValue).Select(n => n.Value).ToList();

            if (validNumbers.Count == 0)
                return null; 

            return validNumbers.Average();
        }
    }
}
