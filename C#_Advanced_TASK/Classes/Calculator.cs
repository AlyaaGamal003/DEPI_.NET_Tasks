using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Create a simple calculator using delegates for different mathematical operations.
    public delegate double MathOperation(double a, double b);

    internal class Calculator
    {
        public double Execute(double a, double b, MathOperation operation)
        {
            return operation(a, b);
        }
    }
}
