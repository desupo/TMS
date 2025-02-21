namespace TMS.application.DTOs;

public class EventDto
{
    public string Equipment { get; set; }
    public string EventCode { get; set; }
    /// <summary>
    /// DateTime without the offset. we wil get the offset from the city time zone information. 
    /// </summary>
    public DateTime EventTime { get; set; }
    public int CityId { get; set; }
}
