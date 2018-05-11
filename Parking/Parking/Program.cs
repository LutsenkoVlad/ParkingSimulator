using Parking.Enums;
using Parking.Interfaces;
using Parking.Logger;
using System.Collections.Generic;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            ISettings settings = SetSettings(Settings.Instance);
            IParking parking = Parking.Instance;
            parking.SetSettings(settings, new FileLogger("Transactions.log"));
            foreach (var car in GetCars())
            {
                parking.AddCar(car);
            }
            Menu menu = new Menu();
            menu.MainMenu(parking);
        }

        private static IEnumerable<Car> GetCars()
        {
            return new List<Car>
            {
                new Car(100,CarType.Bus),
                new Car(100,CarType.Motorcycle),
                new Car(100,CarType.Passenger),
                new Car(100,CarType.Truck),
                new Car(100,CarType.Motorcycle),
                new Car(0,CarType.Truck),
            };
        }

        private static ISettings SetSettings(ISettings settings)
        {
            var prices = new Dictionary<CarType, decimal>();
            prices.Add(CarType.Passenger, 5);
            prices.Add(CarType.Truck, 3);
            prices.Add(CarType.Bus, 2);
            prices.Add(CarType.Motorcycle, 1);
            settings.SetSettings(prices, 10, 5);
            return settings;
        }
    }
}
