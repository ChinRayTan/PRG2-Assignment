//==========================================================
// Student Number   : S10268411
// Student Name	: Tan Chin Ray
// Partner Name	: -
//==========================================================


using System.Transactions;

namespace S10268411_PRG2Assignment
{
    internal class Program
    {
        private static Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
        private static Dictionary<string, BoardingGate> boardingGatesDict = new Dictionary<string, BoardingGate>();
        private static Dictionary<string, Flight> flightsDict = new Dictionary<string, Flight>();
        
        private static Terminal terminal5 = new Terminal("Terminal 5");

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
                Console.WriteLine("3. Assign a Boarding Gate to a Flight [UNAVAILABLE - SOLO]");
                Console.WriteLine("4. Create Flight");
                Console.WriteLine("5. Display Airline Flights");
                Console.WriteLine("6. Modify flight details [UNAVAILABLE - SOLO]");
                Console.WriteLine("7. Display Flight Schedule [UNAVAILABLE - SOLO]");
                Console.WriteLine("8. Process all unassigned flights to boarding gates");
                Console.WriteLine("9. Display total fee per airline for the day");
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
                            foreach (Flight flight in terminal5.Flights.Values)
                            {
                                Console.WriteLine($"{flight.FlightNumber,-18}{terminal5.Airlines[flight.FlightNumber.Substring(0, 2)].Name,-22}{flight.Origin,-22}{flight.Destination,-21}{flight.ExpectedTime.ToString(@"g")}");
                            }
                            break;

                        case 2:
                            Console.WriteLine("=============================================");
                            Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
                            Console.WriteLine("=============================================");
                            Console.WriteLine($"{"Gate Name",-14}{"DDJB",-10}{"CFFT",-10}{"LWTT"}");
                            foreach (BoardingGate boardingGate in terminal5.BoardingGates.Values)
                            {
                                Console.WriteLine($"{boardingGate.GateName,-14}{boardingGate.SupportsDDJB,-10}{boardingGate.SupportsCFFT,-10}{boardingGate.SupportsLWTT,-10}");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Not implemented - solo project");
                            break;

                        case 4:
                            bool loop = true;
                            while (loop == true)
                            {
                                try
                                {
                                    Console.Write("Enter Flight Number: ");
                                    string flightNumber = Console.ReadLine();
                                    if (terminal5.Airlines.Keys.FirstOrDefault(x => x.ToUpper().Contains(flightNumber.Substring(0, 2))) == null || string.IsNullOrEmpty(flightNumber))
                                    {
                                        throw new ArgumentException("Invalid flight number.");
                                    }

                                    Console.Write("Enter Origin: ");
                                    string origin = Console.ReadLine();
                                    if (string.IsNullOrEmpty(origin))
                                    {
                                        throw new ArgumentException("Invalid flight origin.");
                                    }

                                    Console.Write("Enter Destination: ");
                                    string destination = Console.ReadLine();
                                    if (string.IsNullOrEmpty(destination))
                                    {
                                        throw new ArgumentException("Invalid flight destination.");
                                    }

                                    Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                                    if (DateTime.TryParse(Console.ReadLine(), out DateTime expectedTime))
                                    {
                                        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
                                        string? requestCode = Console.ReadLine();

                                        Flight flightObject;
                                        switch (requestCode.ToUpper())
                                        {
                                            case "NONE":
                                                flightObject = new NORMFlight(flightNumber, origin, destination, expectedTime);
                                                break;

                                            case "LWTT":
                                                flightObject = new LWTTFlight(flightNumber, origin, destination, expectedTime);
                                                break;

                                            case "DDJB":
                                                flightObject = new DDJBFlight(flightNumber, origin, destination, expectedTime);
                                                break;

                                            case "CFFT":
                                                flightObject = new CFFTFlight(flightNumber, origin, destination, expectedTime);
                                                break;

                                            default:
                                                throw new ArgumentException("Invalid request code.");
                                        }
                                        terminal5.Flights.Add(flightNumber, flightObject);
                                        File.AppendAllText("flights.csv", string.Join(",", flightNumber, origin, destination, expectedTime.ToString("t"), requestCode));
                                        Console.WriteLine($"Flight {flightNumber} has been added!");
                                        Console.WriteLine();
                                        Console.Write("Would you like to add another flight [Y/N]: ");
                                        string response = Console.ReadLine();
                                        if (response.ToLower() != "y") 
                                        {
                                            loop = false;
                                            if (response.ToLower() != "n") Console.WriteLine("I'm taking that as a no.");  
                                        }
                                    }
                                    else
                                    {
                                        throw new ArgumentException("Invalid time.");
                                    }
                                } catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                            break;

                        case 5:
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("=============================================");
                                    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
                                    Console.WriteLine("=============================================");
                                    Console.WriteLine($"{"Airline Code",-17}{"Airline Name"}");
                                    foreach (KeyValuePair<string, Airline> keyValuePair in terminal5.Airlines)
                                    {
                                        Console.WriteLine($"{keyValuePair.Key,-17}{keyValuePair.Value.Name}");
                                    }
                                    Console.Write("Enter Airline Code: ");
                                    string airlineCode = Console.ReadLine();

                                    Airline airline = terminal5.Airlines.Values.FirstOrDefault(x => x.Code == airlineCode);
                                    if (airline == null) throw new ArgumentException("Invalid airline code.");

                                    Console.WriteLine("=============================================");
                                    Console.WriteLine($"List of Flights for {airline.Name}");
                                    Console.WriteLine("=============================================");
                                    Console.WriteLine($"{"Flight Number",-18}{"Airline Name",-22}{"Origin",-22}{"Destination",-21}{"Expected Departure/Arrival Time"}");
                                    
                                    foreach (Flight flight in airline.Flights.Values)
                                    {
                                        Console.WriteLine($"{flight.FlightNumber,-18}{airline.Name,-22}{flight.Origin,-22}{flight.Destination,-21}{flight.ExpectedTime.ToString(@"g")}");
                                    }

                                    break;
                                } catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                            break;

                        case 6:
                            Console.WriteLine("Not implemented - solo project");
                            break;

                        case 7:
                            Console.WriteLine("Not implemented - solo project");
                            break;

                        case 8:
                            int successfullyAssigned = 0;
                            int alredyAssigned = terminal5.BoardingGates.Values.Where(x => x.Flight != null).Count();
                            try
                            {
                                // Populate list of unassigned gates and flights
                                List<BoardingGate> unassignedGates = terminal5.BoardingGates.Values.ToList().Where(x => x.Flight == null).ToList();
                                Queue<Flight> unassignedFlights = new Queue<Flight>();

                                // Check which flights are unassigned
                                foreach (Flight flight in terminal5.Flights.Values)
                                {
                                    if (terminal5.BoardingGates.Values.FirstOrDefault(x => x.Flight == flight) == null)
                                    {
                                        unassignedFlights.Enqueue(flight);
                                    }
                                }

                                Console.WriteLine($"Unassigned flights: {unassignedFlights.Count}");
                                Console.WriteLine($"Unassigned boarding gates: {unassignedGates.Count}");

                                // Assign all the special flights first
                                while (unassignedFlights.Count - unassignedFlights.Where(x => x is NORMFlight).ToList().Count > 0)
                                {
                                    Flight flight = unassignedFlights.Dequeue();
                                    BoardingGate? suitableGate;

                                    if (flight is CFFTFlight)
                                    {
                                        suitableGate = unassignedGates.FirstOrDefault(x => x.SupportsCFFT == true);
                                        if (suitableGate == null) throw new Exception("No available CFFT capable gates for flight.");
                                    }
                                    else if (flight is DDJBFlight)
                                    {
                                        suitableGate = unassignedGates.FirstOrDefault(
                                            x => x.SupportsDDJB == true
                                        );

                                        if (suitableGate == null) throw new Exception("No available DDJB capable gates for flight.");
                                    }
                                    else if (flight is LWTTFlight)
                                    {
                                        suitableGate = unassignedGates.FirstOrDefault(
                                            x => x.SupportsLWTT == true
                                        );

                                        if (suitableGate == null) throw new Exception("No available LWTT capable gates for flight.");
                                    }
                                    else
                                    {
                                        unassignedFlights.Enqueue(flight);
                                        continue;
                                    }

                                    suitableGate.Flight = flight;
                                    unassignedGates.Remove(suitableGate);
                                    successfullyAssigned += 1;
                                }

                                // Assign normal flights (which are fine with anything)
                                while (unassignedFlights.Count > 0)
                                {
                                    Flight flight = unassignedFlights.Dequeue();
                                    BoardingGate? suitableGate = unassignedGates.FirstOrDefault();

                                    if (suitableGate == null) throw new Exception("No available gates for normal flights.");
                                    else
                                    {
                                        suitableGate.Flight = flight;
                                        unassignedGates.Remove(suitableGate);
                                        successfullyAssigned += 1;
                                    }
                                }
                            } catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            } finally
                            {
                                Console.WriteLine($"{successfullyAssigned} flights successfully assigned to gates.");
                                double percentage = (successfullyAssigned / (double)successfullyAssigned + alredyAssigned) * 100;
                                Console.WriteLine($"{(successfullyAssigned == 0 ? "0" : percentage):F2}% out of total assigned flights processed");
                            }
                            break;

                        case 9:

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
                terminal5.Airlines.Add(airlineArray[1], airlineObject);
            }
            Console.WriteLine($"{terminal5.Airlines.Count} Airlines Loaded!");

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

                terminal5.BoardingGates.Add(boardingGateArray[0], boardingGateObject);
            }
            Console.WriteLine($"{terminal5.BoardingGates.Count} Boarding Gates Loaded!");

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
                terminal5.Flights.Add(flightArray[0], flightObject);
            }

            // Load flights into respective airlines
            foreach (Flight flight in terminal5.Flights.Values)
            {
                Airline airline = terminal5.Airlines.FirstOrDefault(x => x.Key == flight.FlightNumber.Substring(0, 2)).Value;
                airline.Flights.Add(flight.FlightNumber, flight);
            }
            Console.WriteLine($"{terminal5.Flights.Count} Flights Loaded!");
        }
    }
}
