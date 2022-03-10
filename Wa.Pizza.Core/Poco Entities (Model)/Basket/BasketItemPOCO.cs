using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WA.Pizza.Core.CatalogType;

[Table(nameof(BasketItem))]
public class BasketItem
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }
	[Required]
	[StringLength(30)]
	public string CatalogItemName { get; set; }
	[Required]
	[Column(TypeName = "decimal(18,4)")]
	public decimal UnitPrice { get; set; }
	[Required]
	public int Quantity { get; set; }
	[Required]
	public CatalogType CatalogType { get; set; }
	[Required]	
	public Guid CatalogItemId { get; set; }

	[Required]
	public CatalogItem catalogItem { get; set; }



}