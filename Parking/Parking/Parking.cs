using Parking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking
{
    internal class Parking
    {
        public IList<Car> Cars { get; set; } = new List<Car>();

        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();

        public decimal Balance { get; private set; } = 0;

        public ISettings Settings { get; private set; }

        public void SetSettings(ISettings settings)
        {
            Settings = settings;
        }

        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

        public static Parking Instance => lazy.Value;

        private Parking() { }

        public void AddCar(Car car)
        {
            if (Settings.ParkingSpace <= Cars.Count)
                Cars.Add(car);
        }

        public void RemoveCar(Car car)
        {
            Cars.Remove(car);
        }

        public int GetFreeParkingSpaces => Settings.ParkingSpace - Cars.Count;

        public int GetNotFreeParkingSpaces => Cars.Count;

        public IEnumerable<Transaction> GetTransactionsForOneMinute()
        {
            DateTime now = DateTime.Now;
            return Transactions.Where(x => x.Date_Time.Day == now.Day && x.Date_Time.Hour == now.Hour && x.Date_Time.Minute == now.Minute);
        }
    }
}
