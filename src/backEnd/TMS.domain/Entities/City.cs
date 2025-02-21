using TMS.domain.Contracts;

namespace TMS.domain.Entities;

public class City : BaseEntity
{
    public City()
    {
        Start_Cities = new HashSet<Trip>();
        Destination_Cities = new HashSet<Trip>();
    }
    public string Name { get; set; }
    public string TimeZone { get; set; }

    public virtual ICollection<Trip> Start_Cities { get; set; }
    public virtual ICollection<Trip> Destination_Cities { get; set; }
}
