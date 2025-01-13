using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268411_PRG2Assignment
{
    internal class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }

        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public double CalculateFees()
        {
            return -1;
        }

        public override string ToString()
        {
            return $"{base.ToString()} \tRequest Fee: {RequestFee}";
        }
    }
}
