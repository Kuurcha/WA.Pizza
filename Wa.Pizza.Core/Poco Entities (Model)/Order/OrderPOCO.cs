using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public enum OrderStatus
{
    Canceled,
    Cooking,
    Delivering,
    Delivered,
}
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

}
