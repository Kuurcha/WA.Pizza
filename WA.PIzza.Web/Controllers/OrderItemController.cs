using Mapster;
using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.DTO.Order;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class OrderItemController: ControllerBase
    {
        private readonly OrderItemDataService _orderItemService;

        
        public OrderItemController(OrderItemDataService orderItemService)
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
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetByOrderId(int orderId)
        {
            IEnumerable<OrderItemDTO> orderItems = await _orderItemService.GetOrderItemsByOrderId(orderId);
            return new ObjectResult(orderItems);
        }
        /// <summary>
        /// Returns order items for specific user 
        /// </summary>
        /// <returns></returns>
        [HttpGet("byUser")]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetByUserId(int userId)
        {
            IEnumerable<OrderItemDTO> orderItems = await _orderItemService.GetOrderItemsByUserId(userId); 
            return new ObjectResult(orderItems);
        }
        /// <summary>
        /// Returns specific order item by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDTO>> Get(int id)
        {
            OrderItemDTO orderItemDTO = await _orderItemService.GetByIdAsync(id);
            if (orderItemDTO == null)
                return NotFound();
            return new ObjectResult(orderItemDTO);
        }

        /// <summary>
        /// Posts order item
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(OrderItemDTO orderItemDTO, int orderId)
        {
            if (orderItemDTO == null)
                return BadRequest();      
            await _orderItemService.AddOrderItem(orderItemDTO, orderId);
            return Accepted();
        }

    }
}
