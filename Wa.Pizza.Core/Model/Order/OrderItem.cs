
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(OrderItem))]
public class OrderItem
{
	
	public int Id { get; set; }
	public string CatalogItemName { get; set; }
	public decimal UnitPrice { get; set; }
	public decimal Discount { get; set; }
	public int Quantity { get; set; }

	public int CatalogItemId { get; set; }

	public CatalogItem CatalogItem { get; set; }

	public int OrderId { get; set; }

	public Order Order { get; set; }

}

