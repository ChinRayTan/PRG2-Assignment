using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268411_PRG2Assignment
{
    internal abstract class Flight
    {
        public string FlightNumber { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime ExpectedTime { get; set; }

        public string Status { get; set; }

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
        }

        public abstract double CalculateFees();

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber} \tOrigin: {Origin} \tDestination: {Destination} \tExpected Time: {ExpectedTime} \tStatus: {Status}";
        }
    }
}
