using WA.Pizza.Core.CatalogType;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(CatalogItem))]
public class CatalogItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    [Required]
    [StringLength(254)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }
    [Required]
    public CatalogType? CatalogType { get; set; }

    [Required]
    public OrderItem orderItem { get; set; }
    [Required]
    public BasketItem basketItem { get; set; }
}
