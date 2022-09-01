using FluentValidation;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class AdvertisementClientValidator: UpdateDeleteAdvertisementClientValidator<AdvertisementClientDTO>
    {
        public AdvertisementClientValidator(): base()
        {

        }
    }
}
