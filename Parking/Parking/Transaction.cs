using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    internal class Transaction
    {
        public DateTime Date_Time { get; set; }

        public Guid CarId { get; set; }

        public decimal WriteOffs { get; set; }

        public Transaction(Guid carId, decimal writeOffs)
        {
            CarId = carId;
            WriteOffs = writeOffs;
            Date_Time = DateTime.Now;
        }
    }
}
