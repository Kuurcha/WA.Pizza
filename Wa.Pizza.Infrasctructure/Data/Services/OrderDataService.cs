using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.DTO.Order;
using Wa.Pizza.Infrasctructure.Services.Interfaces;
using Wa.Pizza.Infrasctructure.Validators;

namespace Wa.Pizza.Infrasctructure.Services
{
    public class OrderDataService : IEntityService<GetOrderDTO>
    {
        private readonly ApplicationDbContext _context;

        public OrderDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<int> AddOrder(int basketId, string description)
        {
            Basket basket = await _context.Basket.AsNoTracking().Include(b => b.BasketItems).FirstOrDefaultAsync(b => b.Id == basketId);
            if (basket == null)
                throw new EntityNotFoundException("No basket with id: "  + basketId + " can't create order");
            if (basket.BasketItems == null)
                throw new WrongDataFormatException("Invalid basket item list");

            Order order = new Order() { Description = description, CreationDate = DateTime.UtcNow, Status = OrderStatus.Accepted, ApplicationUserId = basket.ApplicationUserId, OrderItems = new List<OrderItem>() };
            _context.ShopOrder.Add(order);
            
            foreach (BasketItem basketItem in basket.BasketItems)
            {
                OrderItem orderItem = basketItem.Adapt<OrderItem>();
                orderItem.Id = 0; //Настроить Mapster
                orderItem.OrderId = order.Id;
                order.OrderItems.Add(orderItem);
            }
            return await _context.SaveChangesAsync();
        }

        public Task<GetOrderDTO> GetById(int guid)
        {
            Task<GetOrderDTO> orderDTO = _context.ShopOrder.Where(x => x.Id == guid).Include(o => o.OrderItems).ProjectToType<GetOrderDTO>().FirstOrDefaultAsync();
            if (orderDTO == null)
                throw new EntityNotFoundException("Order with id: " + guid + " does not exists");
            return orderDTO;
        }

        public async Task<int> UpdateStatus(int orderID, OrderStatus orderStatus)
        {
            Order order = await _context.ShopOrder.FirstOrDefaultAsync(b => b.Id == orderID);
            if (order == null)
                throw new EntityNotFoundException("Order with id: " + orderID + " does not exists");
            order.Status = orderStatus;
            return await _context.SaveChangesAsync();
                
        }


    }
}
