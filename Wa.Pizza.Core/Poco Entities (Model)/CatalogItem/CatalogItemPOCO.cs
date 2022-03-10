using WA.Pizza.Core.CatalogType;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class CatalogItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
        
    public decimal Price { get; set; }

    public int Quantity { get; set; }
    [Required]
    public CatalogType CatalogType { get; set; }

    [Required]
    public Guid BasketItemId { get; set; }
    [Required]
    public Guid ShopOrderItemId { get; set; }
    [Required]
    public BasketItem basketItem { get; set; }
    [Required]
    public OrderItem orderItem { get; set; }


}
