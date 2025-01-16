namespace S10268411_PRG2Assignment
{
    internal class Program
    {
        private static Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
        private static Dictionary<string, BoardingGate> boardingGatesDict = new Dictionary<string, BoardingGate>();
        private static Dictionary<string, Flight> flightsDict = new Dictionary<string, Flight>();

        static void Main(string[] args)
        {
            InitialiseValues();
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=============================================");
                Console.WriteLine("Welcome to Changi Airport Terminal 5");
                Console.WriteLine("=============================================");
                Console.WriteLine("1. List all Flights");
                Console.WriteLine("2. List Boarding Gates");
                Console.WriteLine("3. Assign a Boarding Gate to a Flight");
                Console.WriteLine("4. Create Flight");
                Console.WriteLine("5. Display Airline Flights");
                Console.WriteLine("6. Modify flight details");
                Console.WriteLine("7. Display Flight Schedule");
                Console.WriteLine("0. Exit");
                Console.WriteLine();
                Console.WriteLine("Please select an option: ");
                if (Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("=============================================");
                            Console.WriteLine("List of Flights for Changi Airport Terminal 5");
                            Console.WriteLine("=============================================");
                            Console.WriteLine($"{"Flight Number",-18}{"Airline Name",-22}{"Origin",-22}{"Destination",-21}{"Expected Departure/Arrival Time"}");
                            foreach (Flight flight in flightsDict.Values)
                            {
                                Console.WriteLine($"{flight.FlightNumber,-18}{airlineDict[flight.FlightNumber.Substring(0, 2)].Name,-22}{flight.Origin,-22}{flight.Destination,-21}{flight.ExpectedTime.ToString(@"g")}");
                            }
                            break;

                        case 2:
                            Console.WriteLine("=============================================");
                            Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
                            Console.WriteLine("=============================================");
                            Console.WriteLine($"{"Gate Name",-14}{"DDJB",-10}{"CFFT",-10}{"LWTT"}");
                            foreach (BoardingGate boardingGate in boardingGatesDict.Values)
                            {
                                Console.WriteLine($"{boardingGate.GateName,-14}{boardingGate.SupportsDDJB,-10}{boardingGate.SupportsCFFT,-10}{boardingGate.SupportsLWTT,-10}");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Not implemented - solo project");
                            break;

                        case 4:
                            break;

                        case 5:
                            break;

                        case 6:
                            break;

                        case 7:
                            break;

                        case 0:
                            Console.WriteLine("Goodbye!");
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid option!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option!");
                }
            }
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
                BoardingGate boardingGateObject = new BoardingGate(
                    boardingGateArray[0], 
                    Convert.ToBoolean(boardingGateArray[1]), 
                    Convert.ToBoolean(boardingGateArray[2]), 
                    Convert.ToBoolean(boardingGateArray[3])
                );

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
                Flight flightObject;
                switch (flightArray?[4] ?? "")
                {
                    case "":
                    case " ":
                        flightObject = new NORMFlight(flightArray[0], flightArray[1], flightArray[2], Convert.ToDateTime(flightArray[3]));
                        break;

                    case "LWTT":
                        flightObject = new LWTTFlight(flightArray[0], flightArray[1], flightArray[2], Convert.ToDateTime(flightArray[3]));
                        break;

                    case "DDJB":
                        flightObject = new DDJBFlight(flightArray[0], flightArray[1], flightArray[2], Convert.ToDateTime(flightArray[3]));
                        break;

                    case "CFFT":
                        flightObject = new CFFTFlight(flightArray[0], flightArray[1], flightArray[2], Convert.ToDateTime(flightArray[3]));
                        break;

                    default:
                        throw new ArgumentException("Invalid request code");
                }
                flightsDict.Add(flightArray[0], flightObject);
            }
            Console.WriteLine($"{flightsDict.Count} Flights Loaded!");
        }
    }
}
