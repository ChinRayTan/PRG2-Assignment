//==========================================================
// Student Number   : S10268411
// Student Name	: Tan Chin Ray
// Partner Name	: -
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268411_PRG2Assignment
{
    internal class Airline
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();

        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public bool AddFlight(Flight flight)
        {
            return false;
        }

        public double CalculateFees()
        {
            return -1;
        }

        public bool RemoveFlight(Flight flight)
        {
            return false;
        }

        public override string ToString()
        {
            return $"Name: {Name} \tCode: {Code}";
        }
    }
}
