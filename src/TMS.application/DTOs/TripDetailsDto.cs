using TMS.domain.Entities;

namespace TMS.application.DTOs;

public class TripDetailsDto
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