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
            return false;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            return false;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            
        }

        public void PrintAirlineFees()
        {

        }

        public override string ToString()
        {
            return $"Terminal Name: {TerminalName}";
        }
    }
}
