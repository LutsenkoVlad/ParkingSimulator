using Parking.Enums;
using Parking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    internal class Menu
    {
        private Parking _parking;

        private List<MenuOption> BuildMainMenu()
        {
            return new List<MenuOption> { new MenuOption("Add car to parking", _parking.AddCar),
                          new MenuOption("Delete car to parking", _parking.RemoveCar),
                          new MenuOption("Add money to car", _parking.AddMoneyToCar),
                          new MenuOption("Show all transactions for 1 minute", _parking.GetTransactionsForLastMinute),
                          new MenuOption("Show parking general balance", _parking.GetBalance),
                          new MenuOption("Show all free parking spaces", _parking.GetFreeParkingSpaces),
                          new MenuOption("Show all not free parking spaces", _parking.GetNotFreeParkingSpaces),
                          new MenuOption("Show Transactions log", _parking.ShowLog),
                          new MenuOption("Show prices for all car", _parking.ShowPrices),
                          new MenuOption("Show sum of transactions for 1 minute", _parking.ShowTransactionSumForOneMinute),
                          new MenuOption("Exit", ()=>Console.WriteLine("Goodbye"), true)
            };
        }

        private static void DisplayMainMenu(List<MenuOption> options)
        {
            //Console.Clear();
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

        public void MainMenu(Parking parking)
        {
            _parking = parking;
            bool exitMenu = false;

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(parking.Settings.Timeout);

            var timer = new System.Threading.Timer((e) =>
            {
                parking.GetPaymentFromCar();
            }, null, periodTimeSpan, periodTimeSpan);

            var timer2 = new System.Threading.Timer((e) =>
            {
                parking.LogTransactionForOneLastMinute();
            }, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));

            var menu = BuildMainMenu();

            while (!exitMenu)
            {
                DisplayMainMenu(menu);

                var menuChoice = GetMenuSelection(menu);

                menuChoice.ItemHandler?.Invoke();

                exitMenu = menuChoice.IsExitOption;
            }

            if(timer != null)
            {
                timer.Dispose();
            }
            if (timer2 != null)
            {
                timer.Dispose();
            }
        }
    }

    internal class MenuOption
    {
        public string ItemText { get; }
        public Action ItemHandler { get; }
        public bool IsExitOption { get; }

        public MenuOption(string itemText, Action itemHandler, bool isExitOption = false)
        {
            ItemText = itemText;
            ItemHandler = itemHandler;
            IsExitOption = isExitOption;
        }
    }
}
