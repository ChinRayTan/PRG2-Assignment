using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268411_PRG2Assignment
{
    internal class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
        {

        }

        public override double CalculateFees()
        {
            if (Origin == "Singapore (SIN)")
                return 800;
            else
                return 500;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
