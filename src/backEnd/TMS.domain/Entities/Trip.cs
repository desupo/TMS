using System.ComponentModel.DataAnnotations.Schema;
using TMS.domain.Contracts;

namespace TMS.domain.Entities;

public class Trip : BaseEntity<long>
{
    public string EquipmentId { get; set; }
    public int Origin_CityId { get; set; }
    public int Destination_CityId { get; set; }
    public DateTimeOffset Start_Date { get; set; }
    public DateTimeOffset End_Date { get; set; }
    //Computed field. ReadOnly
    public TimeSpan Duration { get; private set; }
    /// <summary>
    /// This will be true if the trip has any missing code or misplaced code
    /// </summary>
    public bool HasIssue { get; set; }
    /// <summary>
    /// Most time this will be true, unless the latest code is not Z
    /// </summary>
    public bool Completed { get; set; }


    public virtual City Origin_City { get; set; }

    public virtual City Destination_City { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
}
