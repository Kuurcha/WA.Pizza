using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
[Table(nameof(Basket))]
public class Basket
{
	//NUllable reference tiype
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[Required]
	public DateTime LastModified { get; set; }
	[Required]
	public int? ApplicationUserId { get; set; }
	 
	public ApplicationUser? ApplicationUser { get; set; }

	 
	public ICollection<BasketItem>? BasketItems { get; set; }


}

