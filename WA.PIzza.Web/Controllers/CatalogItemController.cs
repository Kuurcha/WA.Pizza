using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Data.Services;
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
        public async Task<ActionResult<IEnumerable<CatalogItem>>> Get()
        {
            return new ObjectResult(await _catalogItemDataService.getCatalogAsync());
        }
        /// <summary>
        /// Allows to get specific catalogue item
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogItem>> Get(int id)
        {
            CatalogItem catalogItem = await _catalogItemDataService.GetByIdAsync(id);
            if (catalogItem == null)
                return NotFound();
            return new ObjectResult(catalogItem);
        }

    



    }
}
