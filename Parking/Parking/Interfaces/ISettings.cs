using Parking.Enums;
using System;
using System.Collections.Generic;

namespace Parking.Interfaces
{
    interface ISettings
    {
        /// <summary>
        /// Every Timeout seconds charges money for parking space
        /// </summary>
        int Timeout { get; }

        /// <summary>
        /// Prices for parking for all cars
        /// </summary>
        Dictionary<CarType, decimal> Prices { get; }

        /// <summary>
        /// Amount of parking spaces
        /// </summary>
        int ParkingSpace { get; }

        /// <summary>
        /// Coefficient of fine
        /// </summary>
        double Fine { get; }

        /// <summary>
        /// Set settings for parking data
        /// </summary>
        /// <param name="prices">Prices for parking for all cars</param>
        /// <param name="parkingSpace">Amount of parking spaces</param>
        /// <param name="fine">Coefficient of fine</param>
        /// <param name="timeout">Every Timeout seconds charges money for parking space</param>
        void SetSettings(Dictionary<CarType, decimal> prices, int parkingSpace, double fine, int timeout);
    }
}
