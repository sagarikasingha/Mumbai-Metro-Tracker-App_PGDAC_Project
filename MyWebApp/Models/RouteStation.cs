namespace MyWebApp.Models;


public class RouteStation
{
    public uint Id { get; set; }
    public uint StationIdFrom { get; set; }
    public uint StationIdTo { get; set; }
    public int Stop { get; set; }
    public TimeSpan  Time { get; set; }
    public float Fare { get; set; }
    public int InterchangeStops { get; set; }
}
