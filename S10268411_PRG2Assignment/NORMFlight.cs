using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268411_PRG2Assignment
{
    internal class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
        {

        }

        public double CalculateFees()
        {
            return -1;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
