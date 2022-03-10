using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class Adress
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string? AdressString { get; set; }

    public Guid ApplicationUserId { get; set; }

    ApplicationUser ApplicationUser { get; set; }


}

