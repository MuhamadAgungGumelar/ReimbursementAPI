using System.Security.Claims;

namespace ReimbursementAPI.Contracts
{
    public interface ITokenHandler
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
