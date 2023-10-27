using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Enums;

namespace ReimbursementAPI.DTO.Employee
{
    public class EmployeesDto : GeneralDto
    {
        public string FirstName { get; set; } //deklarasi property
        public string? LastName { get; set; } //deklarasi property
        public DateTime BirthDate { get; set; } //deklarasi property
        public GenderLevel Gender { get; set; } //deklarasi property
        public DateTime HiringDate { get; set; } //deklarasi property
        public string Email { get; set; } //deklarasi property
        public string PhoneNumber { get; set; } //deklarasi property
        public Guid? ManagerGuid { get; set; } //deklarasi property

        public static explicit operator EmployeesDto(Employees employee) //implementasi explicit Operator
        {
            return new EmployeesDto
            {
                Guid = employee.Guid,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                ManagerGuid = employee.ManagerGuid,
            };
        }

        public static implicit operator Employees(EmployeesDto employeeDto) //implementasi implicit Operator
        {
            return new Employees
            {
                Guid = employeeDto.Guid,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                Gender = employeeDto.Gender,
                HiringDate = employeeDto.HiringDate,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                ManagerGuid = employeeDto.ManagerGuid,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
