using Wa.Pizza.Core.Model.Advertisement;

namespace Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO
{
    public class AdvertisementDTO: CUDAdvertisementDTO
    {
        public int Id { get; set; }
        public int AdvertisementClientId { get; set; }


    }
}
