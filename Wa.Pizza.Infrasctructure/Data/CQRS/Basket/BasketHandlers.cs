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
    public GetBasketByIdHandler(ApplicationDbContext _ctx): base(_ctx)
    {
        
    }
    public Task<BasketDTO> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
    {
        Task<BasketDTO?> basket = context.Basket.AsNoTracking()
                                .Include(b => b.BasketItems)
                                .ProjectToType<BasketDTO>()
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();
        if (basket == null)
            throw new EntityNotFoundException("basket with user id: " + request.Id + " does not exist");
        return basket!;
    }

}

class GetBasketByUserIdHandler : BaseBasketHandler, IRequestHandler<GetBasketByUserIdQuery, BasketDTO>
{
    public GetBasketByUserIdHandler(ApplicationDbContext _ctx) : base(_ctx)
    {

    }

    public Task<BasketDTO> Handle(GetBasketByUserIdQuery request, CancellationToken cancellationToken)
    {
        Task<BasketDTO?> basket = context.Basket.AsNoTracking()
                                  .Include(b => b.BasketItems)
                                  .ProjectToType<BasketDTO>()
                                  .Where(x => x.ApplicationUserId == request.Id)
                                  .FirstOrDefaultAsync();
        if (basket == null)
            throw new EntityNotFoundException("basket with user id: " + request.Id + " does not exist");
        return basket!;
    }
}

class InsertItemCommandHandler : BaseBasketHandler, IRequestHandler<InsertItemCommand, int>
{
    public InsertItemCommandHandler(ApplicationDbContext _ctx) : base(_ctx)
    {
    }

    public async Task<int> Handle(InsertItemCommand request, CancellationToken cancellationToken)
    {

        CatalogItem? catalogItem = await context.CatalogItem
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(ci => ci.Id == request.BasketItemDTO.CatalogItemId);

        if (catalogItem == null)
            throw new EntityNotFoundException("Catalog item by id: " + request.BasketItemDTO.CatalogItemId.ToString() + " does not exist");
        BasketItem? basketItem = await context.BasketItem
                                .FirstOrDefaultAsync(bi => bi.Id == request.BasketItemDTO.Id);

        Basket? basket = await context.Basket
            .Include(b => b.BasketItems)
            .FirstOrDefaultAsync(b => b.Id == request.BasketItemDTO.BasketId);

        if (basketItem != null && basket != null && basket.BasketItems!.Any(bi => bi.BasketId == basketItem.BasketId))
        {
            basketItem.Quantity = request.BasketItemDTO.Quantity;
            basket!.LastModified = DateTime.UtcNow;
            return await context.SaveChangesAsync();
        }
        else
        {
            basketItem = catalogItem.Adapt<BasketItem>();
            basketItem.Id = 0;
            basketItem.Quantity = request.BasketItemDTO.Quantity;
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

class UpdateItemCommandHandler : BaseBasketHandler, IRequestHandler<UpdateItemCommand, int>
{
    public UpdateItemCommandHandler(ApplicationDbContext _ctx) : base(_ctx)
    {
    }


    public async Task<int> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {

        BasketItem? originalBasketItem = await context!.BasketItem!.Include(bi => bi!.Basket)!.FirstOrDefaultAsync(x => x!.Id == request!.BasketItemDTO!.Id);
        if (originalBasketItem == null)
            throw new EntityNotFoundException("Basket item with id: " + request.BasketItemDTO.Id + " not found. Nothing to update.");
        if (request.BasketItemDTO.Quantity <= 0)
            context.BasketItem.Remove(originalBasketItem);
        else
            originalBasketItem.Quantity = request.BasketItemDTO.Quantity;
        originalBasketItem.Basket.LastModified = DateTime.UtcNow;
        return await context.SaveChangesAsync();
    }
}

class DeleteItemCommandHandler : BaseBasketHandler, IRequestHandler<DeleteItemCommand, int>
{
    public DeleteItemCommandHandler(ApplicationDbContext _ctx) : base(_ctx)
    {
    }


    public async Task<int> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {

        Basket? basket = await context.Basket.Include(b => b.BasketItems).FirstOrDefaultAsync(b => b.Id == request.BasketItemDTO.BasketId);
        if (basket == null)
            throw new EntityNotFoundException("Basket with id: " + request.BasketItemDTO.BasketId + "does not exists. Unable to delete");
        if (basket.BasketItems == null || basket.BasketItems.Where(bi => bi.Id == request.BasketItemDTO.Id) == null)
            throw new EntityNotFoundException("BasketItem with id: " + request.BasketItemDTO.Id + "does not exists. Unable to delete");

        basket.LastModified = DateTime.UtcNow;
        var basketItems = basket.BasketItems.Where(bi => bi.Id == request.BasketItemDTO.Id);
        foreach (var basketItem in basketItems)
        {
            basket.BasketItems.Remove(basketItem);
        }
        return await context.SaveChangesAsync();
    }
}

class ClearBasketCommandHandler : BaseBasketHandler, IRequestHandler<ClearBasketCommand, int>
{
    public ClearBasketCommandHandler(ApplicationDbContext context) : base(context)
    {
    }


    public async Task<int> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
    {
        Basket? basket = await context.Basket.Include(b => b.BasketItems).FirstOrDefaultAsync(b => b.Id == request.BasketDTO.Id);
        if (basket == null)
            throw new EntityNotFoundException("Basket with id: " + request.BasketDTO.Id + "does not exists. Unable to delete");
        if (basket.BasketItems!= null)
            basket.BasketItems.Clear();
        return await context.SaveChangesAsync();
    }
}

class BindBuyerToBasketCommandHandler : BaseBasketHandler, IRequestHandler<BindBuyerToBasketCommand, int>
{
    public BindBuyerToBasketCommandHandler(ApplicationDbContext _ctx) : base(_ctx)
    {
    }


    public async Task<int> Handle(BindBuyerToBasketCommand request, CancellationToken cancellationToken)
    {
        Basket? basket = await context.Basket.FirstOrDefaultAsync(b => b.Id == request.BasketDTO.Id);
        if (basket == null)
            throw new EntityNotFoundException("Basket with id: " + request.BasketDTO.Id + "does not exists. Unable to bind");
        if (basket.ApplicationUser != null)
            throw new UserAlreadyBindedException("Basket with id: " + request.BasketDTO.Id + " already has a binded user. Unable to bind");
        ApplicationUser? applicationUser = await context.ApplicationUser.FirstOrDefaultAsync(a => a.Id == request.ApplicationUserId);
        basket.ApplicationUser = applicationUser;
        return await context.SaveChangesAsync();
    }
}