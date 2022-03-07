using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WA.Pizza.Core.CatalogType;

public class BasketItem
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }
	[Required]
	public string CatalogItemName { get; set; }
	[Required]
	public decimal UnitPrice { get; set; }
	[Required]
	public int Quantity { get; set; }
	[Required]
	public CatalogType CatalogType { get; set; }
	[Required]

	public Guid basketId { get; set; }
	[Required]
	public Basket basket { get; set; }




}