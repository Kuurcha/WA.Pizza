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
            Order order = new Order { };
            return await _orderService.GetDbSet();
        }

    }
}
