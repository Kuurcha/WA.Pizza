﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WA.Pizza.Core.CatalogType;

[Table(nameof(BasketItem))]
public class BasketItem
{
	
	public int Id { get; set; }
	public string CatalogItemName { get; set; }
	public decimal UnitPrice { get; set; }
	public int Quantity { get; set; }
	public CatalogType? CatalogType { get; set; }

	public int BasketId { get; set; }
	public Basket Basket { get; set; }
	public int CatalogItemId { get; set; }
	public CatalogItem CatalogItem { get; set; }



}