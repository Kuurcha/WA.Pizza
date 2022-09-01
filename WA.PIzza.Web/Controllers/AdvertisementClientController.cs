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
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementClientController : ControllerBase
    {
        private readonly ILogger<BasketController> _log;
        private readonly AdvertisementClientService _advertisementClientService;

        public AdvertisementClientController(AdvertisementClientService advertisementClientService, ILogger<BasketController> log)
        {
            _advertisementClientService = advertisementClientService;
            _log = log;
        }

        /// <summary>
        /// Returns advertisement client by api key
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpGet("byApiKey/{apiKey}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<ActionResult<AdvertisementClientDTO>> GetByApiKey(string apiKey)
        {
            AdvertisementClientDTO advertisementClientDTO;
            try
            {
                advertisementClientDTO = await _advertisementClientService.GetAdvertisementClient(apiKey);
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new ObjectResult(advertisementClientDTO);
        }


        /// <summary>
        /// Returns advertisement client by api key
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpGet("byId/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<ActionResult<AdvertisementClientDTO>> GetById(int id, string apiKey)
        {
            AdvertisementClientDTO advertisementClientDTO;
            try
            {
                advertisementClientDTO = await _advertisementClientService.GetAdvertisementClientById(id, apiKey);
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new ObjectResult(advertisementClientDTO);
        }

        /// <summary>
        /// Adds new advertisement client and generates ApiKey
        /// </summary>
        /// <param name="advertisementClientDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.RegularUser)]
        public async Task<ActionResult> AddAdvertisementClient(CreateAdvertisementClientDTO advertisementClientDTO)
        {
            string apiKey;
            try
            {
                apiKey = await _advertisementClientService.CreateAdvertisementClientAsync(advertisementClientDTO);
            }
            catch (WrongDataFormatException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return Accepted(apiKey);

        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<ActionResult> RemoveAvertisementClient(int id, UpdateDeleteAdvertisementClientDTO advertisementClientDTO)
        {
            try
            {
                await _advertisementClientService.DeleteAdvertisementClient(id, advertisementClientDTO);
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

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.RegularUser)]
        public async Task<ActionResult> UpdateAvertisementClient(int id, UpdateDeleteAdvertisementClientDTO advertisementClientDTO)
        {
            try
            {
                await _advertisementClientService.UpdateAdvertisementClient(id, advertisementClientDTO);
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
