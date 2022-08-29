using Wa.Pizza.Core.Model.Advertisement;

namespace Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO
{
    public class AdvertisementDTO
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string RedicrectURL { get; set; }

        public string ImageURL { get; set; }

        public int AdvertisementClientId { get; set; }


    }
}
