using System.Globalization;

namespace BlazorApp1.Services;

public class ShipmentServices
{
    public string GregorianToPersian(DateTime myDate, char delimiter = '-')
    {
        var pc = new PersianCalendar();
        return $"{pc.GetYear(myDate)}{delimiter}{pc.GetMonth(myDate):00}{delimiter}{pc.GetDayOfMonth(myDate):00}";
    }
}
