using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Core.Model.ApplicationUser;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
using Wa.Pizza.Infrasctructure.Services;
using static BasketQueries;
using static Wa.Pizza.Infrasctructure.Data.CQRS.Basket.BasketCommands;

namespace WA.PIzza.Web.Controllers
{
    /// <summary>
    /// Controller for managing baskets
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        readonly ILogger<BasketController> _log;
        /// <summary>
        /// BasketController DI Injection constructor
        /// </summary>
        /// <param name="catalogDataService"></param>
        /// <param name="log"></param>
        /// <param name="mediator"></param>
        public BasketController(CatalogDataService catalogDataService, ILogger<BasketController> log, IMediator mediator)
        {
            _mediator = mediator;
            _log = log;
        }

        /// <summary>
        /// Gets specific baket by specific user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("byUserId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<ActionResult<List<BasketDTO>>> GetBasketByUserId(string userId)
        {
            BasketDTO basket;
            _log.LogInformation("Retriving basket by user id " + userId + "..");

            try
            {
                basket = await _mediator.Send(new GetBasketByUserIdQuery(userId));
                _log.LogInformation("Item retrieved: " + basket.ToString());
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new ObjectResult(basket);
        }
        /// <summary>
        /// Gets specific basket by specific basket id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("byBasketId")]
        [AllowAnonymous]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<ActionResult<List<BasketDTO>>> GetBasketById(int id)
        {
            _log.LogInformation("Retriving basket by  id " + id + "..");
            BasketDTO basket;
            try
            {
                basket = await _mediator.Send(new GetBasketByIdQuery(id));
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
        /// <param name="basketItemDTO">chosen catalogItem</param>
        /// <returns></returns>
        [HttpPost("basketItem")]
        public async Task<ActionResult> AddBasketItemRequest(BasketItemDTO basketItemDTO)
        {
            _log.LogInformation("Adding Basket Item " + basketItemDTO.ToString() + "..");
            try
            {
               await _mediator.Send(new InsertItemCommand(basketItemDTO));
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
        /// <param name="basketItemDTO"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItemRequest(BasketItemDTO basketItemDTO)
        {
            _log.LogInformation("Removing Basket Item " + basketItemDTO.ToString() + "..");
            try
            {
                await _mediator.Send(new DeleteItemCommand(basketItemDTO));
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
        /// <param name="basketItemDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> ChangeBasketItemQuantityRequest(BasketItemDTO basketItemDTO)
        {
            _log.LogInformation("Changing basket item qunatity... " + basketItemDTO + "..");
            try 
            {
                await _mediator.Send(new UpdateItemCommand(basketItemDTO)); 
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
        /// <param name="basketDTO"></param>
        /// <returns></returns>
        [HttpDelete("clear")]
        public async Task<ActionResult> ClearBasketRequest(BasketDTO basketDTO)
        {
            _log.LogInformation("Clearing basket: " + basketDTO.ToString() + "..." );
            try
            {
                await _mediator.Send(new ClearBasketCommand(basketDTO));
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
                await _mediator.Send(new BindBuyerToBasketCommand(basketDTO, applicationUserId));
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
