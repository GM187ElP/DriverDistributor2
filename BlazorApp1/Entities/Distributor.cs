using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Entities;

public class Distributor
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public int? PersonnelCode { get; set; }
    public ICollection<Shipment> Shipments { get; set; } = [];
}
