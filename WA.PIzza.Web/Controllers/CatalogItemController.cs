using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class CatalogItemController: ControllerBase
    {
        private readonly CatalogItemDataService _catalogItemDataService;

        public CatalogItemController(CatalogItemDataService catalogItemDataService)
        {
            _catalogItemDataService = catalogItemDataService;
        }

        /// <summary>
        /// Allows to get all catalogue items 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCatalogItemDTO>>> Get()
        {
            IEnumerable <CatalogItem> catalogItems = await  _catalogItemDataService.getCatalogAsync();
            return new ObjectResult(await catalogItems.BuildAdapter()
                            .AdaptToTypeAsync <IEnumerable<GetCatalogItemDTO>>());
        }
        /// <summary>
        /// Allows to get specific catalogue item
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCatalogItemDTO>> Get(int id)
        {
            CatalogItem catalogItem = await _catalogItemDataService.GetByIdAsync(id, _catalogItemDataService.Get_context());
            if (catalogItem == null)
                return NotFound();

            return new ObjectResult(await catalogItem.BuildAdapter()
                            .AdaptToTypeAsync<GetCatalogItemDTO>());
        }

    



    }
}
