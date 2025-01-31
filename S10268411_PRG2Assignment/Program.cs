//==========================================================
// Student Number   : S10268411
// Student Name	: Tan Chin Ray
// Partner Name	: -
//==========================================================


using System.Transactions;
using System.Reflection;

namespace S10268411_PRG2Assignment
{
    internal class Program
    {
        private static List<Terminal> terminalList = new List<Terminal>();
        private static Terminal selectedTerminal;

        // Terminal selection
        static void Main(string[] args)
        {
            InitialiseValues();
            bool mainMenu = true;
            while (mainMenu)
            {
                Console.WriteLine();
                Console.WriteLine("=============================================");
                Console.WriteLine("Welcome to Changi Airport");
                Console.WriteLine("=============================================");
                for (int i = 1; i < terminalList.Count + 1; i++)
                {
                    Console.WriteLine($"{i}. {terminalList[i - 1].TerminalName}");
                }
                Console.WriteLine("0. Exit");
                Console.WriteLine();
                
                Console.Write("Please select a terminal: ");
                if (Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    // Terminal option
                    if (choice > 0 && choice <= terminalList.Count)
                    {
                        selectedTerminal = terminalList[choice - 1];
                        Entry();
                    }
                    else if (choice == 0) // Exit option
                    {
                        Console.WriteLine("Goodbye!");
                        mainMenu = false;
                        break;
                    } else // Invalid option
                    {
                        Console.WriteLine("Invalid option.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }

            
        }

        // Entry point containing main code for functionality
        private static void Entry()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=============================================");
                Console.WriteLine($"Welcome to Changi Airport {selectedTerminal}");
                Console.WriteLine("=============================================");
                Console.WriteLine("1. List all Flights");
                Console.WriteLine("2. List Boarding Gates");
                Console.WriteLine("3. Assign a Boarding Gate to a Flight [UNAVAILABLE - SOLO]");
                Console.WriteLine("4. Create Flight");
                Console.WriteLine("5. Display Airline Flights");
                Console.WriteLine("6. Modify flight details [UNAVAILABLE - SOLO]");
                Console.WriteLine("7. Display Flight Schedule [UNAVAILABLE - SOLO]");
                Console.WriteLine("8. Process all unassigned flights to boarding gates");
                //Console.WriteLine("9. Display total fee per airline for the day");
                Console.WriteLine("0. Return to main menu");
                Console.WriteLine();
                Console.WriteLine("Please select an option: ");
                if (Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        // Basic feature 3: List all flights
                        case 1:
                            Console.WriteLine("=============================================");
                            Console.WriteLine($"List of Flights for Changi Airport {selectedTerminal}");
                            Console.WriteLine("=============================================");
                            Console.WriteLine($"{"Flight Number",-18}{"Airline Name",-22}{"Origin",-22}{"Destination",-21}{"Expected Departure/Arrival Time"}");
                            foreach (Flight flight in selectedTerminal.Flights.Values)
                            {
                                //                   <--Flight Number-->      <-------------------------Airline Name---------------------------->      <---Origin-->      <---Destination-->      <-Expected Departure/Arrival Time->
                                Console.WriteLine($"{flight.FlightNumber,-18}{selectedTerminal.Airlines[flight.FlightNumber.Substring(0, 2)].Name,-22}{flight.Origin,-22}{flight.Destination,-21}{flight.ExpectedTime.ToString(@"g")}");
                            }
                            break;

                        // Basic feature 4: List all boarding gates
                        case 2:
                            Console.WriteLine("=============================================");
                            Console.WriteLine($"List of Boarding Gates for Changi Airport {selectedTerminal}");
                            Console.WriteLine("=============================================");
                            Console.WriteLine($"{"Gate Name",-14}{"DDJB",-10}{"CFFT",-10}{"LWTT",-10}{"Assigned Flight"}");
                            foreach (BoardingGate boardingGate in selectedTerminal.BoardingGates.Values)
                            {
                                //                   <-----Gate Name----->      <---------DDJB---------->      <----------CFFT--------->      <----------LWTT--------->      <--------Assigned Flight Number-------->
                                Console.WriteLine($"{boardingGate.GateName,-14}{boardingGate.SupportsDDJB,-10}{boardingGate.SupportsCFFT,-10}{boardingGate.SupportsLWTT,-10}{boardingGate.Flight?.FlightNumber ?? "-"}");
                            }
                            break;

                        // Basic feature 5: Assign a boarding gate to flight (NOT IMPLEMENTED - SOLO)
                        case 3:
                            Console.WriteLine("Not implemented - solo project");
                            break;

                        // Basic feature 6: Create flight
                        case 4:
                            bool loop = true;
                            while (loop == true)
                            {
                                try
                                {
                                    Console.Write("Enter Flight Number: ");
                                    string flightNumber = Console.ReadLine();

                                    // Check if flight number is null or empty
                                    if (string.IsNullOrEmpty(flightNumber))
                                    {
                                        throw new ArgumentException("Invalid flight number - This field cannot be left blank.");
                                    }

                                    // Check if specified airline code exists
                                    if (selectedTerminal.Airlines.ContainsKey(flightNumber.Substring(0, 2)) == false)
                                    {
                                        throw new ArgumentException("Invalid flight number - Invalid airline code.");
                                    }

                                    // Checks if there is a space between airline code and number (e.g. SQ 112)
                                    //                                                                    ^
                                    if (flightNumber[2] != ' ')
                                    {
                                        throw new ArgumentException("Invalid flight number - You must leave a space in between the airline code and the number.");
                                    }

                                    // Checks if the rest of the flight number consists only of numbers
                                    if (!Int32.TryParse(flightNumber.Split(' ')[1], out int _flightNumber))
                                    {
                                        throw new ArgumentException("Invalid flight number - the second part of the flight number must consist of digits only.");
                                    }

                                    Console.Write("Enter Origin: ");
                                    string origin = Console.ReadLine();
                                    
                                    // Check if flight origin is null or empty
                                    if (string.IsNullOrEmpty(origin))
                                    {
                                        throw new ArgumentException("Invalid flight origin - This field cannot be left blank.");
                                    }

                                    Console.Write("Enter Destination: ");
                                    string destination = Console.ReadLine();
                                    
                                    // Check if flight destination is null or empty
                                    if (string.IsNullOrEmpty(destination))
                                    {
                                        throw new ArgumentException("Invalid flight destination - This field cannot be left blank.");
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

                                        selectedTerminal.Flights.Add(flightNumber, flightObject);
                                        Airline targetAirline = selectedTerminal.Airlines.Values.FirstOrDefault(x => x.Code.ToUpper().Contains(flightNumber.Substring(0, 2)));
                                        if (!targetAirline.AddFlight(flightObject)) throw new ArgumentException("This exception should never be thrown, but there was an error adding the flight to their airline.");

                                        File.AppendAllText("flights.csv", string.Join(",", flightNumber, origin, destination, expectedTime.ToString("t"), requestCode));
                                        Console.WriteLine($"Flight {flightNumber} has been added!");
                                        Console.WriteLine();
                                        
                                        Console.Write("Would you like to add another flight [y/N]: ");
                                        string response = Console.ReadLine();
                                        if (response.ToLower() != "y")
                                        {
                                            loop = false;
                                            if (response.ToLower() != "n") Console.WriteLine("I'm taking that as a no.");
                                        }
                                    }
                                    else
                                    {
                                        throw new ArgumentException("Invalid date or time.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                            break;

                        // Basic feature 7: Display full flight details from airline
                        case 5:
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("=============================================");
                                    Console.WriteLine($"List of Airlines for Changi Airport {selectedTerminal}");
                                    Console.WriteLine("=============================================");
                                    Console.WriteLine($"{"Airline Code",-17}{"Airline Name"}");
                                    foreach (KeyValuePair<string, Airline> keyValuePair in selectedTerminal.Airlines)
                                    {
                                        Console.WriteLine($"{keyValuePair.Key,-17}{keyValuePair.Value.Name}");
                                    }
                                    
                                    Console.WriteLine("Enter Airline Code: ");
                                    string airlineCode = Console.ReadLine();

                                    // Validates airline code: Set variable to airline object if valid
                                    Airline airline = selectedTerminal.Airlines.Values.FirstOrDefault(x => x.Code.ToUpper() == airlineCode);
                                    if (airline == null) throw new ArgumentException("Invalid airline code.");

                                    Console.WriteLine($"List of Flights for {airline.Name}");
                                    Console.WriteLine($"{"Flight Number",-18}{"Airline Name",-22}{"Origin",-22}{"Destination",-21}{"Boarding Gate",-21}{"Special Request Code",-25}{"Expected Departure/Arrival Time"}");

                                    foreach (Flight flight in airline.Flights.Values)
                                    {
                                        string boardingGate = selectedTerminal.BoardingGates.Values.FirstOrDefault(x => x.Flight == flight)?.GateName ?? "-";
                                        
                                        string specialRequestCode;
                                        if (flight is LWTTFlight) specialRequestCode = "LWTT";
                                        else if (flight is CFFTFlight) specialRequestCode = "CFFT";
                                        else if (flight is DDJBFlight) specialRequestCode = "DDJB";
                                        else specialRequestCode = "-";

                                        Console.WriteLine($"{flight.FlightNumber,-18}{airline.Name,-22}{flight.Origin,-22}{flight.Destination,-21}{boardingGate,-21}{specialRequestCode,-25}{flight.ExpectedTime.ToString(@"G")}");
                                    }

                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                            break;

                        // Basic feature 8: Modify flight details (NOT IMPLEMENTED - SOLO)
                        case 6:
                            Console.WriteLine("Not implemented - solo project");
                            break;

                        // Basic feature 9: Display scheduled flights in chronological order (NOT IMPLEMENTED - SOLO)
                        case 7:
                            Console.WriteLine("Not implemented - solo project");
                            break;

                        // Advanced feature (a): Process all unassigned flights to boarding gates in bulk
                        case 8:
                            try
                            {
                                int successfullyAssigned = 0;
                                int alreadyAssigned = selectedTerminal.BoardingGates.Values.Where(x => x.Flight != null).Count();
                                int totalFlights = 0;
                                try
                                {
                                    // Populate list of unassigned gates and flights
                                    List<BoardingGate> unassignedGates = selectedTerminal.BoardingGates.Values.ToList().Where(x => x.Flight == null).ToList();
                                    Queue<Flight> unassignedFlights = new Queue<Flight>();

                                    // Check which flights are unassigned
                                    foreach (Flight flight in selectedTerminal.Flights.Values)
                                    {
                                        if (selectedTerminal.BoardingGates.Values.FirstOrDefault(x => x.Flight == flight) == null)
                                        {
                                            unassignedFlights.Enqueue(flight);
                                        }
                                    }

                                    totalFlights = unassignedFlights.Count;

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
                                            if (suitableGate == null)
                                            {
                                                Console.WriteLine("Error: No available CFFT capable gates for flight.");
                                                continue;
                                            }
                                        }
                                        else if (flight is DDJBFlight)
                                        {
                                            suitableGate = unassignedGates.FirstOrDefault(x => x.SupportsDDJB == true);

                                            if (suitableGate == null)
                                            {
                                                Console.WriteLine("Error: No available DDJB capable gates for flight.");
                                                continue;
                                            }
                                        }
                                        else if (flight is LWTTFlight)
                                        {
                                            suitableGate = unassignedGates.FirstOrDefault(x => x.SupportsLWTT == true);

                                            if(suitableGate == null)
                                            {
                                                Console.WriteLine("Error: No available LWTT capable gates for flight.");
                                                continue;
                                            }
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

                                        if (suitableGate == null) throw new Exception("No available gates for normal flights."); // Can throw exception here since all the other flights will be normal flights, so we know it will fail regardless
                                        else
                                        {
                                            suitableGate.Flight = flight;
                                            unassignedGates.Remove(suitableGate);
                                            successfullyAssigned += 1;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                                finally
                                {
                                    Console.WriteLine($"{successfullyAssigned}/{totalFlights} flights successfully assigned to gates.");
                                    double percentage = (successfullyAssigned / (double)successfullyAssigned + alreadyAssigned) * 100;
                                    Console.WriteLine($"{(successfullyAssigned == 0 ? "0" : percentage):F2}% out of total assigned flights processed");
                                }
                            } catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        // Advanced feature (b) (NOT IMPLEMENTED - SOLO)
                        case 9:
                            /*try
                            {
                                foreach (Flight flight in terminal5.Flights.Values)
                                {
                                    if (terminal5.BoardingGates.Values.FirstOrDefault(x => x.Flight == flight) == null)
                                    {
                                        throw new InvalidOperationException("Not all flights have been assigned to gates. Please assign all unassigned flights and try again.")
                                    }
                                }

                                foreach (Airline airline in terminal5.Airlines.Values)
                                {
                                    double totalFees = 0;
                                    airline.Flights.Values.ToList().ForEach(x => totalFees += x.CalculateFees());
                                }
                            } catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }*/

                            Console.WriteLine("Not implemented - solo project");
                            break;

                        case 0:
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
            // BONUS: Multi-terminal support by reading the csv files in all folders that contain "terminal"
            foreach (string folder in Directory.GetDirectories(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                if (folder.ToLower().Contains("terminal")) // Any folder that contains "terminal" is considered, opening options for special terminal names
                {
                    string shortFolder = folder.Split(@"\").Last(); // Convert absolute -> relative
                    
                    // Skip terminal folders if they don't contain one or more of the required csv files
                    if (!File.Exists(Path.Combine(folder, "airlines.csv")) || !File.Exists(Path.Combine(folder, "boardinggates.csv")) || !File.Exists(Path.Combine(folder, "flights.csv")))
                    {
                        Console.WriteLine($@"Terminal folder ""{shortFolder}"" does not contain one or more of the following files: airlines.csv, boardinggates.csv, flights.csv. Skipping this one.");
                        continue;
                    } else
                    {
                        Console.WriteLine($@"Processing terminal ""{shortFolder}""...");

                        Terminal terminal = new Terminal(shortFolder);

                        // Load airlines
                        Console.WriteLine("Loading Airlines...");
                        List<string> airlinesFile = File.ReadAllLines(Path.Combine(folder, "airlines.csv")).ToList();
                        airlinesFile.RemoveAt(0); // Remove the headers of the csv file
                        foreach (string airline in airlinesFile)
                        {
                            string[] airlineArray = airline.Split(',');
                            Airline airlineObject = new Airline(airlineArray[0], airlineArray[1]);
                            terminal.AddAirline(airlineObject);
                        }
                        Console.WriteLine($"{terminal.Airlines.Count} Airlines Loaded!");

                        // Load boarding gates
                        Console.WriteLine("Loading Boarding Gates...");
                        List<string> boardingGatesFile = File.ReadAllLines(Path.Combine(folder, "boardinggates.csv")).ToList();
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

                            terminal.AddBoardingGate(boardingGateObject);
                        }
                        Console.WriteLine($"{terminal.BoardingGates.Count} Boarding Gates Loaded!");

                        // Load flights
                        Console.WriteLine("Loading Flights...");
                        List<string> flightsFile = File.ReadAllLines(Path.Combine(folder, "flights.csv")).ToList();
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
                            terminal.Flights.Add(flightArray[0], flightObject);
                        }

                        // Load flights into respective airlines
                        foreach (Flight flight in terminal.Flights.Values)
                        {
                            Airline airline = terminal.Airlines.FirstOrDefault(x => x.Key == flight.FlightNumber.Substring(0, 2)).Value;
                            airline.AddFlight(flight);
                        }
                        Console.WriteLine($"{terminal.Flights.Count} Flights Loaded!");
                        terminalList.Add(terminal);
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
