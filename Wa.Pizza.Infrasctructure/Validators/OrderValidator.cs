using FluentValidation;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.Description).Length(3, 2000).WithMessage("Please input proper description");
            RuleFor(order => order.CreationDate).NotNull().GreaterThan(new DateTime(2022, 6, 1)).WithMessage("Please specify a valid date");
            RuleFor(order => order.Status).NotNull().WithMessage("Please specify order status");

        }
    }
}
