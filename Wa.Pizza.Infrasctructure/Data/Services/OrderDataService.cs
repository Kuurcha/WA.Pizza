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

        public async Task<GetOrderDTO> GetByIdAsync(int guid)
        {
           Order order =  await _context.ShopOrder.FirstOrDefaultAsync(x => x.Id == guid);
           return await order
                            .BuildAdapter()
                            .AdaptToTypeAsync<GetOrderDTO>();
        } 

        public async Task <IEnumerable<GetOrderDTO>> GetAllOrders()
        {
            IEnumerable<Order> orders = await _context.ShopOrder.ToListAsync();
            IEnumerable<GetOrderDTO> result = await orders
                                                .BuildAdapter()
                                                .AdaptToTypeAsync<List<GetOrderDTO>>();
            return result;
        }

        public async Task<List<GetOrderDTO>> GetOrderByApplicationUserIdAsync(int applicationUserId)
        {
            IEnumerable<Order> orders = await _context.ShopOrder.Where(x => x.ApplicationUserId == applicationUserId).ToListAsync();
            return await orders
                            .BuildAdapter()
                            .AdaptToTypeAsync<List<GetOrderDTO>>();
        }

        public async Task<List<GetOrderDTO>> GetOrderItemsAsync()
        {
            IEnumerable<Order> orders = await _context.ShopOrder.Where(o => o.OrderItems.Any(oi => oi.OrderId == o.Id)).ToListAsync();
            return await orders
                            .BuildAdapter()
                            .AdaptToTypeAsync<List<GetOrderDTO>>();
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
