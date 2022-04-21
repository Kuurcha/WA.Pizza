
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(OrderItem))]
public class OrderItem
{
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	//GUID заменить на инт
	[Required]
	[StringLength(30)]
	public string CatalogItemName { get; set; }
	[Column(TypeName = "decimal(18,4)")]
	public decimal UnitPrice { get; set; }
	//fluent
	[Column(TypeName = "decimal(18,4)")]
	public decimal Discount { get; set; }
	public int Quantity { get; set; }

	[Required]
	public int CatalogItemId { get; set; }

	public CatalogItem CatalogItem { get; set; }

	[Required]
	public int OrderId { get; set; }

	public Order Order { get; set; }

}

