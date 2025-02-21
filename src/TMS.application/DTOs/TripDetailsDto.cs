using TMS.domain.Entities;

namespace TMS.application.DTOs;

public class TripDetailsDto
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