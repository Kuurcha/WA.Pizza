namespace Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO
{
    public class UpdateDeleteAdvertisementClientDTO: CreateAdvertisementClientDTO
    {

        public string Name { get; set; }

        public string Website { get; set; }

        public string ApiKey { get; set; }

    }
}
