using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    internal class CustomExceptions
    {

    }
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }

    public class RequiredFieldException : ValidationException
    {
        public RequiredFieldException(string fieldName)
            : base($"The field '{fieldName}' is required.") { }
    }

    public class RangeException : ValidationException
    {
        public RangeException(string fieldName, int min, int max)
            : base($"The field '{fieldName}' must be between {min} and {max}.") { }
    }

    // Validator class
    public class Validator
    {
        private readonly List<Action> _rules = new List<Action>();

        public void AddRule(Action rule)
        {
            _rules.Add(rule);
        }

        public void Validate()
        {
            foreach (var rule in _rules)
            {
                rule(); // throws exception if rule fails
            }
        }
    }
}
