using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.Services;
namespace WA.PIzza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketDataService _basketDataService;

        public BasketController(BasketDataService basketDataService)
        {
            _basketDataService = basketDataService;
        }

        /// <summary>
        /// Gets specific baket by specific user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Basket>> Get(int userId)
        {
            Basket basket = await _basketDataService.GetByIdAsync(userId);
            if (basket == null)
                return NotFound();
            return new ObjectResult(basket);
        }
        /// <summary>
        /// Creates (Posts) new basket
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Basket>> Post(Basket basket)
        {
            if (basket == null)
                return BadRequest();
            await _basketDataService.AddBasket(basket);
            return new ObjectResult(basket);
        }
    }
}
