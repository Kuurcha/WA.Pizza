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
        [HttpGet("byUserId")]
        public async Task<ActionResult<List<BasketDTO>>> GetBasketUserId(int userId)
        {
            BasketDTO basket;
            try
            {
                basket = await _basketDataService.GetByUserId(userId);

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex);
            }
            finally { }
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
            BasketDTO basket;
            try
            {
                basket = await _basketDataService.GetByUserId(id);

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex);
            }
            finally { }
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
        public async Task<ActionResult> AddBasketItemRequest(BasketItemDTO basketItemDTO, int basketId)
        {
            try
            {
                await _basketDataService.AddItem(basketItemDTO, basketId);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex);
            }
            finally { }
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
            try
            {
                await _basketDataService.DeleteItem(basketItemDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex);
            }
            finally { }
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
            try 
            {
                await _basketDataService.DeleteItem(basketItemDTO);

            }
            catch(EntityNotFoundException ex)
            {
                return BadRequest(ex);
            }
            finally { }
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
            try
            {
                await _basketDataService.ClearBasket(basketDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex);
            }
            finally { }
            return Ok();
        }

        /// <summary>
        /// Binds existing Basket to user
        /// </summary>
        /// <param name="basketId"></param>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        [HttpPut("updateUser")]
        public async Task<ActionResult> BindUserToBasketRequest(BasketDTO basketDTO)
        {
            try
            {
                await _basketDataService.ClearBasket(basketDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex);
            }
            finally { }
            return Ok();
        }
    }
}
