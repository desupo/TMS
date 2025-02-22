using Microsoft.Extensions.Logging;

namespace TMS.App.Domain;

public class Trip
{
    public string EquipmentId { get; set; }
    public string OriginCity { get; set; }
    public string DestinationCity { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public TimeSpan Duration { get; set; }
    public bool HasIssue { get; set; }
    public bool Completed { get; set; }
    public List<Event> Events { get; set; }
}
public class Event
{
    public int Id { get; set; }
    public string EquipmentId { get; set; }
    public string Code { get; set; }
    public DateTimeOffset EventDate { get; set; }
    public int CityId { get; set; }
    public string City { get; set; }
}
public class RailcarTripModel
{
    public long TripId { get; set; }
    public string EquipmentId { get; set; }
    public string OriginCity { get; set; }
    public string DestinationCity { get; set; }
    public DateTime StartDateUtc { get; set; }
    public DateTime EndDateUtc { get; set; }
    public double TotalHours { get; set; }
    public List<RailcarEventModel> Events { get; set; } = new();
}

public class RailcarEventModel
{
    public string EventCode { get; set; }
    public DateTime EventTimeUtc { get; set; }
    public string City { get; set; }
}
