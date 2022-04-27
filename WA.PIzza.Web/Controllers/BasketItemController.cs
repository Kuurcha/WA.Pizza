using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Basket;

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
        public async Task<ActionResult<IEnumerable<BasketItemDTO>>> GetListByBasketId(int basketId)
        {
            return await _basketItemDataService.GetListByBasketId(basketId);
        }
        /// <summary>
        /// Return specific basket item value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketItemDTO>> GetById(int id)
        {
            //Убрать 
            BasketItemDTO basketItem = await _basketItemDataService.GetById(id);
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
        public async Task<ActionResult> Post(BasketItemDTO basketItemDTO, int basketId)
        {
            if (basketItemDTO == null)
                return BadRequest();

            await _basketItemDataService.AddBasketItem(basketItemDTO, basketId);

            return Accepted();
        }
        /// <summary>
        /// Deletes basketItem based on basketItemId
        /// </summary>
        /// <param name="basketItemId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(int basketItemId)
        {
            BasketItemDTO basketItem = await _basketItemDataService.GetById(basketItemId);
            if (basketItem == null)
                return BadRequest();

            await _basketItemDataService.Delete(basketItemId);

            return NoContent();
        }

        /// <summary>
        /// Updates basket item
        /// </summary>
        /// <param name="basketItem"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Put(BasketItemDTO basketItemDTO, int basketItemId)
        {
            if (basketItemDTO == null)
                return BadRequest();

            await _basketItemDataService.UpdateBasketItem(basketItemDTO, basketItemId);

            return Accepted();
        }

    }
}
