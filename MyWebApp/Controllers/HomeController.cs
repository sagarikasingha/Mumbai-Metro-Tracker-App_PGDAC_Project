using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.Cms;

public class HomeController : Controller
{
    public IActionResult AdminLogin()
    {
        return View(new Admin());
    }

    [HttpPost]
    public IActionResult AdminLogin([FromServices] SiteDbContext site, Admin input)
    {
        if (ModelState.IsValid)
        {
            var admin = site.admins.Find(input.Username);
            if (admin != null && admin.Password == input.Password)
            {
                var identity = new ClaimsIdentity("Admin");
                identity.AddClaim(new Claim(ClaimTypes.Name, input.Username.ToString()));
                HttpContext.SignInAsync(new ClaimsPrincipal(identity));

                return Redirect("/adminDashboard.html");

            }

            ModelState.AddModelError("Login", "Invalid USERNAME or PASSWORD");

        }
        return RedirectToAction("AdminLogin");
    }

    public IActionResult UserLogin()
    {
        return View(new User());
    }

    [HttpPost]
    public IActionResult UserLogin([FromServices] SiteDbContext site, User Input)
    {
        if (ModelState.IsValid)
        {
            var customer = site.users.Find(Input.Username);
            if (customer?.Password == Input.Password)
            {
                var identity = new ClaimsIdentity("User");
                identity.AddClaim(new Claim(ClaimTypes.Name, Input.Username.ToString()));
                HttpContext.SignInAsync(new ClaimsPrincipal(identity));
                return Redirect("/userInterface.html");
            }

            ModelState.AddModelError("Login", "Invalid Admin ID or Password");

        }

        return RedirectToAction("index");

    }

    public IActionResult UserRegistration()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UserRegistration([FromServices] SiteDbContext site, User user)
    {

        if (ModelState.IsValid)
        {

            var existingAdmin = site.users.FirstOrDefault(x => x.Username == user.Username);
            if (existingAdmin == null)
            {
                site.users.Add(user);
                site.SaveChanges();
                return RedirectToAction("AdminLogin");
            }
            else
            {
                ModelState.AddModelError("Id", "ID is already registered. Please enter a different ID.");
            }
        }

        return View();
    }

    // public IActionResult ViewStaion()
    // {
    //     return View();
    // }

    [HttpGet]
    public IActionResult ViewStation()
    {
        using (var site = new SiteDbContext())
        {
            var selection = from station in site.stations
                            select new Station
                            {
                                StationId = station.StationId,
                                StationName = station.StationName,
                                RouteId = station.RouteId,
                                PreviousStationId = station.PreviousStationId,
                                NextStationId = station.NextStationId
                            };

            return View(selection.ToList());
        }
    }


    public IActionResult AddStation()
    {
        return View();
    }

    [HttpPost]

    public IActionResult AddStation([FromServices] SiteDbContext site, Station input)
    {
        if (ModelState.IsValid)
        {
            var existingStation = site.stations.Find(input.StationId);
            if (existingStation == null)
            {
                site.stations.Add(input);
                site.SaveChanges();
                return RedirectToAction("ViewStations");
            }
            else
            {
                ModelState.AddModelError("", "Station ID already exists.");
            }
        }
        return View(input);
    }

    public IActionResult DisplayAllLineStations([FromServices] SiteDbContext site)
    {
        var viewModel = new LineStationsViewModel
        {
            YellowLineStations = site.stations
                                    .Where(s => s.RouteId == 1)
                                    .Select(s => s.StationName)
                                    .ToList(),
            RedLineStations = site.stations
                                    .Where(s => s.RouteId == 2)
                                    .Select(s => s.StationName)
                                    .ToList(),
            BlueLineStations = site.stations
                                    .Where(s => s.RouteId == 3)
                                    .Select(s => s.StationName)
                                    .ToList()
        };

        return View(viewModel);
    }



    private readonly SiteDbContext _context;

    public HomeController()
    {
        _context = new SiteDbContext();
    }

    // public IActionResult JourneyPlanner()
    // {
    //     // Fetch stations from the database
    //     List<Station> stations = _context.stations.ToList();

    //     // Create SelectLists for both source and destination stations
    //     ViewBag.sourceStationId = new SelectList(stations, "StationId", "StationName");
    //     ViewBag.destinationStationId = new SelectList(stations, "StationId", "StationName");

    //     // Return the view
    //     return View("JourneyPlanner");
    // }

    // [HttpPost]
    // public IActionResult CalculateShortestRoute(int sourceStationId, int destinationStationId)
    // {
    //     var shortestRoute = CalculateShortestRouteAdvance(sourceStationId, destinationStationId);
    //     return View("CalculateShortestRoute", shortestRoute);
    // }

    // private List<Station> CalculateShortestRouteAdvance(int sourceStationId, int destinationStationId)
    // {
    //     // Implement Dijkstra's algorithm to find the shortest path between source and destination stations
    //     // Initialize data structures and fetch station data from the database
    //     // Implement Dijkstra's algorithm to find the shortest path between source and destination stations
    //     // Initialize data structures and fetch station data from the database
    //     Dictionary<int, int> distances = new Dictionary<int, int>(); // StationId -> Distance
    //     Dictionary<int, int?> previousStations = new Dictionary<int, int?>(); // StationId -> PreviousStationId
    //     HashSet<int> visited = new HashSet<int>(); // Visited stations

    //     // Fetch station data from the database
    //     Dictionary<int, List<int>> adjacentStations = GetAdjacentStationsFromDatabase();

    //     // Initialize distances and previous stations
    //     foreach (var stationId in adjacentStations.Keys)
    //     {
    //         distances[stationId] = int.MaxValue;
    //         previousStations[stationId] = null;
    //     }
    //     distances[sourceStationId] = 0;

    //     // Dijkstra's algorithm
    //     while (true)
    //     {
    //         // Find the nearest unvisited station
    //         int nearestStation = -1;
    //         foreach (var entry in distances)
    //         {
    //             if (!visited.Contains(entry.Key) && (nearestStation == -1 || entry.Value < distances[nearestStation]))
    //             {
    //                 nearestStation = entry.Key;
    //             }
    //         }

    //         if (nearestStation == -1)
    //         {
    //             // No reachable stations left
    //             break;
    //         }

    //         if (nearestStation == destinationStationId)
    //         {
    //             // Shortest path found, backtrack to construct the path
    //             List<Station> shortestPath = new List<Station>();
    //             int currentStationId = destinationStationId;
    //             while (currentStationId != sourceStationId)
    //             {
    //                 shortestPath.Add(GetStationById(currentStationId));
    //                 currentStationId = previousStations[currentStationId].Value;
    //             }
    //             shortestPath.Add(GetStationById(sourceStationId));
    //             shortestPath.Reverse(); // Reverse the list to get the correct order
    //             return shortestPath;
    //         }

    //         // Update distances to adjacent stations
    //         foreach (var adjacentStation in adjacentStations[nearestStation])
    //         {
    //             int newDistance = distances[nearestStation] + (int)GetDistance(nearestStation, adjacentStation); // Use actual distance
    //             if (newDistance < distances[adjacentStation])
    //             {
    //                 distances[adjacentStation] = newDistance;
    //                 previousStations[adjacentStation] = nearestStation;
    //             }
    //         }


    //         visited.Add(nearestStation);

    //     }

    //     // If destination is unreachable
    //     return null;
    // }

    // // Fetch adjacent stations data from the database
    // private Dictionary<int, List<int>> GetAdjacentStationsFromDatabase()
    // {
    //     Dictionary<int, List<int>> adjacentStations = new Dictionary<int, List<int>>();

    //     // Retrieve data from the route_stations table
    //     var routeStations = _context.routeStations.ToList();

    //     // Populate the adjacentStations dictionary
    //     foreach (var routeStation in routeStations)

    //     {

    //         // int stationIdFrom = checked((int)routeStation.StationIdFrom);  --- downcasting uint to int 
    //         // int stationIdTo = checked((int)routeStation.StationIdTo);

    //         if (!adjacentStations.ContainsKey((int)routeStation.StationIdFrom))
    //         {
    //             adjacentStations[(int)routeStation.StationIdFrom] = new List<int>();
    //         }
    //         adjacentStations[(int)routeStation.StationIdFrom].Add((int)routeStation.StationIdTo);
    //     }

    //     return adjacentStations;
    // }


    // private double GetDistance(int sourceStationId, int destinationStationId)
    // {
    //     // Query the route_stations table to fetch the time
    //     var routeStation = _context.routeStations.FirstOrDefault(rs => rs.StationIdFrom == sourceStationId && rs.StationIdTo == destinationStationId);

    //     // Check if the routeStation is found
    //     if (routeStation != null)
    //     {
    //         // Return the time (in minutes) retrieved from the database
    //         return routeStation.Time.TotalMinutes;
    //     }
    //     else
    //     {
    //         // If the connection between stations is not found, return a default or large value
    //         return double.MaxValue; // Or any other appropriate value to represent unreachable stations
    //     }
    // }



    // private Station GetStationById(int stationId)
    // {
    //     // Query the stations table to fetch the station information by ID
    //     var station = _context.stations.FirstOrDefault(s => s.StationId == stationId);

    //     // Return the fetched station information
    //     return station;
    // }


public IActionResult JourneyPlanner(int sourceStationId, int destinationStationId)
{
    // Fetch stations from the database
    List<Station> stations = _context.stations.ToList();

    // Create SelectLists for both source and destination stations
    ViewBag.sourceStationId = new SelectList(stations, "StationId", "StationName", sourceStationId);
    ViewBag.destinationStationId = new SelectList(stations, "StationId", "StationName", destinationStationId);


    //fare
    float fare = CalculateFareForMetro(sourceStationId, destinationStationId);
    ViewData["Fare"] = fare;

    //time
    TimeSpan time = CalculateTimeForMetro(sourceStationId, destinationStationId);
    ViewData["Time"] = time;

    //stop
    int stop = CalculateStopForMetro(sourceStationId, destinationStationId);
    ViewData["Stop"] = stop;

    //interchangestop
    int interchangestop = CalculateInterchangeStopsForMetro(sourceStationId, destinationStationId);
    ViewData["InterchangeStops"] = interchangestop;

    // Retrieve all stations between the selected source and destination
    List<Station> allStations = CalculateAllStationsBetweenSourceAndDestination(sourceStationId, destinationStationId);

    // Pass the list of stations to the view
    ViewBag.allStations = allStations;

    // Return the view
    return View("JourneyPlanner" , allStations );
}

    private TimeSpan CalculateTimeForMetro(int sourceStationId, int destinationStationId)
    {
        
        TimeSpan time = new TimeSpan(0, 0, 0);

    // Retrieve the fare table from your database
    var timeTable = _context.routeStations.ToList();

    // Find the fare entry corresponding to the source and destination stations
    var timeEntry = timeTable.FirstOrDefault(f => f.StationIdFrom == sourceStationId && f.StationIdTo == destinationStationId);

    // If a matching fare entry is found, set the fare value
    if (timeEntry != null)
    {
        time = (TimeSpan)timeEntry.Time;
    }
    else
    {
        
        time=TimeSpan.Zero;// Set a default value or return an error code
    }

    return time;
    }
private int CalculateFareForMetro(int sourceStationId, int destinationStationId)
    {
        int fare = 0;

    // Retrieve the fare table from your database
    var fareTable = _context.routeStations.ToList();

    // Find the fare entry corresponding to the source and destination stations
    var fareEntry = fareTable.FirstOrDefault(f => f.StationIdFrom == sourceStationId && f.StationIdTo == destinationStationId);

    // If a matching fare entry is found, set the fare value
    if (fareEntry != null)
    {
        fare = (int)fareEntry.Fare;
    }
    else
    {
        
        fare = 0; // Set a default value or return an error code
    }

    return fare;
    }

private int CalculateStopForMetro(int sourceStationId, int destinationStationId)
    {
        int stop = 0;

    // Retrieve the fare table from your database
    var stopTable = _context.routeStations.ToList();

    // Find the fare entry corresponding to the source and destination stations
    var stopEntry = stopTable.FirstOrDefault(f => f.StationIdFrom == sourceStationId && f.StationIdTo == destinationStationId);

    // If a matching fare entry is found, set the fare value
    if (stopEntry != null)
    {
        stop = (int)stopEntry.Stop;
    }
    else
    {
        
        stop = 0; // Set a default value or return an error code
    }

    return stop;
    }

private int CalculateInterchangeStopsForMetro(int sourceStationId, int destinationStationId)
    {
        int interchangeStops = 0;

    // Retrieve the fare table from your database
    var interchangeStopsTable = _context.routeStations.ToList();

    // Find the fare entry corresponding to the source and destination stations
    var interchangeStopsEntry = interchangeStopsTable.FirstOrDefault(f => f.StationIdFrom == sourceStationId && f.StationIdTo == destinationStationId);

    // If a matching fare entry is found, set the fare value
    if (interchangeStopsEntry != null)
    {
        interchangeStops = (int)interchangeStopsEntry.InterchangeStops;
            }
    else
    {
        
        interchangeStops = 0; // Set a default value or return an error code
    }

    return interchangeStops;
    }


    
    [HttpPost]
    public IActionResult MetroBetweenStations(int sourceStationId, int destinationStationId)
    {   
        float fare = CalculateFareForMetro(sourceStationId, destinationStationId);
        ViewData["Fare"] = fare;

        var shortestRoute = CalculateAllStationsBetweenSourceAndDestination(sourceStationId, destinationStationId);
        return View("MetroBetweenStations", shortestRoute);
    }

private List<Station> CalculateAllStationsBetweenSourceAndDestination(int sourceStationId, int destinationStationId)
{
    List<Station> allStations = new List<Station>();
    HashSet<int> visited = new HashSet<int>();

    Station sourceStation = _context.stations.FirstOrDefault(s => s.StationId == sourceStationId);
    Station destinationStation = _context.stations.FirstOrDefault(s => s.StationId == destinationStationId);

    if (sourceStation != null && destinationStation != null)
    {
        Queue<Station> queue = new Queue<Station>();
        queue.Enqueue(sourceStation);
        visited.Add(sourceStationId);

        while (queue.Count > 0)
        {
            Station currentStation = queue.Dequeue();
            allStations.Add(currentStation);

            if (currentStation.StationId == destinationStationId)
            {
                // Destination reached, include it and stop traversal
                allStations.Add(destinationStation);
                break;
            }

            // Traverse to adjacent stations
            List<Station> adjacentStations = GetAdjacentStations(currentStation);
            foreach (Station adjacentStation in adjacentStations)
            {
                // Check if the adjacent station is between the source and destination
                if (!visited.Contains((int)adjacentStation.StationId))
                {
                    if (IsStationBetween((int)adjacentStation.StationId, sourceStationId, destinationStationId))
                    {
                        queue.Enqueue(adjacentStation);
                        visited.Add((int)adjacentStation.StationId);
                    }
                }
            }
        }

        if (!allStations.Contains(destinationStation))
        {
            allStations.Add(destinationStation);
        }
        
    }

    return allStations;
}




    private bool IsStationBetween(int stationId, int sourceStationId, int destinationStationId)
    {
        // Implement logic to check if the station is between the source and destination
        // You can use timestamps, order information, or any other criteria specific to your data model

        // Assuming stations are ordered based on their IDs
        return stationId > sourceStationId && stationId < destinationStationId;
    }

    private List<Station> GetAdjacentStations(Station station)
    {
        List<Station> adjacentStations = new List<Station>();

        // Assuming a many-to-many relationship between stations in the database
        // Replace "routeStations" with the actual property name representing the relationship in your database model
        var adjacentStationIds = _context.routeStations
            .Where(rs => rs.StationIdFrom == station.StationId)
            .Select(rs => rs.StationIdTo)
            .ToList();

        foreach (int adjacentStationId in adjacentStationIds)
        {
            Station adjacentStation = _context.stations.FirstOrDefault(s => s.StationId == adjacentStationId);
            if (adjacentStation != null)
            {
                adjacentStations.Add(adjacentStation);
            }
        }

        return adjacentStations;
    }



    public IActionResult TrainSchedule()
    {
        var timeTable = _context.timeTables.ToList();
        var stations = _context.stations.ToList();
        ViewData["Stations"] = stations;

        return View(timeTable);
    }

    public IActionResult ViewFeedback()
    {
        List<Feedback> feedback = _context.feedbacks.ToList();
        return View(feedback);

    }

    public IActionResult FeedbackForm()
    {

        return View();
    }

    [HttpPost]
    public IActionResult FeedbackForm(Feedback feedback)
    {
        if (ModelState.IsValid)
        {
            _context.feedbacks.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("ThankYOu");

        }
        else
        {
            ModelState.AddModelError("Some Error Occured", "Please Try Again");
            return View("FeedbackForm", feedback);
        }

    }
    public IActionResult ThankYOu()
    {

        return View();
    }
}








