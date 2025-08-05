using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Entities;

public class Personnel
{
    [Key]
    public string PersonnelCode { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}
