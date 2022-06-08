namespace Wa.Pizza.Infrasctructure.Validators
{
    using FluentValidation;
    public class CatalogItemValidator: AbstractValidator<CatalogItem>
    {
        public CatalogItemValidator()
        {
            RuleFor(catalogItem => catalogItem.Name).NotEmpty().Length(3, 254).WithMessage("Input proper catalog item name");
            RuleFor(catalogItem => catalogItem.Description).NotEmpty().Length(3, 2000).WithMessage("Input proper catalog item description");
            RuleFor(basketItem => basketItem.Price).NotNull().GreaterThanOrEqualTo(0).WithMessage("Please specify a valid price");
            RuleFor(basketItem => basketItem.Quantity).NotNull().GreaterThan(0).WithMessage("Please specify a valid Quantity");
        }
    }
}
