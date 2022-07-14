using MediatR;
using Wa.Pizza.Infrasctructure.DTO.Basket;

namespace Wa.Pizza.Infrasctructure.Data.CQRS.Basket
{
    public class BasketCommands
    {
        public record InsertItemCommand(BasketItemDTO basketItemDTO) : IRequest<int>;
        public record UpdateItemCommand(BasketItemDTO basketItemDTO) : IRequest;
        public record DeleteItemCommand(BasketItemDTO basketItemDTO) : IRequest;
        public record ClearBasketCommand(BasketDTO BasketDTO ) : IRequest;
        public record BindBuyerToBasket(BasketDTO BasketDTO, string applicationUserId);



    }
}
