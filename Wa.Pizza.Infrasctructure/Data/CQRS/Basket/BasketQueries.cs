using MediatR;
using Wa.Pizza.Infrasctructure.DTO.Basket;

public class BasketQueries
{
    public record GetBasketByIdQuery(int Id): IRequest<BasketDTO>;
    public record GetBasketByUserIdQuery(string Id): IRequest<BasketDTO>;

}

