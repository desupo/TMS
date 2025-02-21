using TMS.domain.Contracts;

namespace TMS.domain.Entities;

public class Event : BaseEntity
{
    public string EquipmentId { get; set; }
    public string Code { get; set; } // W or Z
    public DateTimeOffset EventDate { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; }
}
