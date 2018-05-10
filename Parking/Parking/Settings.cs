﻿using Parking.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    /// <summary>
    /// Customizing parking data
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// Every Timeout seconds charges money for parking space
        /// </summary>
        public int Timeout { get; private set; }

        /// <summary>
        /// Prices for parking for all cars
        /// </summary>
        public Dictionary<CarType, decimal> Prices { get; private set; }

        /// <summary>
        /// Amount of parking spaces
        /// </summary>
        public int ParkingSpace { get; private set; }

        /// <summary>
        /// Coefficient of fine
        /// </summary>
        public double Fine { get; private set; }

        private static readonly Lazy<Settings> lazy = new Lazy<Settings>(() => new Settings());

        public static Settings Instance => lazy.Value;
        
        private Settings() { }


        /// <summary>
        /// Set settings for parking data
        /// </summary>
        /// <param name="prices">Prices for parking for all cars</param>
        /// <param name="parkingSpace">Amount of parking spaces</param>
        /// <param name="fine">Coefficient of fine</param>
        /// <param name="timeout">Every Timeout seconds charges money for parking space</param>
        public void SetSettings(Dictionary<CarType, decimal> prices, int parkingSpace, double fine, int timeout = 3)
        {
            Prices = prices;
            ParkingSpace = parkingSpace;
            Fine = fine;
            Timeout = timeout;
        }
    }
}
