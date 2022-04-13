using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
[Table(nameof(Adress))]
public class Adress
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(254)]
    public string AdressString { get; set; }
    [Required]
    public int ApplicationUserId { get; set; }

    [Required]
    public ApplicationUser? ApplicationUser { get; set; }
    


}

