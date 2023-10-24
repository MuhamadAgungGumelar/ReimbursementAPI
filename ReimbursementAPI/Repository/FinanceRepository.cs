using ReimbursementAPI.Contracts;
using ReimbursementAPI.Data;
using ReimbursementAPI.Models;

namespace ReimbursementAPI.Repository
{
    public class FinanceRepository : GeneralRepository<Finances>, IFinanceRepository
    {
        public FinanceRepository(ReimbursementDBContext context) : base(context)
        {
        }
    }
}
