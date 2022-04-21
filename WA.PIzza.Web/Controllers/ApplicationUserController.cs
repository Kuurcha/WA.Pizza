using Mapster;
using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.ApplicationUser;


namespace WA.PIzza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController: ControllerBase
    { 
        private readonly ApplicationUserDataService _applicationUserDataService;
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDTO>> Get(int id)
        {
            ApplicationUser applicationUser = await _applicationUserDataService.GetByIdAsync(id, _applicationUserDataService.Get_context());
            if (applicationUser == null)
                return NotFound();
            return new ObjectResult(await applicationUser.BuildAdapter()
                            .AdaptToTypeAsync<ApplicationUserDTO>());
        } 
    }
}
