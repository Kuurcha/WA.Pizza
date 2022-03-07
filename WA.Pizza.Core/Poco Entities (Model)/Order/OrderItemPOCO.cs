
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class OrderItem
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid id { get; set; }
	[Required]
	public string CatalogItemName { get; set; }
	public decimal UnitPrice { get; set; }


	public decimal Discount { get; set; }


	public int Quantity { get; set; }

	public Guid OrderId { get; set; }

	Order order { get; set; }

}

