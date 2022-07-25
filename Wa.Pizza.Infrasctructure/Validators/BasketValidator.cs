using FluentValidation;
using Wa.Pizza.Infrasctructure.DTO.Basket;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class BasketValidator : AbstractValidator<BasketDTO>
    {
        private readonly ApplicationDbContext _context;
        public BasketValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(basket => basket.Id)
            .Must((basketId) => _context.Basket.Any(basket => basket.Id == basketId))
            .WithMessage("No basket item exists by specified id");
        }
    }
}
