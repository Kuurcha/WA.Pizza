namespace Wa.Pizza.Infrasctructure.DTO.Order
{
    public class UpdateOrderItemDTO
    {
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
    }
}
