using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Core.Model.ApplicationUser;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;


namespace WA.PIzza.Web.Controllers
{
    /// <summary>
    /// Controller for adding advertisements for specfic distributor
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController: ControllerBase
    {
        private readonly ILogger<BasketController> _log;
        private readonly AdvertisementService _advertisementService;

        public AdvertisementController(AdvertisementService advertisementService, ILogger<BasketController> log)
        {
            _advertisementService = advertisementService;
            _log = log;
        }
        /// <summary>
        /// Returns advertisement for any user of the website. Intended for showing ads. Does not include ad distributor info.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AdvertisementDTO>> GetById(int id)
        {
            CUDAdvertisementDTO advertisementDTO;
            try
            {
                advertisementDTO = await _advertisementService.GetAdvertisementAnonymous(id);
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new ObjectResult(advertisementDTO);
        }

        /// <summary>
        /// Returns full advertisement info including ad distributor info, requires appropriate api key and authorisation. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpGet("authorised/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.RegularUser)]
        public async Task<ActionResult<AdvertisementDTO>> GetByIdAuthorised(int id, string apiKey)
        {
            AdvertisementDTO advertisementDTO = new AdvertisementDTO(); //???
            try
            {
                advertisementDTO = await _advertisementService.GetAdvertisement(apiKey, id);
            }
            catch (Exception ex) when (ex is WrongDataFormatException || ex is EntityNotFoundException)
            {
                _log.LogError(ex.Message);
                if (ex is EntityNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                if (ex is WrongDataFormatException)
                {
                    return BadRequest(ex.Message);
                }

            }
            return new ObjectResult(advertisementDTO);
        }

        /// <summary>
        /// Adds advertisement as either admin or user, requires appropriate api key and authorisation. 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="advertisementDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.RegularUser)]
        public async Task<ActionResult> AddAdvertisementClient(string apiKey, CUDAdvertisementDTO advertisementDTO)
        {
            try
            {
                await _advertisementService.CreateAdvertisement(advertisementDTO, apiKey);
            }
            catch (Exception ex) when (ex is WrongDataFormatException || ex is EntityNotFoundException)
            {
                _log.LogError(ex.Message);
                if (ex is EntityNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                if (ex is WrongDataFormatException)
                {
                    return BadRequest(ex.Message);
                }

            }
            return Accepted(apiKey);

        }

        /// <summary>
        /// Removes advertisement  either admin or user, requires appropriate api key and authorisation. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.RegularUser)]
        public async Task<ActionResult> RemoveAvertisement(int id, string apiKey)
        {
            try
            {
                await _advertisementService.RemoveAdvertisement(id, apiKey);
            }
            catch (Exception ex) when (ex is WrongDataFormatException || ex is EntityNotFoundException)
            {
                _log.LogError(ex.Message);
                if (ex is EntityNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                if (ex is WrongDataFormatException)
                {
                    return BadRequest(ex.Message);
                }

            }
            return Ok();
        }

        /// <summary>
        /// Updates advertisement  either admin or user, requires appropriate api key and authorisation. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.RegularUser)]
        public async Task<ActionResult> UpdateAvertisement(int id, CUDAdvertisementDTO advertisementClientDTO, string apiKey)
        {
            try
            {
                await _advertisementService.UpdateAdvertisement(advertisementClientDTO, id, apiKey);
            }
            catch (Exception ex) when (ex is WrongDataFormatException || ex is EntityNotFoundException)
            {
                _log.LogError(ex.Message);
                if (ex is EntityNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                if (ex is WrongDataFormatException)
                {
                    return BadRequest(ex.Message);
                }

            }
            return Ok();
        }


    }
}