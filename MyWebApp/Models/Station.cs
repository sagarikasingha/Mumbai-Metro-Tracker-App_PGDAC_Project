namespace MyWebApp.Models;


public class Station
{   
    
    public uint StationId { get; set; }
    public string StationName { get; set; }
    public uint RouteId { get; set; }
    public uint? PreviousStationId { get; set; }
    public uint? NextStationId { get; set; }
}

