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

        public void SetSettings(ISettings settings)
        {
            Settings = settings;
            _logger = new FileLogger("Transactions.log");
        }


        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

        public static Parking Instance => lazy.Value;

        private Parking() { Transactions.CollectionChanged += AddTransaction; }


        public void AddCar()
        {
            Console.WriteLine("Enter balance and type of car: \nBalance: ");
            int balance = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Choose type of car: " +
                                "\nPassenger = " + 0 +
                                "\nTruck = " + 1 +
                                "\nBus = " + 2 +
                                "\nMotorcycle = " + 3);

            CarType carType = (CarType)Convert.ToInt32(Console.ReadLine());

            AddCar(new Car(balance, carType));
        }

        public void AddCar(Car car)
        {
            if (Settings.ParkingSpace > Cars.Count)
                Cars.Add(car);
        }

        public void RemoveCar()
        {
            Console.WriteLine("Choose car that you want remove from parking");
            for (int i = 1; i < Cars.Count; i++)
            {
                Console.WriteLine($"{i} - id: {Cars[i].Id}, balance: {Cars[i].Balance}, type of car: {Cars[i].CarType}");
            }
            int choosedCar = Convert.ToInt32(Console.ReadLine());
            RemoveCar(choosedCar);
        }

        private void RemoveCar(int choosedCar)
        {
            Cars.RemoveAt(choosedCar);
        }

        public void GetFreeParkingSpaces()
        {
            Console.WriteLine($"free parking spaces - {Settings.ParkingSpace - Cars.Count}");
        }

        public void GetNotFreeParkingSpaces()
        {
            Console.WriteLine($"not free parking spaces - {Cars.Count}");
        }

        public void GetTransactionsForLastMinute()
        {
            DateTime now = DateTime.Now;
            var transactionsForOneLastMinute = Transactions.Where(x => x.Date_Time > DateTime.Now - TimeSpan.FromSeconds(60));
            foreach (var tran in transactionsForOneLastMinute)
            {
                //Console.WriteLine($"car id - {tran.CarId}, sum - {tran.WriteOffs}, time - {tran.Date_Time.Date}:{tran.Date_Time.Hour}:{tran.Date_Time.Minute}:{tran.Date_Time.Second}");
                Console.WriteLine($"car id - {tran.CarId}, sum - {tran.WriteOffs}, time - {tran.Date_Time}");
            }
        }

        public void ShowTransactionSumForOneMinute()
        {
            Console.WriteLine($"Sum of all transactions for last one minute + {GetTransactionSumForOneMinute()}");
        }

        private decimal GetTransactionSumForOneMinute() => Transactions.Where(x => x.Date_Time > DateTime.Now - TimeSpan.FromSeconds(60)).Sum(x => x.WriteOffs);

        public void AddMoneyToCar()
        {
            Console.WriteLine("Choose car that you want add money");
            for (int i = 1; i < Cars.Count; i++)
            {
                Console.WriteLine($"{i} - id: {Cars[i].Id}, balance: {Cars[i].Balance}, type of car: {Cars[i].CarType}");
            }
            int choosedCar = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter sum that you want add: \nSum: ");
            int sum = Convert.ToInt32(Console.ReadLine());
            Cars[choosedCar].AddMoney(sum);
        }

        public void GetPaymentFromCar()
        {
            //Parallel.ForEach(Cars, (car, state, index) =>
            //{
            //    Balance += Settings.Prices.Where(x => x.Key == car.CarType).Select(x => x.Value).FirstOrDefault();
            //    car.Balance -= Settings.Prices.Where(x => x.Key == car.CarType).Select(x => x.Value).FirstOrDefault();
            //});
            foreach (var car in Cars)
            {
                decimal sum = Settings.Prices.Where(x => x.Key == car.CarType).Select(x => x.Value).FirstOrDefault();
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

        public void LogTransactionForOneLastMinute()
        {
            _logger.Log(GetTransactionSumForOneMinute() + " - " + DateTime.Now);
        }

        public void ShowLog()
        {
            string[] lines = System.IO.File.ReadAllLines("Transactions.log");
            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
