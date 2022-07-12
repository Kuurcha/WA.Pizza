using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Core.Model.AuthenticateController;
using Mapster;
using System.Net;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Basket;
using Wa.Pizza.Infrasctructure.DTO.CatalogItem;
using Wa.Pizza.Infrasctructure.Services;
namespace WA.PIzza.Web.Controllers
{


    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

/*        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status =  "Error", Message = "User already exists!"});
            }
            ApplicationUser user = new ApplicationUser() { };

        }*/
    }
}
