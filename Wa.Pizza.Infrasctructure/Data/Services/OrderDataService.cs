using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Services
{
    public class OrderDataService : IEntityService<Order>
    {
        private readonly ApplicationDbContext _context;
        
        
        public OrderDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public Task<int> AddOrder(Order order)
        {
            _context.ShopOrder.Add(order);
            return _context.SaveChangesAsync();
        }

        public async Task<Order> GetByIdAsync(int guid) => await _context.ShopOrder.FirstOrDefaultAsync(x => x.Id == guid);

        public async Task <IEnumerable<Order>> GetAllOrders()
        {
            return await _context.ShopOrder.ToListAsync();
        }

        public async Task<List<Order>> GetOrderByApplicationUserIdAsync(int applicationUserId)
        {
            return await _context.ShopOrder.Where(x => x.ApplicationUserId == applicationUserId).ToListAsync();
        }

        public List<Order> GetOrderItems()
        {
            return _context.ShopOrder.Where(o => o.OrderItems.Any(oi => oi.OrderId == o.Id)).ToList();
        }

        public int UpdateStatus(int orderID, OrderStatus orderStatus)
        {
            var order = new Order { Id = orderID, Status = orderStatus};
            _context.ShopOrder.Attach(order);
            _context.Entry(order).Property(o => o.Status).IsModified = true;
             return _context.SaveChanges();
                
        }

        public async Task<int> removeById(int orderId)
        {
            Order order1 = _context.ShopOrder.FirstOrDefault(x => x.Id == orderId);
            _context.ShopOrder.Remove(order1);
            return await _context.SaveChangesAsync();
        }

    }
}
