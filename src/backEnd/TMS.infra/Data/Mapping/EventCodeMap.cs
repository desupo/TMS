using CsvHelper.Configuration;
using TMS.domain.Entities;

namespace TMS.infra.Data.Mapping;
public class EventCodeMap : ClassMap<Event_Code>
{
    public EventCodeMap()
    {
        Map(m => m.Code).Name("Event Code");
        Map(m => m.Name).Name("Event Description");
        Map(m => m.Description).Name("Long Description");
    }
}
