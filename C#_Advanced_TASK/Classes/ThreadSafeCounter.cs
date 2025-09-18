using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    //Create a thread-safe counter class that multiple threads can increment and decrement safely.
    internal class ThreadSafeCounter
    {
        private int _count = 0;

        // Increment 
        public void Increment()
        {
            Interlocked.Increment(ref _count);
        }

        // Decrement 
        public void Decrement()
        {
            Interlocked.Decrement(ref _count);
        }

        // Get current count
        public int GetCount()
        {
            return _count;
        }

    }
}

