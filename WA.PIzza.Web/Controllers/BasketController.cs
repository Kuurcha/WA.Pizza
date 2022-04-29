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
        [HttpGet("byUser")]
        public async Task<ActionResult<List<BasketDTO>>> GetBasketItemsByUserIdResponse(int userId)
        {
            BasketDTO basket = await _basketDataService.GetByApplicationUserId(userId);
            List<BasketItemDTO> basketItems = await _basketDataService.GetBasketItemListByBasketId(basket.Id);
            if (basket == null || basketItems == null)
                return NotFound();
            return new ObjectResult(basketItems);
        }

        /// <summary>
        /// Gets specific baket by specific basketId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("byBasket")]
        public async Task<ActionResult<List<BasketDTO>>> GetBasketItemsByBasketIdResponse(int basketId)
        {
            List<BasketItemDTO> basketItems = await _basketDataService.GetBasketItemListByBasketId(basketId);
            if (basketItems == null)
                return NotFound();
            return new ObjectResult(basketItems);
        }

        /// <summary>
        /// Creates empty baskets without bindings to any user or items
        /// </summary>
        /// <returns></returns>
        [HttpPost] 
        public async Task<ActionResult> AddBasket(int basketId)
        {
            await _basketDataService.AddBasket( new BasketDTO { LastModified = DateTime.Now });
            return Ok();
        }

        /// <summary>
        /// Checks if Basket exists
        /// </summary>
        /// <param name="basketId"></param>
        /// <returns></returns>
        private async Task<bool> IsBasketByIdExists(int basketId) => (await _basketDataService.GetById(basketId) != null);

        /// <summary>
        /// Adds basket item based on picked catalogItem
        /// </summary>
        /// <param name="catalogItemId">chosen catalogItem</param>
        /// <param name="basketId">id of Basket to add to</param>
        /// <param name="quantity">Amount of chosen catalogItem</param>
        /// <returns></returns>
        [HttpPost("basketItem")]
        public async Task<ActionResult> AddBasketItemRequest(int catalogItemId, int basketId, int quantity)
        {
            CatalogItemDTO catalogItemDTO = await _catalogDataService.GetById(catalogItemId);
            if (catalogItemDTO == null || quantity <= 0)
                return BadRequest();

            if (!(await IsBasketByIdExists(basketId)))
                return BadRequest();
            

            BasketItemDTO basketItemDTO = catalogItemDTO.ToBasketItemDto(quantity, basketId);

            await _basketDataService.AddItem(basketItemDTO);

            await _basketDataService.UpdateDateBasket(basketId);



            return Accepted(); 
        }

        
        /// <summary>
        /// Removes the basket item by it's id 
        /// </summary>
        /// <param name="basketItemId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItemRequest(int basketItemId)
        {
            BasketItemDTO basketItem = await _basketDataService.GetBasketItemById(basketItemId);
            if (basketItem == null)
                return BadRequest();
            await _basketDataService.DeleteItem(basketItem);

            int basketId = basketItem.BasketId;

            if (await IsBasketByIdExists(basketId))
                await _basketDataService.UpdateDateBasket(basketId);

            return Ok();
        }
        /// <summary>
        /// Changes quantity of an item already existing item
        /// </summary>
        /// <param name="basketItemId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> ChangeBasketItemQuantityRequest(int basketItemId, int quantity)
        {
            BasketItemDTO basketItem = await _basketDataService.GetBasketItemById(basketItemId);
            if (basketItem == null)
                return BadRequest();
            basketItem.Quantity = quantity;

            int basketId = basketItem.BasketId;

            if (await IsBasketByIdExists(basketId))
                await _basketDataService.UpdateDateBasket(basketId);

            return Accepted();
        }
        /// <summary>
        /// Clear specified basket by it's id
        /// </summary>
        /// <param name="basketId"></param>
        /// <returns></returns>
        [HttpDelete("clear")]
        public async Task<ActionResult> ClearBasketRequest(int basketId)
        {

            if (await IsBasketByIdExists(basketId))
                await _basketDataService.UpdateDateBasket(basketId);

            await _basketDataService.ClearBasket(basketId);
            return Ok();
        }

        /// <summary>
        /// Binds existing Basket to user
        /// </summary>
        /// <param name="basketId"></param>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        [HttpPut("updateUser")]
        public async Task<ActionResult> BindUserToBasketRequest(int basketId, int applicationUserId)
        {
            if (!(await IsBasketByIdExists(basketId)))
                return BadRequest();
           
            await _basketDataService.BindBuyetToBasket(basketId, applicationUserId);
            return Accepted();
        }
    }
}
