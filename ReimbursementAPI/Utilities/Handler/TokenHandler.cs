using Microsoft.IdentityModel.Tokens;
using ReimbursementAPI.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReimbursementAPI.Utilities.Handler
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTService:SecretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOption = new JwtSecurityToken(issuer: _configuration["JWTService:Issuer"],
                audience: _configuration["JWTService:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: signingCredentials);
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return encodedToken;
        }
    }
}
