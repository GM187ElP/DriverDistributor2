using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BlazorApp1.Entities;

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
}

public class ShipmentAddDto
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
    public int? InvoiceCount { get; set; } 
    public long? InvoiceAmount { get; set; } 
    public List<int> ShipmentNumbers { get; set; } = [];
    public int? ReturnInvoiceCount { get; set; } = 0;
    public long? ReturnInvoiceAmount { get; set; } = 0;
    public int? SecondServiceInvoiceCount { get; set; } 
    public int? ThirdServiceInvoiceCount { get; set; } 
    public long? SecondServiceInvoiceAmount { get; set; } 
    public long? ThirdServiceInvoiceAmount { get; set; } 

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
}