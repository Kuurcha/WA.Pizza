using Mapster;
using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Adress;



namespace WA.PIzza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController: ControllerBase
    {
        private readonly AdressDataService _adressDataService;

        public AdressController(AdressDataService adressDataService)
        {
            _adressDataService = _adressDataService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AdressDTO>> Get(int id)
        {
            Adress adress = await _adressDataService.GetByIdAsync(id, _adressDataService.Get_context());
            if (adress == null)
                return NotFound();
            return new ObjectResult(await adress
                .BuildAdapter()
                .AdaptToTypeAsync<AdressDTO>());
        } 
    }
}
