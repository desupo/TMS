using CsvHelper.Configuration;
using TMS.domain.Entities;

namespace TMS.infra.Data.Mapping;

public class CityMap : ClassMap<City>
{
    public CityMap()
    {
        Map(m => m.Id).Name("City Id");
        Map(m => m.Name).Name("City Name");
        Map(m => m.TimeZone).Name("Time Zone");
    }
}
