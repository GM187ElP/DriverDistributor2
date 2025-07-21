using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace BlazorApp1.Entities;


public class Shipment
{
    private DateTime? _shipmentDateGregorian;
    private string _shipmentDatePersian;
    private string _persianWeekday;
    public long Id { get; set; }

    public DateTime? ShipmentDateGregorian
    {
        get => _shipmentDateGregorian;
        set
        {
            _shipmentDateGregorian = value;
            _shipmentDatePersian = GregorianToPersian(value);
            _persianWeekday = PersianWeekday(value);
        }
    }

    public string ShipmentDatePersian
    {
        get => _shipmentDatePersian;
        private set => _shipmentDatePersian = value;
    }
    public string Weekday
    {
        get => _persianWeekday;
        private set => _persianWeekday = value;
    }

    public int? DriverPersonnelCode { get; set; }
    public int? DistributorPersonnelCode { get; set; }
    public string DriverName { get; set; }
    public string DistributorName { get; set; }
    public string RouteName { get; set; }
    public string WarehouseName { get; set; }
    public int? InvoiceCount { get; set; } = 0;
    public long? InvoiceAmount { get; set; } = 0;
    public ICollection<ShipmentNumber> ShipmentNumbers { get; set; } = [];
    public int? ReturnInvoiceCount { get; set; } = 0;
    public long? ReturnInvoiceAmount { get; set; } = 0;
    public int? SecondServiceInvoiceCount { get; set; } = 0;
    public int? ThirdServiceInvoiceCount { get; set; } = 0;
    public long? SecondServiceInvoiceAmount { get; set; } = 0;
    public long? ThirdServiceInvoiceAmount { get; set; } = 0;

    public int? NetInvoiceCount => InvoiceCount + SecondServiceInvoiceCount + ThirdServiceInvoiceCount - ReturnInvoiceCount;
    public long? NetInvoiceAmount => InvoiceAmount + SecondServiceInvoiceAmount + ThirdServiceInvoiceAmount - ReturnInvoiceAmount;

    public bool HasVip { get; set; } = false;

    public bool IsException { get; set; } = false;

    public Driver Driver { get; set; }
    public Route Route { get; set; }
    public Warehouse Warehouse { get; set; }
    public Distributor Distributor { get; set; }

    private string PersianWeekday(DateTime? date)
    {
        if (date is null)
            return "نا معتبر";
        return date.Value.DayOfWeek switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یکشنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
            DayOfWeek.Thursday => "پنجشنبه",
            DayOfWeek.Friday => "جمعه",
            _ => "نا معتبر"
        };
    }
    private string? GregorianToPersian(DateTime? date)
    {
        if (date is null) return null;
        var pc = new PersianCalendar();
        var d = date.Value;
        return $"{pc.GetYear(d):0000}/{pc.GetMonth(d):00}/{pc.GetDayOfMonth(d):00}";
    }




    public bool Equals(Shipment? other)
    {
        if (other == null)
            return false;

        var properties = typeof(Shipment).GetProperties();

        foreach (var prop in properties)
        {
            var thisValue = prop.GetValue(this);
            var otherValue = prop.GetValue(other);
            if (prop.Name == "ShipmentNumbers")
            {
                var thisNums = (thisValue as string).Split(',') ?? [];
                var otherNums = (otherValue as string).Split(',') ?? [];

                if (!thisNums.All(otherNums.Contains) || !otherNums.All(thisNums.Contains))
                    return false;

                continue;
            }

            if (thisValue == null && otherValue == null)
                continue;



            if (thisValue == null || otherValue == null)
                return false;

            if (!thisValue.Equals(otherValue))
                return false;
        }

        return true;
    }

}

public class ShipmentNumber
{
    public long Id { get; set; }
    public int Number { get; set; } = 0;
    public long ShipmentId { get; set; }
    public Shipment Shipment { get; set; }
}

public class ShipmentWithIndex
{
    public int Index { get; set; }
    public Shipment Shipment { get; set; }
    public string ShipmentNumbers => GetShipmentNumbers();


    public string InvoiceCountFormatted => string.Format("{0:N0}", Shipment.InvoiceCount);
    public string InvoiceAmountFormatted => string.Format("{0:N0}", Shipment.InvoiceAmount);
    public string ReturnInvoiceCountFormatted => string.Format("{0:N0}", Shipment.ReturnInvoiceCount);
    public string ReturnInvoiceAmountFormatted => string.Format("{0:N0}", Shipment.ReturnInvoiceAmount);
    public string SecondServiceInvoiceCountFormatted => string.Format("{0:N0}", Shipment.SecondServiceInvoiceCount);
    public string ThirdServiceInvoiceCountFormatted => string.Format("{0:N0}", Shipment.ThirdServiceInvoiceCount);
    public string SecondServiceInvoiceAmountFormatted => string.Format("{0:N0}", Shipment.SecondServiceInvoiceAmount);
    public string ThirdServiceInvoiceAmountFormatted => string.Format("{0:N0}", Shipment.ThirdServiceInvoiceAmount);
    public string NetInvoiceCountFormatted => string.Format("{0:N0}", Shipment.NetInvoiceCount);
    public string NetInvoiceAmountFormatted => string.Format("{0:N0}", Shipment.NetInvoiceAmount);


    string GetShipmentNumbers()
    {
        var list = Shipment?.ShipmentNumbers?
               .Select(x => x.Number) ?? Enumerable.Empty<int>();
        var numbers = string.Join("-", list);
        return numbers;
    }
}


public class ShipmentEditDto
{
    private DateTime? _shipmentDateGregorian;
    private string _shipmentDatePersian;
    public long Id { get; set; }

    public DateTime? ShipmentDateGregorian
    {
        get => _shipmentDateGregorian;
        set
        {
            _shipmentDateGregorian = value;
            _shipmentDatePersian = GregorianToPersian(value);
        }
    }

    public string ShipmentDatePersian
    {
        get => _shipmentDatePersian;
        private set => _shipmentDatePersian = value;
    }

    public int? DriverPersonnelCode { get; set; }
    public int? DistributorPersonnelCode { get; set; }
    public string DriverName { get; set; }
    public string DistributorName { get; set; }
    public string RouteName { get; set; }
    public string WarehouseName { get; set; }
    public int? InvoiceCount { get; set; } = 0;
    public long? InvoiceAmount { get; set; } = 0;
    public List<int> ShipmentNumbers { get; set; } = [];
    public int? ReturnInvoiceCount { get; set; } = 0;
    public long? ReturnInvoiceAmount { get; set; } = 0;
    public int? SecondServiceInvoiceCount { get; set; } = 0;
    public int? ThirdServiceInvoiceCount { get; set; } = 0;
    public long? SecondServiceInvoiceAmount { get; set; } = 0;
    public long? ThirdServiceInvoiceAmount { get; set; } = 0;

    public bool HasVip { get; set; } = false;

    public bool IsException { get; set; } = false;


    private string? GregorianToPersian(DateTime? date)
    {
        if (date is null) return null;
        var pc = new PersianCalendar();
        var d = date.Value;
        return $"{pc.GetYear(d):0000}/{pc.GetMonth(d):00}/{pc.GetDayOfMonth(d):00}";
    }

    public bool Equals(Shipment? other)
    {
        if (other == null)
            return false;

        var properties = typeof(Shipment).GetProperties();

        foreach (var prop in properties)
        {
            var thisValue = prop.GetValue(this);
            var otherValue = prop.GetValue(other);
            if (prop.Name == "ShipmentNumbers")
            {
                var thisNums = (thisValue as string).Split('-') ?? [];
                var otherNums = (otherValue as string).Split('-') ?? [];

                if (!thisNums.All(otherNums.Contains) || !otherNums.All(thisNums.Contains))
                    return false;

                continue;
            }

            if (thisValue == null && otherValue == null)
                continue;



            if (thisValue == null || otherValue == null)
                return false;

            if (!thisValue.Equals(otherValue))
                return false;
        }

        return true;
    }

    public void MapFrom(Shipment from)
    {
        var dtoProps = typeof(ShipmentEditDto).GetProperties();
        var mainProps = typeof(Shipment).GetProperties();

        foreach (var dtoProp in dtoProps)
        {
            var mainProp = mainProps.FirstOrDefault(p => p.Name == dtoProp.Name && p.PropertyType == dtoProp.PropertyType);

            if (mainProp != null && dtoProp.CanWrite)
            {
                var value = mainProp.GetValue(from);
                dtoProp.SetValue(this, value);
            }
        }
    }

    public void MapTo<T>(T to)
    {
        var dtoProps = this.GetType().GetProperties();
        var targetProps = typeof(T).GetProperties();

        foreach (var dtoProp in dtoProps)
        {
            var targetProp = targetProps.FirstOrDefault(p => p.Name == dtoProp.Name && p.PropertyType == dtoProp.PropertyType);

            if (targetProp != null && targetProp.CanWrite)
            {
                var value = dtoProp.GetValue(this);
                targetProp.SetValue(to, value);
            }
        }
    }
}
