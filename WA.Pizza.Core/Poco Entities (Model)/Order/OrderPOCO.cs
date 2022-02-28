using System;
using System.ComponentModel.DataAnnotations;
using WA.Pizza
enum OrderStatus
{
    Canceled,
    Cooking,
    Delivering,
    Delivered,
}
public class Order
{
	public int id { get; set; }
	[Required]
	public string[] Description { get; set; }

	public DateTime CreationDate { get; set; }

	public OrderStatus Status { get; set; } 

	public ApplicationUser ApplicationUser { get; set; }
}
