using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(ApplicationUser))]
public class ApplicationUser
{
	[Required]
	public int Id { get; set; }
	public ICollection<Adress>? Adresses { get; set; }

	public Basket Basket { get; set; }

	public ICollection<Order>? Orders { get; set; }


}
