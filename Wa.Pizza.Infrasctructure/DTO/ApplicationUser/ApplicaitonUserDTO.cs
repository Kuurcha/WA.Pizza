using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wa.Pizza.Infrasctructure.DTO.Adress;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.DTO.Order;
namespace Wa.Pizza.Infrasctructure.DTO.ApplicationUser
{
    public class ApplicationUserDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<AdressDTO>? Adresses { get; set; }

        public BasketDTO Basket { get; set; }

        public ICollection<GetOrderDTO>? Orders { get; set; }
    }
}
