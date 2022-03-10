using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(ApplicationUser))]
public class ApplicationUser
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }
	[Required]
	public Guid? adressID { get; set; }
	[Required]
	public Adress adress { get; set; }

	[Required]
	public Basket basket { get; set; }

	public ICollection<Order>? Orders { get; set; }


}
