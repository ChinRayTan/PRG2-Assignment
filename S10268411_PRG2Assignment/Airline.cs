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
            if (Flights.ContainsKey(flight.FlightNumber)) return false;
            else
                Flights.Add(flight.FlightNumber, flight);
                return true;
        }

        public double CalculateFees()
        {
            double totalPrice = Flights.Values.Sum(x => x.CalculateFees());

            double discounts = 0;
            discounts += Math.Floor(Flights.Count / 3.0) * 350; // Every 3 flights discount
            if (Flights.Count > 5) discounts += totalPrice * 0.03; // More than 5 flights discount

            foreach (Flight flight in Flights.Values)
            {
                if (flight.ExpectedTime.Hour > 21 && flight.ExpectedTime.Hour < 11) discounts += 110; // 9pm < time < 11am discount

                if (new[] { "Dubai (DXB)", "Bangkok (BKK)", "Tokyo (NRT)" }.Contains(flight.Origin)) discounts += 25; // Dubai/Bangkok/Tokyo discount

                if (flight is NORMFlight) discounts += 50; // No request code discount
            }

            return totalPrice - discounts;
        }

        public bool RemoveFlight(Flight flight)
        {
            return Flights.Remove(flight.FlightNumber);
        }

        public override string ToString()
        {
            return $"Name: {Name} \tCode: {Code}";
        }
    }
}
