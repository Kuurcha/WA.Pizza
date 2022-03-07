using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class RefreshToken
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string RefreshTokenString { get; set; }
    [Required]
    public DateTime RefresthTokenExpiration { get; set; }
    [Required]
    public Guid ApplicationUserId { get; set; }
    [Required]
    ApplicationUser ApplicationUser { get; set; }

}
