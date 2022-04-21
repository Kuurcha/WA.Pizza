namespace Wa.Pizza.Infrasctructure.DTO.Adress
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    public class AdressDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AdressString { get; set; }
    }
}
