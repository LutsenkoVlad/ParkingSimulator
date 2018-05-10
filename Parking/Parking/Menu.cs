using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    internal static class Menu
    {
        public static ConsoleKeyInfo DisplayMenu()
        {
            Console.WriteLine("Welcome, to the parking");
            Console.WriteLine("1.Add car to parking");
            Console.WriteLine("2.Delete car to parking");
            Console.WriteLine("3.Show all transactions for 1 minute");
            Console.WriteLine("4.Show parking general balance");
            Console.WriteLine("5.Show all free parking spaces");
            Console.WriteLine("6.Show all not free parking spaces");
            Console.WriteLine("7.Show Transactions log");
            Console.WriteLine("8.Show prices for all car");
            Console.WriteLine("9.Show sum of transactions for 1 minute");
            return Console.ReadKey();
        }
    }
}
