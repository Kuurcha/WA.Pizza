using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.Services
{
    [Route ("api/[Service]")]
    [ApiController]
    public class ControllerService : ControllerBase
    {
        private readonly CatalogItemDataService _catalogItemDataService;

        public ControllerService(CatalogItemDataService catalogItemDataService)
        {
            _catalogItemDataService = catalogItemDataService;
            //Эндпоинты, http, GET
            //ctor
        }

        /// <summary>
        /// Get catalog item list
        /// </summary>
        /// <returns>list of catalogItems</returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogItem>>> Get()
        {
            return new ObjectResult(await _catalogItemDataService.getCatalogAsync());
        }

      


    }
}
