using Mapster;
using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.Services
{
    [Route ("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDataService _orderDataService;

        public OrderController(OrderDataService orderDataService)
        {
            _orderDataService = orderDataService;
            //Эндпоинты, http, GET
            //ctor
        }
        /// <summary>
        /// Returns lists of order of specific user by specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int userId)
        {
            IEnumerable<Order> orders = await _orderDataService.GetOrderByApplicationUserIdAsync(userId);
            if (orders == null)
                return NotFound();
            return new ObjectResult(orders);
        }
        /// <summary>
        /// Returns specific order by specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetById(int id)
        {
            Order order = await _orderDataService.GetByIdAsync(id, _orderDataService.Get_context());
         
            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }
        /// <summary>
        /// Posts order 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
/*        [HttpPost]
        public async Task<ActionResult<Order>> Post(OrderDTO order)
        {

            if (order == null)
                return BadRequest();
            await _orderDataService.AddOrder(order);
            return Accepted(order);
        }*/


    }
}
