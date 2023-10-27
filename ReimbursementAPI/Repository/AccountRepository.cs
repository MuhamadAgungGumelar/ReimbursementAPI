using ReimbursementAPI.Contracts;
using ReimbursementAPI.Data;
using ReimbursementAPI.DTO.Account;
using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Handler;
using System.Data;

namespace ReimbursementAPI.Repository
{
    public class AccountRepository : GeneralRepository<Accounts>, IAccountRepository
    {
        public AccountRepository(ReimbursementDBContext context) : base(context)
        {
        }

        public Employees? Login(string email)
        {
            var employee = _context.Set<Employees>().FirstOrDefault(e => e.Email == email); //mengambil data employee berdasarkan email

            return employee;

        }

        public RegisterAccountResponseDto? Register(RegisterAccountsRequestDto request)
        {
            var transaction = _context.Database.BeginTransaction(); //melakukan inisiasi transaction
            try
            {
                var employee = new Employees(); //melakukan inject data baru ke dalam objek employee
                employee.FirstName = request.FirstName;
                employee.LastName = request.LastName;
                employee.BirthDate = request.BirthDate;
                employee.Gender = request.Gender;
                employee.HiringDate = request.HiringDate;
                employee.Email = request.Email;
                employee.PhoneNumber = request.PhoneNumber;
                employee.ManagerGuid = request.ManagerGuid;
                employee.CreatedDate = DateTime.Now;
                employee.ModifiedDate = DateTime.Now;

                _context.Set<Employees>().Add(employee); //melakukan add employee baru ke database
                _context.SaveChanges(); //melakukan save kedalam database

                var newEmployee = _context.Set<Employees>().Where(e => e.Email == employee.Email).FirstOrDefault();
                _context.ChangeTracker.Clear(); //melakukan clear changetracker

                var otp = OtpHandler.GenerateRandomOtp(); //melakukan generate otp
                var account = new Accounts(); //melakukan inject data baru ke dalam object account
                account.Guid = newEmployee.Guid;
                account.Password = HashHandler.HashPassword(request.Password); //melakukan hash password yang akan disimpan
                account.Otp = otp;
                account.IsUsed = false;
                account.IsActivated = false;
                account.ExpiredTime = DateTime.Now.AddDays(1);
                account.CreatedDate = DateTime.Now;
                account.ModifiedDate = DateTime.Now;
                _context.Set<Accounts>().Add(account); //melakukan add account baru ke database
                _context.SaveChanges(); //melakukan save kedalam database

                var defaultRole = _context.Set<Roles>().FirstOrDefault(r => r.Name == "User").Guid;
                _context.ChangeTracker.Clear(); //melakukan clear changetracker

                var accountRole = new AccountRoles();
                accountRole.AccountGuid = newEmployee.Guid;
                accountRole.RoleGuid = defaultRole;
                _context.Set<AccountRoles>().Add(accountRole); //melakukan add account baru ke database
                _context.SaveChanges(); //melakukan save kedalam database

                transaction.Commit(); //melakukan commit transaction setelah semua berhasil
                return new RegisterAccountResponseDto
                {
                    Email = employee.Email,
                    Otp = account.Otp,
                }; //mengembalikan data request
            }
            catch (Exception ex)
            {
                transaction.Rollback(); //melakukan rollback transaction ketika transaction gagal
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
