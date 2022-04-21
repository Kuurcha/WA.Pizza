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

        public async Task<int> AddOrderItem(OrderItemDTO orderItemDTO, int orderId )
        {
            OrderItem orderItem = await orderItemDTO
                                        .BuildAdapter()
                                        .AdaptToTypeAsync<OrderItem>();
            orderItem.OrderId = orderId;
            _context.ShopOrderItem.Add(orderItem);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsByUserId(int userId)
        {
            IEnumerable<OrderItem> orderItems =  await _context.ShopOrderItem.Where(oi => oi.Order.ApplicationUserId == userId).ToListAsync();
            return await orderItems
                            .BuildAdapter()
                            .AdaptToTypeAsync<List<OrderItemDTO>>();
        }

        public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderId(int orderId)
        {
            IEnumerable<OrderItem> orderItems = await _context.ShopOrderItem.Where(o => o.OrderId == orderId).ToListAsync();
            return await orderItems
                            .BuildAdapter()
                            .AdaptToTypeAsync<List<OrderItemDTO>>();
        }


        public int UpdateOrderId(int orderItemId, int orderId)
        {
            var orderItem = new OrderItem { Id = orderItemId, OrderId = orderId };
            _context.ShopOrderItem.Attach(orderItem);
            _context.Entry(orderItem).Property(oi => oi.OrderId).IsModified = true;
            return _context.SaveChanges();

        }

        public async Task<OrderItemDTO> GetByIdAsync(int guid)
        {
            OrderItem orderItem = await _context.ShopOrderItem.FirstOrDefaultAsync(x => x.Id == guid);
            return await orderItem
                            .BuildAdapter()
                            .AdaptToTypeAsync<OrderItemDTO>();
        }
        public OrderItem BasketItemToOrderItem(BasketItem basketItem, int orderId, int discount )
        {
            return new OrderItem { CatalogItemId = basketItem.CatalogItemId, OrderId = orderId, Quantity = basketItem.Quantity, UnitPrice = basketItem.UnitPrice, Discount = discount, CatalogItemName = basketItem.CatalogItemName };
        }

        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetAllOrderItems()
        {
            IEnumerable<OrderItem> orderItems = await _context.ShopOrderItem.ToListAsync();
            return await orderItems
                            .BuildAdapter()
                            .AdaptToTypeAsync<List<OrderItemDTO>>();
        }

        public async Task<int> UpdateOrderItem(UpdateOrderItemDTO orderItemDTO)
        {
            OrderItem orderItem = await orderItemDTO
                            .BuildAdapter()
                            .AdaptToTypeAsync<OrderItem>();
            orderItem.CatalogItemName = (await _context.CatalogItem.FirstOrDefaultAsync(ci => ci.Id == orderItem.CatalogItemId)).Name;
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


    }
}
