using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReimbursementAPI.Contracts
{
    public interface ITokenHandler
    {
        string GenerateToken(IEnumerable<Claim> claims);
        public JwtSecurityToken? DecodeToken(string token);
    }
}
