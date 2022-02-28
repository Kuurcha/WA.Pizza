using System;
using System.ComponentModel.DataAnnotations;

public class Basket
{
	public int id { get; set; }
	[Required]
	public DateTime LastModified { get; set; }

	public ApplicationUser ApplicationUser { get; set; }

	public BasketItem[] BasketItems { get; set; }
	public CatalogItem CatalogItem { get; set; }
}

