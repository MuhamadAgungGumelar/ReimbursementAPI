using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Enums;

namespace ReimbursementAPI.DTO.Employee
{
    public class NewEmployeesDto
    {
        public string FirstName { get; set; } //deklarasi property
        public string? LastName { get; set; } //deklarasi property
        public DateTime BirthDate { get; set; } //deklarasi property
        public GenderLevel Gender { get; set; } //deklarasi property
        public DateTime HiringDate { get; set; } //deklarasi property
        public string Email { get; set; } //deklarasi property
        public string PhoneNumber { get; set; } //deklarasi property
        public Guid ManagerGuid { get; set; }

        public static implicit operator Employees(NewEmployeesDto dto) //implementasi implicit Operator
        {
            return new Employees
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                HiringDate = dto.HiringDate,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
