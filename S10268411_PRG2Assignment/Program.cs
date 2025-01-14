﻿namespace S10268411_PRG2Assignment
{
    internal class Program
    {
        private static Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
        private static Dictionary<string, BoardingGate> boardingGatesDict = new Dictionary<string, BoardingGate>();
        private static Dictionary<string, Flight> flightsDict = new Dictionary<string, Flight>();

        static void Main(string[] args)
        {
            InitialiseValues();
        }

        private static void InitialiseValues()
        {
            // Load airlines
            Console.WriteLine("Loading Airlines...");
            List<string> airlinesFile = File.ReadAllLines("airlines.csv").ToList();
            airlinesFile.RemoveAt(0); // Remove the headers of the csv file
            foreach (string airline in airlinesFile)
            {
                string[] airlineArray = airline.Split(',');
                Airline airlineObject = new Airline(airlineArray[0], airlineArray[1]);
                airlineDict.Add(airlineArray[1], airlineObject);
            }
            Console.WriteLine($"{airlineDict.Count} Airlines Loaded!");

            // Load boarding gates
            Console.WriteLine("Loading Boarding Gates...");
            List<string> boardingGatesFile = File.ReadAllLines("boardinggates.csv").ToList();
            boardingGatesFile.RemoveAt(0);
            foreach (string boardingGate in boardingGatesFile)
            {
                string[] boardingGateArray = boardingGate.Split(',');
                BoardingGate boardingGateObject = new BoardingGate(boardingGateArray[0], Convert.ToBoolean(boardingGateArray[1]), Convert.ToBoolean(boardingGateArray[2]), Convert.ToBoolean(boardingGateArray[3]));
                boardingGatesDict.Add(boardingGateArray[0], boardingGateObject);
            }
            Console.WriteLine($"{boardingGatesDict.Count} Boarding Gates Loaded!");

            // Load flights
            Console.WriteLine("Loading Flights...");
            List<string> flightsFile = File.ReadAllLines("flights.csv").ToList();
            flightsFile.RemoveAt(0);
            foreach (string flight in flightsFile)
            {
                string[] flightArray = flight.Split(',');
                Flight flightObject = new Flight(flightArray[0], flightArray[1], flightArray[2], Convert.ToDateTime(flightArray[3]));
                flightsDict.Add(flightArray[0], flightObject);
            }
            Console.WriteLine($"{flightsDict.Count} Flights Loaded!");
        }
    }
}
