using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(ApplicationUser))]
public class ApplicationUser
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[Required]
	public int? AdressID { get; set; }
	[Required]
	public Adress Adress { get; set; }

	[Required]
	public Basket Basket { get; set; }

	public ICollection<Order>? Orders { get; set; }


}
