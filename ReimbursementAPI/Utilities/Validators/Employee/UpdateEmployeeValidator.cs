using FluentValidation;
using ReimbursementAPI.DTO.Employee;

namespace ReimbursementAPI.Utilities.Validators.Employee
{
    public class UpdateEmployeeValidator : AbstractValidator<EmployeesDto>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(e => e.Guid).NotEmpty(); //rule validator guid
            RuleFor(e => e.FirstName).NotEmpty(); //rule validator first name
            RuleFor(e => e.LastName).MaximumLength(100); //rule validator last name
            RuleFor(e => e.BirthDate) //rule validator birth date
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18)); //18 y.o
            RuleFor(e => e.Gender) //rule validator gender
                .NotEmpty()
                .IsInEnum();
            RuleFor(e => e.HiringDate) //rule validator hiring date
                .NotEmpty();
            RuleFor(e => e.Email) //rule validator email
                .NotEmpty()
                .EmailAddress();
            RuleFor(e => e.PhoneNumber) //rule validator phone number
                .NotEmpty()
                .Length(10, 20);
        }
    }
}
