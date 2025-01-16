using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268411_PRG2Assignment
{
    internal class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, double requestFee = 150) : base(flightNumber, origin, destination, expectedTime)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            return -1;
        }

        public override string ToString()
        {
            return $"{base.ToString()} \tRequest Fee: {RequestFee}";
        }
    }
}
