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
    internal class Terminal
    {
        public string TerminalName { get; set; }

        public Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();

        public Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();

        public Dictionary<string, BoardingGate> BoardingGates = new Dictionary<string, BoardingGate>();

        public Dictionary<string, double> GateFees = new Dictionary<string, double>();

        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
        }

        public bool AddAirline(Airline airline)
        {
            if (Airlines.ContainsKey(airline.Code)) return false;
            else
                Airlines.Add(airline.Code, airline);
                return true;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (BoardingGates.ContainsKey(boardingGate.GateName)) return false;
            else
                BoardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            return Airlines.FirstOrDefault(x => x.Key == flight.FlightNumber.Substring(0, 2)).Value;
        }

        public void PrintAirlineFees()
        {
            foreach (Airline airline in Airlines.Values)
            {
                Console.WriteLine($"{airline.Name} ({airline.Code}): ${airline.CalculateFees()}");
            }
        }

        public override string ToString()
        {
            return $"Terminal Name: {TerminalName}";
        }
    }
}
