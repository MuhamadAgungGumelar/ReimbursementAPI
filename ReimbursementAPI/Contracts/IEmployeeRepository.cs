using ReimbursementAPI.Models;

namespace ReimbursementAPI.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employees>
    {
        Employees? GetByEmail(string email);
    }
}
