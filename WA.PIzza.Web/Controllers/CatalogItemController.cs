using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;

namespace WA.PIzza.Web.Controllers
{
    /// <summary>
    /// Controler for managing Catalog Items
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogItemController : ControllerBase
    {
        private readonly CatalogDataService _catalogItemDataService;
        readonly ILogger<CatalogItemController> _log;
        /// <summary>
        /// CatalogItemController DI constructor
        /// </summary>
        /// <param name="catalogItemDataService"></param>
        /// <param name="log"></param>
        public CatalogItemController(CatalogDataService catalogItemDataService, ILogger<CatalogItemController> log)
        {
            _catalogItemDataService = catalogItemDataService;
            _log = log;
        }

        /// <summary>
        /// Allows to get all catalogue items 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogItemDTO>>> Get()
        {
            _log.LogInformation("Catalog items requested...");
            IEnumerable<CatalogItemDTO> catalogItems = await _catalogItemDataService.getCatalogAsync();
            _log.LogInformation("Catalog items sent: " + catalogItems.ToString());
            return new ObjectResult(catalogItems);
        }
        /// <summary>
        /// Allows to get specific catalogue item
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogItemDTO>> GetById(int id)
        {
            CatalogItemDTO catalogItem;
            _log.LogInformation("Catalog item by id requested: " + id + "...");
            try
            {
                catalogItem = await _catalogItemDataService.GetById(id);
                _log.LogInformation("Catalog item sent: " + catalogItem.ToString());
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return new ObjectResult(catalogItem);
        }
        /// <summary>
        /// Adds catalog item
        /// </summary>
        /// <param name="catalogItemDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddCatalogItem(CatalogItemDTO catalogItemDTO)
        {
            _log.LogInformation("Catalog item add request: " + catalogItemDTO.ToString());
            try
            {
                await _catalogItemDataService.AddItem(catalogItemDTO);
                _log.LogInformation("Catalog item added");
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            return Accepted();
        }
        /// <summary>
        /// Updates catalog item based on user data
        /// </summary>
        /// <param name="catalogItemDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateCatalogItem(CatalogItemDTO catalogItemDTO)
        {
            _log.LogInformation("Catalog item update request: " + catalogItemDTO.ToString());
            try
            {
                await _catalogItemDataService.UpdateItem(catalogItemDTO);
                _log.LogInformation("Catalog item updated");
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
            return Accepted();
        }





    }
}
