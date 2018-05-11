using Parking.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Parking
{
    internal class Menu
    {
        private IParking _parking;

        private List<MenuOption> BuildMainMenu()
        {
            return new List<MenuOption> { new MenuOption("Add car to parking", _parking.AddCar),
                          new MenuOption("Take car from parking", _parking.RemoveCar),
                          new MenuOption("Add money to car", _parking.AddMoneyToCar),
                          new MenuOption("Show all transactions for 1 minute", _parking.GetTransactionsForLastMinute),
                          new MenuOption("Show parking general balance", _parking.GetBalance),
                          new MenuOption("Show free parking spaces", _parking.GetFreeParkingSpaces),
                          new MenuOption("Show occupied parking spaces", _parking.GetNotFreeParkingSpaces),
                          new MenuOption("Show log of transactions", _parking.ShowLog),
                          new MenuOption("Show prices for parking", _parking.ShowPrices),
                          new MenuOption("Show sum of transactions for last one minute", _parking.ShowTransactionSumForOneMinute),
                          new MenuOption("Exit", () => Console.WriteLine("Goodbye"), true)
            };
        }

        private static void DisplayMainMenu(List<MenuOption> options)
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Library.");
            Console.WriteLine("What do you want to do?");
            int optionCount = 1;
            foreach (var option in options)
            {
                Console.WriteLine($"{optionCount++}.{option.ItemText}");
            }
        }

        private static MenuOption GetMenuSelection(List<MenuOption> options)
        {
            do
            {
                string userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int selection) &&
                    selection > 0 &&
                    selection <= options.Count)
                {
                    return options[selection - 1];
                }

                Console.WriteLine("Sorry, Try again");
            }
            while (true);
        }

        public void MainMenu(IParking parking)
        {
            _parking = parking;
            bool exitMenu = false;

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(parking.Settings.Timeout);
            Timer parkingPaymentTimer = null;
            Timer logTransactionTimer = null;
            try
            {
                parkingPaymentTimer = new Timer((e) =>
                {
                    parking.GetPaymentFromCar();
                }, null, periodTimeSpan, periodTimeSpan);

                logTransactionTimer = new Timer((e) =>
                {
                    parking.LogTransactionEveryMinute();
                }, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));

                var menu = BuildMainMenu();

                while (!exitMenu)
                {
                    DisplayMainMenu(menu);

                    var menuChoice = GetMenuSelection(menu);

                    menuChoice.ItemHandler?.Invoke();

                    exitMenu = menuChoice.IsExitOption;
                }
            }
            finally
            {
                if (parkingPaymentTimer != null)
                {
                    parkingPaymentTimer.Dispose();
                }
                if (logTransactionTimer != null)
                {
                    logTransactionTimer.Dispose();
                }
            }
        }
    }
}
