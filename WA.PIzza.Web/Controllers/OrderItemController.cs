using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class OrderItemController: ControllerBase
    {
        private readonly OrderItemService _orderItemService;

        public OrderItemController(OrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
            //Эндпоинты, http, GET
            //ctor
        }
        /// <summary>
        /// Returns order items for specific user 
        /// </summary>
        /// <returns></returns>
        [HttpGet("byOrder")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetByOrderId(int orderId)
        {
            return await _orderItemService.GetOrderItemsByOrderId(orderId);
        }
        /// <summary>
        /// Returns order items for specific user 
        /// </summary>
        /// <returns></returns>
        [HttpGet("byUser")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetByUserId(int userId)
        {
            return await _orderItemService.GetOrderItemsByUserId(userId);
        }
        /// <summary>
        /// Returns specific order item by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> Get(int id)
        {
            OrderItem orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound();
            return new ObjectResult(orderItem);
        }

        /// <summary>
        /// Posts order item
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<OrderItem>> Post(OrderItem orderItem)
        {
            if (orderItem == null)
                return BadRequest();       
            
            await _orderItemService.AddOrderItem(orderItem);
            return Accepted(orderItem);
        }

    }
}
