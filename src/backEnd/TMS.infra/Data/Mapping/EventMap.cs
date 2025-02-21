using CsvHelper.Configuration;
using TMS.domain.Entities;

namespace TMS.infra.Data.Mapping;

public sealed class EventMap : ClassMap<Event>
{
    public EventMap()
    {
        Map(m => m.EquipmentId).Name("Equipment Id"); // Map "Equipment Id" to EquipmentId
        Map(m => m.Code).Name("Event Code"); // Map "Code" to Code
        Map(m => m.EventDate).Name("Event Time").TypeConverter<DateTimeOffsetConverter>(); // Map "Event Date" to EventDate
        Map(m => m.CityId).Name("City Id"); // Map "City Id" to CityId
    }
}
