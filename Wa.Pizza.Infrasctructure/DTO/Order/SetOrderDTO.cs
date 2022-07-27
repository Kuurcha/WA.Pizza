namespace Wa.Pizza.Infrasctructure.DTO.Order
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    public class SetOrderDTO
    {
        public int basketId { get; set; }
        public string description {get; set;}
    }
}
