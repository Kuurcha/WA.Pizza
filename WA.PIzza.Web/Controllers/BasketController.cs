using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
using Wa.Pizza.Infrasctructure.Services;
namespace WA.PIzza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketDataService _basketDataService;
        private readonly CatalogDataService _catalogDataService;
        private readonly string _applicationUserUri = "https://localhost/api/applicationUser";
        public BasketController(BasketDataService basketDataService, CatalogDataService catalogDataService)
        {
            _basketDataService = basketDataService;
            _catalogDataService = catalogDataService;
        }

        /// <summary>
        /// Gets specific baket by specific user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<BasketDTO>>> GetBasketItemsByUserIdResponse(int userId)
        {
            BasketDTO basket = await _basketDataService.GetById(userId);
            List<BasketItemDTO> basketItems = await _basketDataService.GetBasketItemListByBasketId(userId);
            if (basket == null)
                return NotFound();
            return new ObjectResult(basketItems);
        }

        [HttpPost]
        public async Task<ActionResult> AddBasketItemRequest(int catalogItemId, int basketId, int quantity)
        {
            CatalogItemDTO catalogItemDTO = await _catalogDataService.GetById(catalogItemId);
            if (catalogItemDTO == null || quantity <= 0)
                return BadRequest();

            if (_basketDataService.GetById(basketId) == null)
                return BadRequest();

            BasketItemDTO basketItemDTO = catalogItemDTO.ToBasketItemDto(quantity, basketId);

            await _basketDataService.AddItem(basketItemDTO);

            await _basketDataService.UpdateDateBasket(basketId);

            return Accepted(); 
        }

    }
}
