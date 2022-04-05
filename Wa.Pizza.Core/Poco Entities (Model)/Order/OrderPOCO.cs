using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table(nameof(Order))]
public class Order
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
	[Required]
    [StringLength(2000)]
    public string Description { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    [Required]
    public OrderStatus Status { get; set; }
    public int? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    [Required]
    public ICollection<OrderItem> OrderItems { get; set; }


}
