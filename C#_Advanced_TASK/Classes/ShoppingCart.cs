using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Implement a shopping cart system using multiple collection types (List for items, Dictionary for quantities, HashSet for discounts).
    internal class ShoppingCart
    {
        private List<string> items = new List<string>();
        private Dictionary<string, int> quantities = new Dictionary<string, int>();
        private HashSet<string> discounts = new HashSet<string>();
       
        public void AddItem(string item, int quantity)
        {
            items.Add(item);

            if (quantities.ContainsKey(item))
                quantities[item] += quantity;
            else
                quantities[item] = quantity;

            Console.WriteLine($"{quantity} x {item} added.");
        }

        public bool RemoveItem(string item)
        {
            if (quantities.ContainsKey(item))
            {
                items.RemoveAll(i => i == item); // remove from list
                quantities.Remove(item);
                Console.WriteLine($"{item} removed from cart.");
                return true;
            }
            throw new Exception($"{item} not found in cart.");
        }

       
        public void ApplyDiscount(string code)
        {
            if (discounts.Add(code))
                Console.WriteLine($"Discount '{code}' applied.");
            else
                Console.WriteLine($"Discount '{code}' already applied.");
        }


        public void ViewCart()
        {
            Console.WriteLine("\n--- Shopping Cart ---");

            if (items.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            foreach (var kvp in quantities)
            {
                Console.WriteLine($"{kvp.Key} x {kvp.Value}");
            }

            Console.WriteLine("Discounts: " + (discounts.Count > 0 ? string.Join(", ", discounts) : "None"));
        }

    }
}
