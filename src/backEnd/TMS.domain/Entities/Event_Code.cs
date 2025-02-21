using TMS.domain.Contracts;

namespace TMS.domain.Entities;

public class Event_Code : BaseEntity
{
    //[MaxLength(1)]
    //[Required(AllowEmptyStrings = false)]
    //[RegularExpression(@"^[A-Za-z]$", ErrorMessage = "Invalid entry, please enter a single alphabet character (A-Z)")]
    public string Code { get; set; } = string.Empty;
    //[MaxLength(35)]
    public string Name { get; set; } = string.Empty;
    //[MaxLength(450)]
    public string Description { get; set; } = string.Empty;
}
