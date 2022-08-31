using FluentValidation;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class AdvertisementValidator : CUDAdvertisementValidator<AdvertisementDTO>
    {
        public AdvertisementValidator(): base()
        {

        }
    }
}
