using WA.Pizza.Core.CatalogType;

namespace Wa.Pizza.Infrasctructure.DTO.Basket
{
    public class BasketItemDTO
    {
		public int Id { get; set; }
		public string CatalogItemName { get; set; }
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }

		public CatalogType? CatalogType { get; set; }
		// public int BasketId { get; set; }
		//Не  нужен, так как в воркфлоу BasketItem нужен для преобразования в OrderItem, а список Basket можно получить из GetBasketDTO
		public int CatalogItemId { get; set; }
	}
}
