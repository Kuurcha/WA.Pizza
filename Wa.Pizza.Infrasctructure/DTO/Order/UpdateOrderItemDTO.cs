namespace Wa.Pizza.Infrasctructure.DTO.Order
{
    public class UpdateOrderItemDTO
    {
        public int orderId { get; set; }
        public OrderStatus orderStatus { get; set; }
    }
}
