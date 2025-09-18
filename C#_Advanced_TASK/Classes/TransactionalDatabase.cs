using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Design a transaction rollback system that handles exceptions and maintains data consistency.
    internal class TransactionalDatabase
    {
        private readonly List<string> _data = new List<string>();
        public void ExecuteTransaction(Action<List<string>> transactionAction)
        {
            var snapshot = new List<string>(_data);
            try
            {
                transactionAction(_data); 
                Console.WriteLine("Transaction completed successfully.");
            }
            catch (Exception ex)
            {
                _data.Clear();
                _data.AddRange(snapshot);
                Console.WriteLine($"Transaction failed: {ex.Message}. Rolled back to previous state.");
            }
        }
        public void PrintData()
        {
            Console.WriteLine("Current Data: " + string.Join(", ", _data));
        }
    }
}
