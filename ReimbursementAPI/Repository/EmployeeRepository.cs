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

        public Employees? GetByEmail(string email)
        {
            var employee = _context.Set<Employees>().FirstOrDefault(e => e.Email == email); //mengambil data employee berdasarkan email

            return employee;
        }
    }
}
