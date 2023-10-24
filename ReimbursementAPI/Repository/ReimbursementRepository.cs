using ReimbursementAPI.Contracts;
using ReimbursementAPI.Data;
using ReimbursementAPI.Models;

namespace ReimbursementAPI.Repository
{
    public class ReimbursementRepository : GeneralRepository<Reimbursements>, IReimbursementRepository
    {
        public ReimbursementRepository(ReimbursementDBContext context) : base(context)
        {
        }
    }
}
