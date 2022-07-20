using MediatR;
using Wa.Pizza.Infrasctructure.DTO.Basket;

namespace Wa.Pizza.Infrasctructure.Data.CQRS.Basket
{
    public class BasketCommands
    {
        public record InsertItemCommand(BasketItemDTO BasketItemDTO) : IRequest<int>;
        public record UpdateItemCommand(BasketItemDTO BasketItemDTO) : IRequest<int>;
        public record DeleteItemCommand(BasketItemDTO BasketItemDTO) : IRequest<int>;
        public record ClearBasketCommand(BasketDTO BasketDTO ) : IRequest<int>;
        public record BindBuyerToBasketCommand(BasketDTO BasketDTO, string ApplicationUserId): IRequest<int>;



    }
}
