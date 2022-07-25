namespace Wa.Pizza.Infrasctructure.Validators
{
    using FluentValidation;
    public class BasketItemValidator: AbstractValidator<BasketItem>
    {
        public BasketItemValidator()
        {
            //Заинджектить
            RuleFor(basketItem => basketItem.UnitPrice).NotNull().GreaterThanOrEqualTo(0).WithMessage("Please specify a valid price");
            RuleFor(basketItem => basketItem.Quantity).NotNull().GreaterThan(0).WithMessage("Please specify a valid Quantity");
            RuleFor(basketItem => basketItem.CatalogType).NotNull();
            RuleFor(basketItem => basketItem.CatalogItemName).Length(2, 250).WithMessage("Not a valid CatalogName");
        }
    }
}
