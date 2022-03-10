
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(OrderItem))]
public class OrderItem
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }
	[Required]
	[StringLength(30)]
	public string CatalogItemName { get; set; }
	[Column(TypeName = "decimal(18,4)")]
	public decimal UnitPrice { get; set; }

	[Column(TypeName = "decimal(18,4)")]
	public decimal Discount { get; set; }
	public int Quantity { get; set; }

	[Required]
	public Guid CatalogItemId { get; set; }

	[Required]
	public CatalogItem catalogItem { get; set; }

	[Required]

	public ICollection<Order> Orders { get; set; }
}

