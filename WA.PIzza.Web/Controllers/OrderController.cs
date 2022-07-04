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
        readonly ILogger<OrderController> _log;
        public OrderController(OrderDataService orderDataService, ILogger<OrderController> log)
        {
            _orderDataService = orderDataService;
            _log = log;
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
            _log.LogInformation("Get by id request: " + id);
            try
            {
                order = await _orderDataService.GetById(id);
                _log.LogInformation("Item recieved: " + order.ToString());
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
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
            _log.LogInformation("Create order request with id:  " + basketId + " and description: " + description + " ...");
            try
            {
                await _orderDataService.AddOrder(basketId, description);
                _log.LogInformation("Order added");
            }
            catch( EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                BadRequest(ex);
            }
            return Accepted();
        }
        [HttpPut]
        public async Task<ActionResult<Order>> UpdateOrderStatus(int orderId, OrderStatus orderStatus)
        {
            _log.LogInformation("Updating order with id: " + orderId + " to status:" + orderStatus.ToString() + "...");  
            try
            {

                await _orderDataService.UpdateStatus(orderId, orderStatus);
                _log.LogInformation("Order updated");
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                BadRequest(ex);
            }
            return Accepted();
        }

    }
}
