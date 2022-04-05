using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
[Table(nameof(Basket))]
public class Basket
{
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[Required]
	public DateTime LastModified { get; set; }
	[Required]
	public int ApplicationUserId { get; set; }
	[Required]
	public ApplicationUser ApplicationUser { get; set; }

	[Required]
	public ICollection<BasketItem>? BasketItems { get; set; }


}

