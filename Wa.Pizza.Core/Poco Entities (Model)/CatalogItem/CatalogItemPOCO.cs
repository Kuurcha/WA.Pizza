using WA.Pizza.Core.CatalogType;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(CatalogItem))]
public class CatalogItem
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(254)]
    public string Name { get; set; }
    [Required]
    [StringLength(2000)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }
    [Required]
    public CatalogType? CatalogType { get; set; }

    [Required]

    public ICollection<BasketItem>? BasketItems { get; set; }

    [Required]
    public ICollection<OrderItem>? OrderItems { get; set; }
}
