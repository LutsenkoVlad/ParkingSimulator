using Parking.Enums;
using Parking.Interfaces;
using Parking.Logger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Parking
{
    internal class Parking : IParking
    {
        public IList<Car> Cars { get; set; } = new List<Car>();
        public ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();
        public decimal Balance { get; private set; } = 0;
        public ISettings Settings { get; private set; }
        BaseLogger _logger;

        public void SetSettings(ISettings settings, BaseLogger logger)
        {
            Settings = settings;
            _logger = logger;
        }

        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

        public static Parking Instance => lazy.Value;

        private Parking() { Transactions.CollectionChanged += AddTransaction; }

        public void AddCar()
        {
            Console.WriteLine("Enter balance and type of car: \nBalance: ");
            try
            {
                int balance = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Choose type of car: " +
                                "\nPassenger = " + 0 +
                                "\nTruck = " + 1 +
                                "\nBus = " + 2 +
                                "\nMotorcycle = " + 3);

                CarType carType = (CarType)Convert.ToInt32(Console.ReadLine());

                AddCar(new Car(balance, carType));
            }
            catch (FormatException ex)
            {
                Console.WriteLine("You entered incorrect data. Try again");
            }
        }

        public void AddCar(Car car)
        {
            if (Settings.ParkingSpace > Cars.Count)
                Cars.Add(car);
        }

        public void RemoveCar()
        {
            Console.WriteLine("Choose car that you want take from parking");
            for (int i = 1; i < Cars.Count; i++)
            {
                Console.WriteLine($"{i} - id: {Cars[i].Id}, balance: {Cars[i].Balance}, type of car: {Cars[i].CarType}");
            }
            try
            {
                RemoveCar(Convert.ToInt32(Console.ReadLine()));
            }
            catch (FormatException ex)
            {
                Console.WriteLine("You entered incorrect data. Try again");
            }
        }

        public void GetFreeParkingSpaces()
        {
            Console.WriteLine($"free parking spaces - {Settings.ParkingSpace - Cars.Count}");
        }

        public void GetNotFreeParkingSpaces()
        {
            Console.WriteLine($"ocupied parking spaces - {Cars.Count}");
        }

        public void GetTransactionsForLastMinute()
        {
            DateTime now = DateTime.Now;
            var transactionsForOneLastMinute = Transactions.Where(x => x.Date_Time > DateTime.Now - TimeSpan.FromSeconds(60));
            foreach (var tran in transactionsForOneLastMinute)
            {
                Console.WriteLine($"car id - {tran.CarId}, sum - {tran.WriteOffs}, time - {tran.Date_Time}");
            }
        }

        public void ShowTransactionSumForOneMinute()
        {
            Console.WriteLine($"Sum of all transactions for last one minute - {GetTransactionSumForOneMinute()}");
        }

        public void AddMoneyToCar()
        {
            Console.WriteLine("Choose car that you want add money");
            for (int i = 1; i < Cars.Count; i++)
            {
                Console.WriteLine($"{i} - id: {Cars[i].Id}, balance: {Cars[i].Balance}, type of car: {Cars[i].CarType}");
            }
            try
            {
                int choosedCar = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter sum that you want add: \nSum: ");
                int sum = Convert.ToInt32(Console.ReadLine());
                Cars[choosedCar].AddMoney(sum);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("You entered incorrect data. Try again");
            }
        }

        public void GetPaymentFromCar()
        {
            //It has sense when will be a lot of cars in parking
            //Parallel.ForEach(Cars, (car, state, index) =>
            //{
            //    decimal sum = Settings.Prices.Where(x => x.Key == car.CarType).Select(x => x.Value).FirstOrDefault();
            //    if (car.Balance < sum) sum = sum * Settings.Fine;
            //    var transaction = new Transaction(car.Id, sum);
            //    Transactions.Add(transaction);
            //});
            foreach (var car in Cars)
            {
                decimal sum = Settings.Prices.Where(x => x.Key == car.CarType).Select(x => x.Value).FirstOrDefault();
                if (car.Balance < sum) sum = sum * Settings.Fine;
                var transaction = new Transaction(car.Id, sum);
                Transactions.Add(transaction);
            }
        }

        public void GetBalance() => Console.WriteLine($"Parking balance: {Balance}");

        public void ShowPrices()
        {
            foreach (var price in Settings.Prices)
            {
                Console.WriteLine($"{price.Key} - {price.Value}");
            }
        }

        public void LogTransactionEveryMinute()
        {
            _logger.Log(GetTransactionSumForOneMinute() + " - " + DateTime.Now);
        }

        public void ShowLog()
        {
            string[] lines = File.ReadAllLines("Transactions.log");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        #region private methods
        private void RemoveCar(int choosedCar)
        {
            if (Cars[choosedCar].Balance >= 0)
            {
                Cars.RemoveAt(choosedCar);
            }
            else
            {
                Console.WriteLine("You have to pay parking." + Environment.NewLine + "Then you can take the car.");
            }
        }

        private void AddTransaction(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var transaction = e.NewItems.OfType<Transaction>().First();
                Balance += transaction.WriteOffs;
                Car car = Cars.Where(x => x.Id == transaction.CarId).SingleOrDefault();
                car.Balance -= transaction.WriteOffs;
            }
        }

        private decimal GetTransactionSumForOneMinute()
            => Transactions.Where(x => x.Date_Time > DateTime.Now - TimeSpan.FromSeconds(60)).Sum(x => x.WriteOffs);
        #endregion
    }
}
