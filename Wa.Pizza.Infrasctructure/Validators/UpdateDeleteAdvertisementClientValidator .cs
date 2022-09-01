using FluentValidation;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class UpdateDeleteAdvertisementClientValidator<T> : CreateAdvertisementClientValidator<T> where T : UpdateDeleteAdvertisementClientDTO
    {
        public UpdateDeleteAdvertisementClientValidator(): base()
        {
            RuleFor(ac => ac.Name).NotNull().NotEmpty().Length(3, 256).WithMessage("Not a valid advertisement client name"); 

        }
    }
}
