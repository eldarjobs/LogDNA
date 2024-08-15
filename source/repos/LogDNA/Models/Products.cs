using System.ComponentModel.DataAnnotations;

namespace LogDNA.Models;

public class Products
{
    public int Id { get; set; }
    [Required]

    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Description { get; set; }
}
