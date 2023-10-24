using ReimbursementAPI.Contracts;
using ReimbursementAPI.Data;
using ReimbursementAPI.Models;

namespace ReimbursementAPI.Repository
{
    public class AccountRepository : GeneralRepository<Accounts>, IAccountRepository
    {
        public AccountRepository(ReimbursementDBContext context) : base(context)
        {
        }
    }
}
