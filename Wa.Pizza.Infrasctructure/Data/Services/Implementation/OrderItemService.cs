using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Services
{
    public class OrderItemService : IEntityService<OrderItem>
    {
        private readonly ApplicationDbContext _context;
    
        public OrderItemService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public Task<int> AddOrderItem(OrderItem orderItem)
        {
            _context.ShopOrderItem.Add(orderItem);
            return _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByUserId(int userId)
        {
            return await _context.ShopOrderItem.Where(oi => oi.Order.ApplicationUserId == userId).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByOrderId(int orderId)
        {
            return await _context.ShopOrderItem.Where(o => o.OrderId == orderId).ToListAsync();
        }


        public int UpdateOrderId(int orderItemId, int orderId)
        {
            var orderItem = new OrderItem { Id = orderItemId, OrderId = orderId};
            _context.ShopOrderItem.Attach(orderItem);
            _context.Entry(orderItem).Property(oi => oi.OrderId).IsModified = true;
             return _context.SaveChanges();
                
        }

        public async Task<OrderItem> GetByIdAsync(int guid) => await _context.ShopOrderItem.FirstOrDefaultAsync(x => x.Id == guid);

    }
}
