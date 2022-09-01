using MediatR;
using Wa.Pizza.Infrasctructure.DTO.Basket;

namespace Wa.Pizza.Infrasctructure.Data.CQRS.Basket
{
    public class BasketCommands
    {
        public record InsertItemCommand(BasketItemDTO BasketItemDTO) : IRequest<Unit>;
        public record UpdateItemCommand(BasketItemDTO BasketItemDTO) : IRequest<Unit>;
        public record DeleteItemCommand(BasketItemDTO BasketItemDTO) : IRequest<Unit>;
        public record ClearBasketCommand(BasketDTO BasketDTO ) : IRequest<Unit>;
        public record BindBuyerToBasketCommand(BasketDTO BasketDTO, string ApplicationUserId): IRequest<Unit>;



    }
}
