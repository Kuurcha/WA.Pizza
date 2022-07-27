using FluentValidation;
using Wa.Pizza.Infrasctructure.DTO.Order;

namespace Wa.Pizza.Infrasctructure.Validators
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderItemDTO>
    {
        private readonly ApplicationDbContext _context;
        public UpdateOrderValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(order => order.orderStatus).NotNull().WithMessage("No order status specified");
            RuleFor(order => order.orderId)
                .Must((orderId) => orderExists(orderId))
                .WithMessage("Order with specified id does not exist");



        }

        private bool orderExists(int orderId)
        {
            return _context.ShopOrder.Any(order => order.Id == orderId);
        }
    }
}

public class AddOrderValidator : AbstractValidator<SetOrderDTO>
{
    private readonly ApplicationDbContext _context;
    public AddOrderValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(order => order.description).Length(3, 2000).WithMessage("Invalid description");
        RuleFor(order => order.basketId)
                .Must((basketId) => basketExists(basketId))
                .WithMessage("Basket with specified id does not exist");
    }

    private bool basketExists(int basketId)
    {
        return _context.Basket.Any(basket => basket.Id == basketId);
    }


}
