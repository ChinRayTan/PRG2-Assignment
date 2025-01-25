using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            if (Origin == "Singapore (SIN)")
                return 800 + 150;
            else
                return 500 + 150;
        }

        public override string ToString()
        {
            return $"Flight Type: CFFT \n{base.ToString()} \tRequest Fee: {RequestFee}";
        }
    }
}
