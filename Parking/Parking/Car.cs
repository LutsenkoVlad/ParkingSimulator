using Parking.Enums;
using System;

namespace Parking
{
    internal class Car
    {
        public decimal Balance { get; set; }

        public Guid Id { get; set; }

        public CarType CarType { get; set; }

        public void AddMoney(decimal money)
        {
            if(money > 0)
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
