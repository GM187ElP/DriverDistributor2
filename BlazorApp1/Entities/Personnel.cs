using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Entities;

public class Personnel
{
    [Key]
    public int PersonnelCode { get; set; }
    public string Name { get; set; }
}
