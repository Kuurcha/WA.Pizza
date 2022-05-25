using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogItemController: ControllerBase
    {
        private readonly CatalogDataService _catalogItemDataService;

        public CatalogItemController(CatalogDataService catalogItemDataService)
        {
            _catalogItemDataService = catalogItemDataService;
        }

        /// <summary>
        /// Allows to get all catalogue items 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogItemDTO>>> Get()
        {
            IEnumerable <CatalogItemDTO> catalogItems = await  _catalogItemDataService.getCatalogAsync();
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
            try
            {
                catalogItem = await _catalogItemDataService.GetById(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex);

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
            CatalogItemDTO catalogItem;
            try
            {
                await _catalogItemDataService.AddItem(catalogItemDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex);

            }
            return Accepted();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCatalogItem(CatalogItemDTO catalogItemDTO)
        {
            CatalogItemDTO catalogItem;
            try
            {
                await _catalogItemDataService.UpdateItem(catalogItemDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex);

            }
            return Accepted();
        }





    }
}
