using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.DTO.Order;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Services
{
    public class OrderItemDataService : IEntityService<OrderItemDTO>
    {
        private readonly ApplicationDbContext _context;
    
        public OrderItemDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<int> AddOrderItem(OrderItemDTO orderItemDTO, int orderId)
        {
            OrderItem orderItem = orderItemDTO .Adapt<OrderItem>();
            orderItem.OrderId = orderId;
            _context.ShopOrderItem.Add(orderItem);
            return await _context.SaveChangesAsync();
        }

        public Task<List<OrderItemDTO>> GetOrderItemsByUserId(int userId)
        {
            return _context.ShopOrderItem.Where(oi => oi.Order.ApplicationUserId == userId).ProjectToType<OrderItemDTO>().ToListAsync();
        }
        public Task<List<OrderItemDTO>> GetOrderItemsByOrderId(int orderId)
        {
            return _context.ShopOrderItem.Where(o => o.OrderId == orderId).ProjectToType<OrderItemDTO>().ToListAsync();
        }
        public Task<OrderItemDTO> GetById(int guid)
        {
            return _context.ShopOrderItem.Where(x => x.Id == guid).ProjectToType<OrderItemDTO>().FirstAsync();
        }
        public Task<List<OrderItemDTO>> GetAllOrderItems()
        {
            return _context.ShopOrderItem.ProjectToType<OrderItemDTO>().ToListAsync();
        }

        public async Task<int> UpdateOrderItem(UpdateOrderItemDTO orderItemDTO)
        {
            OrderItem orderItem = orderItemDTO.Adapt<OrderItem>();
            orderItem.CatalogItemName = (await _context.CatalogItem.FirstAsync(ci => ci.Id == orderItem.CatalogItemId)).Name;
            _context.ShopOrderItem.Update(orderItem);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateOrderItemId(int orderItemId, int orderId)
        {
            var orderItem = new OrderItem { Id = orderItemId, OrderId = orderId};
            _context.ShopOrderItem.Attach(orderItem);
            _context.Entry(orderItem).Property(oi => oi.OrderId).IsModified = true;
            return await _context.SaveChangesAsync();
                
        }

        public async Task<int> DeleteOrderItem(int id)
        {
            OrderItem orderItem = _context.ShopOrderItem.FirstOrDefault(x => x.Id == id);
            _context.ShopOrderItem.Remove(orderItem);
            return await _context.SaveChangesAsync();
        }


    }
}
