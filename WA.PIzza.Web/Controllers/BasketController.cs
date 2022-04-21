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

            Basket basket = await _basketDataService.GetByIdAsync(userId, _basketDataService.Get_context());
            if (basket == null)
                return NotFound();
            BasketDTO basketDTO = await basket.BuildAdapter()
                           .AdaptToTypeAsync<BasketDTO>();
            return new ObjectResult(basket);
        }
        /// <summary>
        /// Creates (Posts) new basket
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Basket>> Post(BasketDTO basketDTO, int userId)
        {

            if (basketDTO == null)
                return BadRequest();
            Basket basket = null;
/*            using (HttpClient client = new HttpClient())
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create();
                var httpRequest = new HttpRequestMessage("GET", "https://localhost/api/");
                return await client.SendAsync(httpRequest);
            }*/
            await _basketDataService.AddBasket(basket);
            return new ObjectResult(basket);
        }
    }
}
