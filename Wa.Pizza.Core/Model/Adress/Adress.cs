using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
[Table(nameof(Adress))]
public class Adress
{
    public int Id { get; set; }
    public string AdressString { get; set; }
    [Required]
    public int ApplicationUserId { get; set; }  
    public ApplicationUser ApplicationUser { get; set; }
    
    

}

