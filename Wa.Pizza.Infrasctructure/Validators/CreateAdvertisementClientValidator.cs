using FluentValidation;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class CreateAdvertisementClientValidator<T> : AbstractValidator<T> where T: CreateAdvertisementClientDTO
    {
        public CreateAdvertisementClientValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty().Length(3, 256).WithMessage("Name for the advertisement should not be empty");
            RuleFor(a => a.Website).NotNull().NotEmpty().Length(3, 1000).WithMessage("Website for the advertisement should not be empty");

        }
    }
}
