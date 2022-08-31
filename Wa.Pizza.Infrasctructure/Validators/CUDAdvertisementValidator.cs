using FluentValidation;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class CUDAdvertisementValidator<T> : AbstractValidator<T> where T : CUDAdvertisementDTO
    {
        public CUDAdvertisementValidator()
        {
            RuleFor(a => a.RedicrectURL).NotNull().NotEmpty().Length(3, 10000).WithMessage("RedirectURL for the advertisement should not be empty");
            RuleFor(a => a.ImageURL).NotNull().NotEmpty().Length(3, 10000).WithMessage("ImageURL for the advertisement should not be empty");
            RuleFor(a => a.Description).NotNull().NotEmpty().Length(3, 10000).WithMessage("Not a valid description");
        }
    }
}
