using ReimbursementAPI.Contracts;
using ReimbursementAPI.Data;
using ReimbursementAPI.Models;

namespace ReimbursementAPI.Repository
{
    public class AccountRoleRepository : GeneralRepository<AccountRoles>, IAccountRoleRepository
    {
        public AccountRoleRepository(ReimbursementDBContext context) : base(context)
        {
        }
    }
}
