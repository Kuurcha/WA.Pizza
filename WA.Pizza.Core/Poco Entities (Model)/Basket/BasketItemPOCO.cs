using System;
using System.ComponentModel.DataAnnotations;
using WA.Pizza.Core.CatalogType;

public class BasketItem
{
	public int id { get; set; }
	[Required]
	public string CatalogItemName { get; set; }
	public decimal UnitPrice { get; set; }
	public int Quantity { get; set; }
	public CatalogType CatalogType { get; set; }
	
	public CatalogItem CatalogItem { get; set; }


}