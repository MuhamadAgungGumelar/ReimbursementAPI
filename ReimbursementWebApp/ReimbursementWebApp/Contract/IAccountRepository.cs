using ReimbursementAPI.DTO.Account;
using ReimbursementAPI.Utilities.Handler;
using ReimbursementWebApp.Models;

namespace ReimbursementWebApp.Contract
{
    public interface IAccountRepository : IRepository<AccountsDto, Guid>
    {
        Task<ResponseOKHandler<TokenDto>> Login(AccountLoginDto loginAccountDto);
    }
}
