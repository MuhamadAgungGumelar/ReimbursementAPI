using ReimbursementAPI.Contracts;
using ReimbursementAPI.Data;
using ReimbursementAPI.Models;

namespace ReimbursementAPI.Repository
{
    public class EmployeeRepository : GeneralRepository<Employees>, IEmployeeRepository
    {
        public EmployeeRepository(ReimbursementDBContext context) : base(context)
        {
        }
    }
}
