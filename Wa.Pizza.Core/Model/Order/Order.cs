using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Core.Configuration;

[Table(nameof(Order))]
[EntityTypeConfiguration(typeof(OrderConfiguration))]
public class Order
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public OrderStatus Status { get; set; }
    public string ApplicationUserId { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }


}
