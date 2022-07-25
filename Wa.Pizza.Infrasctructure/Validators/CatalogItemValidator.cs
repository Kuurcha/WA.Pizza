namespace Wa.Pizza.Infrasctructure.Validators
{
    using FluentValidation;
    using Wa.Pizza.Infrasctructure.DTO.CatalogItem;

    public class CatalogItemValidator: AbstractValidator<CatalogItemDTO>
    {
        public CatalogItemValidator()
        {
            RuleFor(catalogItem => catalogItem.Name).NotEmpty().Length(3, 254).WithMessage("Invalid catalog item name");
            RuleFor(catalogItem => catalogItem.Description).NotEmpty().Length(3, 2000).WithMessage("Invalid catalog item description");
            RuleFor(catalogItem => catalogItem.Price).NotNull().GreaterThanOrEqualTo(0).WithMessage("Invalid price");
            RuleFor(catalogItem => catalogItem.Quantity).NotNull().GreaterThan(0).WithMessage("Invalid quantity");
        }
    }
}
