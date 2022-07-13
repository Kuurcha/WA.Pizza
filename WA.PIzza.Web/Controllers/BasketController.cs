using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wa.Pizza.Core.Exceptions;
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
        readonly ILogger<BasketController> _log;
        public BasketController(BasketDataService basketDataService, CatalogDataService catalogDataService, ILogger<BasketController> log)
        {
            _basketDataService = basketDataService;
            _catalogDataService = catalogDataService;
            _log = log;
        }

        /// <summary>
        /// Gets specific baket by specific user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("byUserId")]
        public async Task<ActionResult<List<BasketDTO>>> GetBasketByUserId(int userId)
        {
            BasketDTO basket;
            _log.LogInformation("Retriving basket by user id " + userId + "..");
            try
            {
                basket = await _basketDataService.GetByUserId(userId);
                _log.LogInformation("Item retrieved: " + basket.ToString());
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex);
            }
            return new ObjectResult(basket);
        }
        /// <summary>
        /// Gets specific baket by specific user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("byBasketId")]
        public async Task<ActionResult<List<BasketDTO>>> GetBasketById(int id)
        {
            _log.LogInformation("Retriving basket by  id " + id + "..");
            BasketDTO basket;
            try
            {
                basket = await _basketDataService.GetByUserId(id);
                _log.LogInformation("Item retrieved: " + basket.ToString());

            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex);
            }
            return new ObjectResult(basket);
        }


        /// <summary>
        /// Adds basket item based on picked catalogItem
        /// </summary>
        /// <param name="catalogItemId">chosen catalogItem</param>
        /// <param name="basketId">id of Basket to add to</param>
        /// <param name="quantity">Amount of chosen catalogItem</param>
        /// <returns></returns>
        [HttpPost("basketItem")]
        public async Task<ActionResult> AddBasketItemRequest(BasketItemDTO basketItemDTO)
        {
            _log.LogInformation("Adding Basket Item " + basketItemDTO.ToString() + "..");
            try
            {

                await _basketDataService.AddItem(basketItemDTO);
                _log.LogInformation("Item added");
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex);
            }
            return Accepted();
        }

        
        /// <summary>
        /// Removes the basket item by it's id 
        /// </summary>
        /// <param name="basketItemId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItemRequest(BasketItemDTO basketItemDTO)
        {
            _log.LogInformation("Removing Basket Item " + basketItemDTO.ToString() + "..");
            try
            {
                await _basketDataService.DeleteItem(basketItemDTO);
                _log.LogInformation("Item removed");
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex);
            }
            return Ok();
        }
        /// <summary>
        /// Changes quantity of an item already existing item
        /// </summary>
        /// <param name="basketItemId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> ChangeBasketItemQuantityRequest(BasketItemDTO basketItemDTO)
        {
            _log.LogInformation("Changing basket item qunatity... " + basketItemDTO + "..");
            try 
            {
                await _basketDataService.UpdateItem(basketItemDTO);
                _log.LogInformation("Item updated");

            }
            catch(EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex);
            }
            return Ok();
        }
        /// <summary>
        /// Clear specified basket by it's id
        /// </summary>
        /// <param name="basketId"></param>
        /// <returns></returns>
        [HttpDelete("clear")]
        public async Task<ActionResult> ClearBasketRequest(BasketDTO basketDTO)
        {
            _log.LogInformation("Clearing basket: " + basketDTO.ToString() + "..." );
            try
            {
                await _basketDataService.ClearBasket(basketDTO);
                _log.LogInformation("Basket Cleared");
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex);
            }
            return Ok();
        }

        /// <summary>
        /// Binds existing Basket to user
        /// </summary>
        /// <param name="basketDTO"></param>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        [HttpPut("updateUser")]
        public async Task<ActionResult> BindUserToBasketRequest(BasketDTO basketDTO, string applicationUserId)
        {
            _log.LogInformation("Binding Buyer To Basket: " + basketDTO.ToString() + "to id: + " + applicationUserId + "...");
            try
            {
                await _basketDataService.BindBuyerToBasket(basketDTO, applicationUserId);
                _log.LogInformation("Basket binded");
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex);
            }
            finally { }
            return Ok();
        }
    }
}
