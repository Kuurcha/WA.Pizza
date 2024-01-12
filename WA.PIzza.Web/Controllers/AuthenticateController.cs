using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Core.Model.AuthenticateController;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.DTO.Auth;

namespace WA.PIzza.Web.Controllers
{
    /// <summary>
    /// Controller for managing registering, managing refresh and access tokens
    /// </summary>
    public class AuthenticateController : ControllerBase
    {

        private readonly ILogger<BasketController> _log;
        private readonly AuthenticationService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// Authenticate Controller DI injection constructor
        /// </summary>
        /// <param name="log"></param>
        /// <param name="authenticationService"></param>
        /// <param name="userManager"></param>
        public AuthenticateController(UserManager<ApplicationUser> userManager, ILogger<BasketController> log, AuthenticationService authenticationService)
        {
            _log = log;
            _authenticationService = authenticationService;
            _userManager = userManager;

        }

        /// <summary>
        /// Registers user with specified data with "user" role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            try
            {
                await _authenticationService.Register(model);
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        }
        /// <summary>
        ///  Registers user with specified data with "admin" role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register - admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest model)
        {
            try
            {
                await _authenticationService.RegisterAdmin(model);
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        }
        /// <summary>
        /// Returns access and refresh token if user data is correct
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            TokenResponse tokenResponse;
            try
            {
                tokenResponse = await _authenticationService.LoginUser(model);
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(tokenResponse.accessToken),
                expiration = tokenResponse.accessToken.ValidTo,
                refreshToken = tokenResponse.refreshToken
            });

        }
        /// <summary>
        /// Refreshes access token based on refresh token
        /// </summary>
        /// <param name="request"></param>
        /// <returns>New access token for the same user</returns>
        [HttpPost]
        [Route("refresh")]

        public async Task<IActionResult> RefreshAsync(TokenRequestDTO request)
        {

            TokenResponse tokenResponse;
            try
            {
                tokenResponse = await _authenticationService.RefreshAccessToken(request);
            }
            catch (EntityNotFoundException ex)
            {
                _log.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(tokenResponse.accessToken),
                refreshToken = tokenResponse.refreshToken
            });

        }
        /// <summary>
        /// Resets user's current refresh token
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> RevokeAsync()
        {
            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest();
            user.refreshToken = null;
            return NoContent();
        }

    }
}