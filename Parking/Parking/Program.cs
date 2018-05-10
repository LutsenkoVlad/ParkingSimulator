using Parking.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = Settings.Instance;
            var prices = new Dictionary<CarType, decimal>();
            prices.Add(CarType.Passenger, 1);
            prices.Add(CarType.Truck, 2);
            prices.Add(CarType.Bus, 3);
            prices.Add(CarType.Motorcycle, 4);
            settings.SetSettings(prices, 10, 5);
            Parking parking = Parking.Instance;
            parking.SetSettings(settings);

            ConsoleKeyInfo cki;
            do
            {
                Console.WriteLine("Welcome, to the best parking in the world");
                Console.WriteLine("1.Add car to the parking");
                Console.WriteLine("2.Take car from the parking");
                Console.WriteLine("3.Show all transactions for 1 minute");
                Console.WriteLine("4.Show parking general balance");
                Console.WriteLine("5.Show all free parking spaces");
                Console.WriteLine("6.Show all not free parking spaces");
                Console.WriteLine("7.Show Transactions log");
                Console.WriteLine("8.Show prices for all car");
                Console.WriteLine("9.Show sum of transactions for 1 minute");
                cki = Console.ReadKey();
                switch (cki.Key.ToString())
                {
                    case "1": { Console.WriteLine("Enter balance of car and choose type of car"); ;parking.AddCar(new Car(100,CarType.Passenger)); break; }
                    
                }
            } while (cki.Key != ConsoleKey.Escape);
            


            //ConsoleKeyInfo cki;
            //do
            //{
            //    cki = Menu.DisplayMenu();
            //}
            //while (cki.Key != ConsoleKey.Escape);
        }
    }
}
