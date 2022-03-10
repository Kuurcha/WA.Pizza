using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
[Table(nameof(Basket))]
public class Basket
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key]
	public Guid Id { get; set; }
	[Required]
	public DateTime LastModified { get; set; }
	[Required]
	public Guid ApplicationUserId { get; set; }
	[Required]
	ApplicationUser applicationUser { get; set; }

	public Guid? BasketItem { get; set; }

	public BasketItem? basketItem { get; set; }


}

