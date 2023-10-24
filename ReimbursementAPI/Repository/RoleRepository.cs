using ReimbursementAPI.Contracts;
using ReimbursementAPI.Data;
using ReimbursementAPI.Models;

namespace ReimbursementAPI.Repository
{
    public class RoleRepository : GeneralRepository<Roles>, IRoleRepository
    {
        public RoleRepository(ReimbursementDBContext context) : base(context)
        {
        }
    }
}
