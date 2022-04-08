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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> Get()
        {
            return await _orderItemService.GetAllOrderItems();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> Get(int id)
        {
            OrderItem orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound();
            return new ObjectResult(orderItem);
        }


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
