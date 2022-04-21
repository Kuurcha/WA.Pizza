using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Data.Services;

namespace WA.PIzza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly BasketItemDataService _basketItemDataService;

        public BasketItemController(BasketItemDataService basketItemDataService)
        {
            _basketItemDataService = basketItemDataService;
        }
        /// <summary>
        /// Returns items of a specific basket based on basket id
        /// </summary>
        /// <param name="basketId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketItem>>> GetListByBasketId(int basketId)
        {
            return await _basketItemDataService.GetListByBasketId(basketId);
        }
        /// <summary>
        /// Return specific basket item value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketItem>> GetById(int id)
        {
            //Убрать 
            BasketItem basketItem = await _basketItemDataService.GetByIdAsync(id, _basketItemDataService.Get_context());
            if (basketItem == null)
                return NotFound();
            return new ObjectResult(basketItem);
        }
        /// <summary>
        /// Posts basketItem to database
        /// </summary>
        /// <param name="basketItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BasketItem>> Post(BasketItem basketItem)
        {
            if (basketItem == null)
                return BadRequest();

            await _basketItemDataService.AddBasketItem(basketItem);

            return Accepted(basketItem);
        }
        /// <summary>
        /// Deletes basketItem based on basketItemId
        /// </summary>
        /// <param name="basketItemId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<BasketItem>> Delete(int basketItemId)
        {
            BasketItem basketItem = await _basketItemDataService.GetByIdAsync(basketItemId, _basketItemDataService.Get_context());
            if (basketItem == null)
                return BadRequest();

            await _basketItemDataService.DeleteAsync(basketItem);

            //У Delete не accepted
            return NoContent();
        }

        /// <summary>
        /// Updates basket item
        /// </summary>
        /// <param name="basketItem"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<BasketItem>> Put(BasketItem basketItem)
        {
            if (basketItem == null)
                return BadRequest();

            await _basketItemDataService.UpdateBasketItem(basketItem);

            return Accepted(basketItem);
        }

    }
}
