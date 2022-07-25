namespace Wa.Pizza.Infrasctructure.Validators
{
    using FluentValidation;
    using Wa.Pizza.Infrasctructure.DTO.Basket;

    public class BasketItemValidator: AbstractValidator<BasketItemDTO>
    {
        public BasketItemValidator()
        {
            RuleFor(basketItem => basketItem.UnitPrice).NotNull().GreaterThanOrEqualTo(0).WithMessage("Not a valid price");
            RuleFor(basketItem => basketItem.Quantity).NotNull().GreaterThan(0).WithMessage("Not a valid quantity");
            RuleFor(basketItem => basketItem.CatalogType).NotNull().WithMessage("Catalog type should not be null");
            RuleFor(basketItem => basketItem.CatalogItemName).Length(2, 250).WithMessage("Not a valid catalog mame");
        }
    }


}
