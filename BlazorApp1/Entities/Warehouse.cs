using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Entities;

public class Warehouse
{
    [Key]
    [Label("انبار")]
    public string Name { get; set; }
    public ICollection<Shipment> Shipments { get; set; } = [];
}
