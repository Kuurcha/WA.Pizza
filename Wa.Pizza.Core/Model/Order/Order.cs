using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(Order))]
public class Order
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public OrderStatus Status { get; set; }
    public int? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }


}
