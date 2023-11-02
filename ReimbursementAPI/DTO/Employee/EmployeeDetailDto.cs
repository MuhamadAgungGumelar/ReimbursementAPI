using ReimbursementAPI.Utilities.Enums;

namespace ReimbursementAPI.DTO.Employee
{
    public class EmployeeDetailDto : GeneralDto
    {
        public string FirstName { get; set; } //deklarasi property
        public string? LastName { get; set; } //deklarasi property
        public DateTime BirthDate { get; set; } //deklarasi property
        public GenderLevel Gender { get; set; } //deklarasi property
        public DateTime HiringDate { get; set; } //deklarasi property
        public string Email { get; set; } //deklarasi property
        public string PhoneNumber { get; set; } //deklarasi property
        public Guid? ManagerGuid { get; set; } //deklarasi property
        public bool IsActivated { get; set; }
    }
}
