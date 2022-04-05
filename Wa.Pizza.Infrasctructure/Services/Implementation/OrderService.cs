using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Services
{
    public class OrderService : IEntityService<Order>
    {
        private readonly ApplicationDbContext _context;
    
        public OrderService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public void AddOrder(Order order)
        {
            _context.ShopOrder.Add(order);
            _context.SaveChanges();
        }

        public Order GetByGUID(int guid)
        {
            return _context.ShopOrder.First(x => x.Id == guid);
        }

        public async Task<ActionResult<IEnumerable<Order>>> GetDbSet()
        {
            return await _context.ShopOrder.ToListAsync();
        }

        public Order getOrderByApplicationUser(ApplicationUser applicationUser)
        {
            return _context.ShopOrder.First(x => x.ApplicationUser == applicationUser);
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

    }
}
