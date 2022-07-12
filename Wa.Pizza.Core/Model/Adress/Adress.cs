using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Core.Configuration;

[Table(nameof(Adress))]
[EntityTypeConfiguration(typeof(AdressConfiguration))]
public class Adress
{
    public int Id { get; set; }
    public string AdressString { get; set; }
    public string? ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    

}

