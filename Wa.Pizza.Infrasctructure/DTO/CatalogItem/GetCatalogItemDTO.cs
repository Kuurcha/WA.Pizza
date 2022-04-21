using WA.Pizza.Core.CatalogType;

namespace Wa.Pizza.Infrasctructure.DTO.CatalogItem
{
    public class GetCatalogItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public CatalogType? CatalogType { get; set; }
    }
}
