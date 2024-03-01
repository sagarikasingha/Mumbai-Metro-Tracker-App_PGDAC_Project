namespace MyWebApp.Models;


public class TimeTable
{
    public uint Id { get; set; }
    public uint StationIdFrom { get; set; }
    public uint StationIdTo { get; set; }
    public TimeSpan FirstTrain { get; set; }
    public TimeSpan LastTrain { get; set; }
}
