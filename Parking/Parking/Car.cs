using Parking.Enums;
using System;

namespace Parking
{
    internal class Car
    {
        private decimal balance;

        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {
                if (value >= 0)
                {
                    balance = value;
                }
            }
        }

        public Guid Id { get; set; }

        public CarType CarType { get; set; }

        public void AddMoney(decimal money)
        {
            Balance += money;
        }

        public Car(decimal balance, CarType carType)
        {
            Id = Guid.NewGuid();
            Balance = balance;
            CarType = carType;
        }
    }
}
