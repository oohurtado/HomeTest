using Home.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Home.Source.Helpers
{
    public static class TokenHelper
    {
        public static UserTokenDTO BuildToken(List<Claim> claims, string jwtKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(365);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            var userToken = new UserTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = expiration,
            };

            return userToken;
        }

        public static List<Claim> CreateClaims( string? userId, string? email, List<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId!),
                new Claim(ClaimTypes.Email, email!),
            };

            roles.ForEach(p => claims.Add(new Claim(ClaimTypes.Role, p)));

            return claims;
        }
    }
}
