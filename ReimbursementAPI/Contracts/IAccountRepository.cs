using ReimbursementAPI.DTO.Account;
using ReimbursementAPI.Models;

namespace ReimbursementAPI.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Accounts>
    {
        RegisterAccountResponseDto? Register(RegisterAccountsRequestDto request);
        Employees? Login(string email);
    }
}
