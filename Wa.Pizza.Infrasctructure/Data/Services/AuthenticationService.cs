using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Core.Model.ApplicationUser;
using Wa.Pizza.Core.Model.AuthenticateController;
using Wa.Pizza.Infrasctructure.DTO.Auth;

namespace Wa.Pizza.Infrasctructure.Data.Services
{

    public class AuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, TokenService tokenService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _tokenService = tokenService;

        }

        /// <summary>
        /// Checks if user with requested username exists
        /// </summary>  
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> checkIfUserExists(RegisterRequest model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            return userExists != null;
        }
        private async Task _сreateRolesIfDoesNotExists()
        {
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            if (!await roleManager.RoleExistsAsync(Roles.RegularUser))
                await roleManager.CreateAsync(new IdentityRole(Roles.RegularUser));
        }
        public async Task<IdentityResult> Register(RegisterRequest model)
        {

            if (await checkIfUserExists(model))
            {
                throw new EntityNotFoundException("User with username: " + model.Username + " already exists."); 
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(), //Random value to change each time credential information about user changes
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors;
                string errorString = "";
                foreach(var error in errors)
                {
                    errorString += error.Description + Environment.NewLine;
                }
                throw new Exception("Unable to regiter user. " + errorString);
            }
            await _сreateRolesIfDoesNotExists();
            await userManager.AddToRoleAsync(user, Roles.RegularUser);
            return result;

        }

        public async Task<IdentityResult> RegisterAdmin(RegisterRequest model)
        {
            if (await checkIfUserExists(model))
            {
                throw new EntityNotFoundException("User with username: " + model.Username + " already exists.");
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(), //Random value to change each time credential information about user changes
                UserName = model.Username,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors;
                string errorString = "";
                foreach (var error in errors)
                {
                    errorString += error.Description + Environment.NewLine;
                }
                throw new Exception("Unable to regiter user. " + errorString);
            }
            await _сreateRolesIfDoesNotExists();
            await userManager.AddToRoleAsync(user, Roles.Admin);

            return result;
        }
        public async Task<TokenResponse> LoginUser(LoginRequest model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                throw new EntityNotFoundException("User with a username: " + model.Username + " does not exists. Unable to login");
            }
            if (!(await userManager.CheckPasswordAsync(user, model.Password)))
            {
                throw new WrongDataFormatException("Password is incorrect. Unable to login");
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string secretKey = _configuration["JWT: SecretKey"];
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var generatedRefreshToken = _tokenService.GenerateRefreshToken();

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT: ValidIssuer"],
                audience: _configuration["JWT: ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

            );

            user.refreshToken = generatedRefreshToken;

            await userManager.UpdateAsync(user);
            return new TokenResponse { refreshToken = generatedRefreshToken, accessToken = token };
     

        }
        public async Task<TokenResponse> RefreshAccessToken(TokenRequestDTO request)
        {
            if (request.Token == null || request.RefreshToken == null)
                throw new WrongDataFormatException("Both access and refresh token are required for refreshing");
            string accessToken = request.Token;
            string refreshToken = request.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
                throw new WrongDataFormatException("Invalid request token");
            var username = principal.Identity!.Name; //this is mapped to the Name claim by default
            var user = await userManager.FindByNameAsync(username);
            if (user is null || user.refreshToken!.Token != refreshToken || user.refreshToken.ExpirationDate <= DateTime.Now)
            {
                throw new WrongDataFormatException("Refresh token isn't tied to any user or expired");
            }
            string secretKey = _configuration["JWT: SecretKey"];
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var newAccessToken = new JwtSecurityToken(
                issuer: _configuration["JWT: ValidIssuer"],
                audience: _configuration["JWT: ValidAudience"],
                expires: DateTime.Now.AddHours(1),

                claims: principal.Claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.refreshToken = newRefreshToken;
            await userManager.UpdateAsync(user);
            return new TokenResponse { refreshToken = newRefreshToken, accessToken = newAccessToken };
        }
    }
}
