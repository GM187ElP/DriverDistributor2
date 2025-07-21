using BlazorApp1.Services;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Entities;

public class Route
{
    [Key]
    [Label("مسیر")]
    public string Name { get; set; }

    [Label("مسیر خاص")]
    public bool IsExt { get; set; }
    public ICollection<Shipment> Shipments { get; set; } = [];
}
