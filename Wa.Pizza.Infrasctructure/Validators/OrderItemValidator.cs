using FluentValidation;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(orderItem => orderItem.CatalogItemName).NotEmpty().Length(3, 254).WithMessage("Please input proper catalog item name");
            RuleFor(orderItem => orderItem.UnitPrice).NotNull().GreaterThan(0).WithMessage("Please specify a valid price");
            RuleFor(orderItem => orderItem.Discount).NotNull().GreaterThanOrEqualTo(0).WithMessage("Please specify a valid discount");
            RuleFor(orderItem => orderItem.CatalogItemId).NotNull().WithMessage("Please specify proper catalog item that order item is based on");
            RuleFor(orderItem => orderItem.OrderId).NotNull().WithMessage("Please specify proper order that order items belongs to");
            RuleFor(orderItem => orderItem.Discount).NotNull().GreaterThanOrEqualTo(0).WithMessage("Please specify a valid Quantity");
        }
    }
}
