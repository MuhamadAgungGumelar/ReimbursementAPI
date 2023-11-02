using FluentValidation;
using ReimbursementAPI.DTO.Employee;

namespace ReimbursementAPI.Utilities.Validators.Employee
{
    public class CreateEmployeeValidator : AbstractValidator<NewEmployeesDto>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().MaximumLength(100); //rule validator first name
            RuleFor(e => e.LastName).MaximumLength(100); //rule validator last name
            RuleFor(e => e.BirthDate) //rule validator birth date
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18)); //18 y.o
            RuleFor(e => e.Gender) //rule validator gender
                .NotNull()
                .IsInEnum();
            RuleFor(e => e.HiringDate) //rule validator hiring date
                .NotEmpty();
            RuleFor(e => e.Email) //rule validator email
                .NotEmpty()
                .EmailAddress().MaximumLength(100);
            RuleFor(e => e.PhoneNumber) //rule validator phone number
                .NotEmpty()
                .Length(10, 20);
        }
    }
}
