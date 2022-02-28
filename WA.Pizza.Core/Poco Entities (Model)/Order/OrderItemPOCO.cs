
using System.ComponentModel.DataAnnotations;
public class OrderItemPOCO
{
	public int id { get; set; }
	[Required]
	public string CatalogItemName { get; set; }
	public decimal UnitPrice { get; set; }

	public decimal Discount { get; set; }

	public int Quantity { get; set; }

	public CatalogItem CatalogItem { get; set; }


}

