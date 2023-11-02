using FluentValidation;
using ReimbursementAPI.DTO.Account;
using ReimbursementAPI.Utilities.Enums;

namespace ReimbursementAPI.Utilities.Validators.Account
{
    public class RegisterAccountValidator : AbstractValidator<RegisterAccountsRequestDto>
    {
        public RegisterAccountValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(e => e.FirstName).MaximumLength(100);
            RuleFor(e => e.BirthDate).NotEmpty().LessThanOrEqualTo(DateTime.Now.AddYears(-18));
            RuleFor(e => e.Gender).NotNull().IsInEnum();
            RuleFor(e => e.HiringDate).NotEmpty();
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.PhoneNumber).NotEmpty().Length(10, 20);
            RuleFor(e => e.Password).NotEmpty().Length(8, 50);
            RuleFor(e => e.ConfirmPassword).NotEmpty().Equal(e => e.Password);
        }
    }
}
