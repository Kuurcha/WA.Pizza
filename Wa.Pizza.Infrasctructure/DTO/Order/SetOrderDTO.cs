namespace Wa.Pizza.Infrasctructure.DTO.Order
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    public class SetOrderDTO
    {
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
