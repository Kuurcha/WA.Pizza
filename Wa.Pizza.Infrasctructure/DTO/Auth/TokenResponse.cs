using System.IdentityModel.Tokens.Jwt;

namespace Wa.Pizza.Infrasctructure.DTO.Auth
{
    public class TokenResponse
    {
        public RefreshToken refreshToken { get; set; }

        public JwtSecurityToken accessToken { get; set; }


    }
}
