using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Services
{
    public class OrderItemDataService : IEntityService<OrderItem>
    {
        private readonly ApplicationDbContext _context;
    
        public OrderItemDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public OrderItem BasketItemToOrderItem(BasketItem basketItem, int orderId, int discount )
        {
            return new OrderItem { CatalogItemId = basketItem.CatalogItemId, OrderId = orderId, Quantity = basketItem.Quantity, UnitPrice = basketItem.UnitPrice, Discount = discount, CatalogItemName = basketItem.CatalogItemName };
        }
        public Task<int> AddOrderItem(OrderItem orderItem)
        {
            _context.ShopOrderItem.Add(orderItem);
            return _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
        {
            return await _context.ShopOrderItem.ToListAsync();
        }

        public List<Order> GetOrderItems()
        {
            return _context.ShopOrder.Where(o => o.OrderItems.Any(oi => oi.OrderId == o.Id)).ToList();
        }

        public async Task<int> UpdateOrderItem(OrderItem orderItem)
        {
            _context.ShopOrderItem.Update(orderItem);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateOrderIetmId(int orderItemId, int orderId)
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

        public async Task<OrderItem> GetByIdAsync(int guid) => await _context.ShopOrderItem.FirstOrDefaultAsync(x => x.Id == guid);

    }
}
