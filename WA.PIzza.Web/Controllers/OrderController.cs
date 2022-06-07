using Mapster;
using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Order;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.Services
{
    [Route("api/[controller]")]
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
        /// Returns specific order by specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDTO>> GetById(int id)
        {
            GetOrderDTO order;
            try
            {
                order = await _orderDataService.GetById(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex);
            }
            return new ObjectResult(order);
        }
        /// <summary>
        /// Posts order 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(int basketId, string description)
        {
            try
            {
                await _orderDataService.AddOrder(basketId, description);
            }
            catch( EntityNotFoundException ex)
            {
                BadRequest(ex);
            }
            return Accepted();
        }
        [HttpPut]
        public async Task<ActionResult<Order>> UpdateOrderStatus(int orderId, OrderStatus orderStatus)
        {
            try
            {
                await _orderDataService.UpdateStatus(orderId, orderStatus);
            }
            catch (EntityNotFoundException ex)
            {
                BadRequest(ex);
            }
            return Accepted();
        }

    }
}
