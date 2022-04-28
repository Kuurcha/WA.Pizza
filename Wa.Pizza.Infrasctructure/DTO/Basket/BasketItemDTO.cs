using System.ComponentModel.DataAnnotations;
using WA.Pizza.Core.CatalogType;

namespace Wa.Pizza.Infrasctructure.DTO.Basket
{
    public class BasketItemDTO
    {
		public int Id { get; set; }
		[Required]
		public string CatalogItemName { get; set; }
		[Required]
		public decimal UnitPrice { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public int BasketId { get; set; }
		[Required]
		public CatalogType? CatalogType { get; set; }
		[Required]
		public int CatalogItemId { get; set; }
	}
}
