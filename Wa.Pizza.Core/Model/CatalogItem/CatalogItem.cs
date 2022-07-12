 using WA.Pizza.Core.CatalogType;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Core.Configuration;

[Table(nameof(CatalogItem))]
[EntityTypeConfiguration(typeof(CatalogItemConfiguration))]
public class CatalogItem
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public CatalogType? CatalogType { get; set; }    
    public ICollection<BasketItem>? BasketItems { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }
}
