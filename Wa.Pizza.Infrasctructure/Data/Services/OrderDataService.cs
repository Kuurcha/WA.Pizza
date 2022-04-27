using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.DTO.Order;
using Wa.Pizza.Infrasctructure.Services.Interfaces;

namespace Wa.Pizza.Infrasctructure.Services
{
    public class OrderDataService : IEntityService<GetOrderDTO>
    {
        private readonly ApplicationDbContext _context;
        
        
        public OrderDataService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<int> AddOrder(SetOrderDTO orderDto, int applicationUserId)
        {
            Order order = await orderDto
                            .BuildAdapter()
                            .AdaptToTypeAsync<Order>();
            order.ApplicationUserId = applicationUserId;
            _context.ShopOrder.Add(order);
            return await _context.SaveChangesAsync();
        }

        public Task<GetOrderDTO> GetById(int guid) => _context.ShopOrder.Where(x => x.Id == guid).ProjectToType<GetOrderDTO>().FirstAsync();

        public Task<List<GetOrderDTO>> GetAllOrders() => _context.ShopOrder.ProjectToType<GetOrderDTO>().ToListAsync();

        public Task<List<GetOrderDTO>> GetOrderByApplicationUserId(int applicationUserId) => _context.ShopOrder.Where(x => x.ApplicationUserId == applicationUserId).ProjectToType<GetOrderDTO>().ToListAsync();

        public Task<List<GetOrderDTO>> GetOrderItems() => _context.ShopOrder.Where(o => o.OrderItems.Any(oi => oi.OrderId == o.Id)).ProjectToType<GetOrderDTO>().ToListAsync();


        public int UpdateStatus(int orderID, OrderStatus orderStatus)
        {
            var order = new Order { Id = orderID, Status = orderStatus};
            _context.ShopOrder.Attach(order);
            _context.Entry(order).Property(o => o.Status).IsModified = true;
             return _context.SaveChanges();
                
        }

        public async Task<int> removeById(int orderId)
        {
            _context.ShopOrder.Remove(await _context.ShopOrder.FirstOrDefaultAsync(x => x.Id == orderId));
            return await _context.SaveChangesAsync();
        }

    }
}
