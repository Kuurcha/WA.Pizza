using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Infrasctructure.Data.CQRS.Basket;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.Validators;
using static BasketQueries;
using static Wa.Pizza.Infrasctructure.Data.CQRS.Basket.BasketCommands;

class GetBasketByIdHandler : BaseBasketHandler, IRequestHandler<GetBasketByIdQuery, BasketDTO>
{
    public GetBasketByIdHandler(ApplicationDbContext _ctx, BasketItemValidator _basketItemValidator, IMediator _mediator): base(_ctx, _basketItemValidator, _mediator)
    {
        
    }
    public Task<BasketDTO> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
    {
        Task<BasketDTO> basket = context.Basket.AsNoTracking()
                                .Include(b => b.BasketItems)
                                .ProjectToType<BasketDTO>()
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();
        if (basket == null)
            throw new EntityNotFoundException("basket with user id: " + request.Id + " does not exist");
        return basket;
    }

}

class GetBasketByUserIdHandler : BaseBasketHandler, IRequestHandler<GetBasketByUserIdQuery, BasketDTO>
{
    public GetBasketByUserIdHandler(ApplicationDbContext _ctx, BasketItemValidator _basketItemValidator, IMediator _mediator) : base(_ctx, _basketItemValidator, _mediator)
    {

    }

    public Task<BasketDTO> Handle(GetBasketByUserIdQuery request, CancellationToken cancellationToken)
    {
        Task<BasketDTO> basket = context.Basket.AsNoTracking()
                                  .Include(b => b.BasketItems)
                                  .ProjectToType<BasketDTO>()
                                  .Where(x => x.ApplicationUserId == request.Id)
                                  .FirstOrDefaultAsync();
        if (basket == null)
            throw new EntityNotFoundException("basket with user id: " + request.Id + " does not exist");
        return basket;
    }
}

class InsertItemCommandHandler : BaseBasketHandler, IRequestHandler<InsertItemCommand, int>
{
    public InsertItemCommandHandler(ApplicationDbContext _ctx, BasketItemValidator _basketItemValidator, IMediator _mediator) : base(_ctx, _basketItemValidator, _mediator)
    {
    }

    public async Task<int> Handle(InsertItemCommand request, CancellationToken cancellationToken)
    {
        await validateDTOAsync(request.basketItemDTO);

        CatalogItem? catalogItem = await context.CatalogItem
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(ci => ci.Id == request.basketItemDTO.CatalogItemId);

        if (catalogItem == null)
            throw new EntityNotFoundException("Catalog item by id: " + request.basketItemDTO.CatalogItemId + " does not exist");
        BasketItem? basketItem = await context.BasketItem
                                .FirstOrDefaultAsync(bi => bi.Id == request.basketItemDTO.Id);

        Basket? basket = await context.Basket
            .Include(b => b.BasketItems)
            .FirstOrDefaultAsync(b => b.Id == request.basketItemDTO.BasketId);

        if (basketItem != null && basket.BasketItems.Any(bi => bi.BasketId == basketItem.BasketId))
        {
            basketItem.Quantity = request.basketItemDTO.Quantity;
            basket!.LastModified = DateTime.UtcNow;
            return await context.SaveChangesAsync();
        }
        else
        {
            //Мапстер перемапить?
            basketItem = catalogItem.Adapt<BasketItem>();
            basketItem.Id = 0;
            basketItem.Quantity = request.basketItemDTO.Quantity;
            basketItem.CatalogItemName = catalogItem.Name;
            basketItem.CatalogItemId = catalogItem.Id;
        }


        if (basket == null)
        {
            basket = new Basket { };
            basketItem.BasketId = basket.Id;
            context.Basket.Adapt(basket);
            context.Basket.Attach(basket).State = EntityState.Added;
        }

        if (basket!.BasketItems == null)
            basket.BasketItems = new List<BasketItem>();
        basket.BasketItems.Add(basketItem);
        basket.LastModified = DateTime.UtcNow;
        return await context.SaveChangesAsync();
    }
}

