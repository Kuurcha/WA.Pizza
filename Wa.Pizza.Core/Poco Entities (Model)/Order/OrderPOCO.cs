using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public enum OrderStatus
{
    Canceled,
    Cooking,
    Delivering,
    Delivered,
}
[Table(nameof(Order))]
public class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
	[Required]
	public string Description { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    [Required]
    public OrderStatus Status { get; set; }
    public Guid? ApplicationUserId { get; set; }
    public ApplicationUser? applicationUser { get; set; }
    [Required]
    public Guid OrderItemId { get; set; }
    [Required]
    public OrderItem orderItem { get; set; }

}
