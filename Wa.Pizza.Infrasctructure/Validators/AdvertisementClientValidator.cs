using FluentValidation;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class AdvertisementClientValidator: AbstractValidator<AdvertisementClientDTO>
    {
        public AdvertisementClientValidator()
        {
            RuleFor(ac => ac.Website).NotNull().NotEmpty().Length(3, 10000).WithMessage("Website for the advertisement client should not be empty");
            RuleFor(ac => ac.ApiKey).NotNull().NotEmpty().Length(34, 1024).WithMessage("Api key should not be empty and be at least 35 characters long");
            RuleFor(ac => ac.Name).NotNull().NotEmpty().Length(3, 256).WithMessage("Not a valid advertisement client name");
            
        }
    }
}
