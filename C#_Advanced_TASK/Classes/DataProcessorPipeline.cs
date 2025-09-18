using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Create a data processing pipeline using delegates for transformation and filtering operations.
    public delegate string DataProcessor(string input);
    internal class DataProcessorPipeline
    {
        private readonly List<DataProcessor> _steps = new List<DataProcessor>();
        public void AddStep(DataProcessor step)
        {
            _steps.Add(step);
        }
        public string Execute(string input)
        {
            string result = input;
            foreach (var step in _steps)
            {
                result = step(result);
            }
            return result;
        }

    }
}
