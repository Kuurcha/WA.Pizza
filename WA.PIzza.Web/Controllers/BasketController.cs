using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.Services;
namespace WA.PIzza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketDataService _basketDataService;
        private readonly string _applicationUserUri = "https://localhost/api/applicationUser";
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
        public async Task<ActionResult<BasketDTO>> Get(int userId)
        {

            BasketDTO basket = await _basketDataService.GetByIdAsync(userId);
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
        public async Task<ActionResult> Post(BasketDTO basketDTO, int userId)
        {

            if (basketDTO == null)
                return BadRequest();
            await _basketDataService.AddBasket(basketDTO, userId);  
            return new ObjectResult(basketDTO);
        }
    }
}
