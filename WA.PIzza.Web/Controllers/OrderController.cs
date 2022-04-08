using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {   
            _orderService = orderService;
            //Эндпоинты, http, GET
            //ctor
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return new ObjectResult(await _orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            Order order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            if (order == null)
                return BadRequest();       
            
            await _orderService.AddOrder(order);
            return Accepted(order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(int id)
        {
            Order order = await _orderService.GetByIdAsync(id);
            await _orderService.removeById(id);
            
            return Ok(order);
        }
    



    }
}
