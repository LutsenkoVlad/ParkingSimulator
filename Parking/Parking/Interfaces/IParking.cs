using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Parking.Interfaces
{
    internal interface IParking
    {
        IList<Car> Cars { get; set; }

        ObservableCollection<Transaction> Transactions { get; set; }

        decimal Balance { get; }

        ISettings Settings { get; }

        void SetSettings(ISettings settings);

        void AddCar();

        void RemoveCar();

        void GetFreeParkingSpaces();

        void GetNotFreeParkingSpaces();

        void GetTransactionsForLastMinute();
    }
}
