using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TMS.infra.Data.Mapping;

public class DateTimeOffsetConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (DateTimeOffset.TryParse(text, out var result))
        {
            return result;
        }
        return base.ConvertFromString(text, row, memberMapData);
    }
}