using Parking.Logger;
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

        void SetSettings(ISettings settings, BaseLogger logger);

        void AddCar();

        void RemoveCar();
        void AddCar(Car car);
        void AddMoneyToCar();
        void GetFreeParkingSpaces();

        void GetNotFreeParkingSpaces();

        void GetTransactionsForLastMinute();
        void GetPaymentFromCar();
        void LogTransactionEveryMinute();
        void GetBalance();
        void ShowLog();
        void ShowPrices();
        void ShowTransactionSumForOneMinute();
    }
}
