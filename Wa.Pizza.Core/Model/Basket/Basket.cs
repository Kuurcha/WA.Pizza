using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Core.Configuration;

[Table(nameof(Basket))]
[EntityTypeConfiguration(typeof(BasketConfiguration))]
public class Basket
{
	public int Id { get; set; }
	[Required]
    public DateTime LastModified { get; set; }

	public string ApplicationUserId { get; set; }

	public ApplicationUser? ApplicationUser { get; set; }

	public ICollection<BasketItem>? BasketItems { get; set; }


}

