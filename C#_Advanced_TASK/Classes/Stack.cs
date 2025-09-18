using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Create a generic `Stack<T>` class with Push, Pop, and Peek methods.
    internal class Stack<T>
    {
        private List<T> items = new List<T>();
        public void Push(T item)
        {
            items.Add(item);
        }
        public T Pop()
        {
            if (items.Count == 0)
                throw new InvalidOperationException("Stack is empty.");
            T item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return item;   
        }
        public T Peek()
        {
            if (items.Count == 0)
                throw new InvalidOperationException("Stack is empty.");
            return items[items.Count - 1];
        }
        public void PrintItems()
        {
            Console.WriteLine("List Items: ");
            for (int i = items.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(items[i]);
            }
        }
    }
}
