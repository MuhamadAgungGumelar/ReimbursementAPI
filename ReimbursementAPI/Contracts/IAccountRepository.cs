using ReimbursementAPI.DTO.Account;
using ReimbursementAPI.Models;

namespace ReimbursementAPI.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Accounts>
    {
        RegisterAccountsRequestDto? Register(RegisterAccountsRequestDto request, bool isValid);
        Employees? Login(string email);
    }
}
