using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class ApplicationUser
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }
	[Required]
	public Guid OrderID { get; set; }
	[Required]
	public Guid BasketId { get; set; }
	[Required]
	public Order order { get; set; }
	public Basket basket { get; set; }


}
