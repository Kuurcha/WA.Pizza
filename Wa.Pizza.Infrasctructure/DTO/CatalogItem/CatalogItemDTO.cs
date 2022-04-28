using Wa.Pizza.Infrasctructure.DTO.Basket;
using WA.Pizza.Core.CatalogType;

namespace Wa.Pizza.Infrasctructure.DTO.CatalogItem
{
    public class CatalogItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public CatalogType? CatalogType { get; set; }
        
        public BasketItemDTO ToBasketItemDto(int quantity, int basketId)
        {
            return new BasketItemDTO {  CatalogItemId = Id, CatalogItemName = Name, UnitPrice = Price, CatalogType = CatalogType, Quantity = quantity, BasketId = basketId };
        }
    }
}
