using FluentValidation;
using ReimbursementAPI.DTO.Account;

namespace ReimbursementAPI.Utilities.Validators.Account
{
    public class LoginValidator : AbstractValidator<AccountLoginDto>
    {
        public LoginValidator()
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(e => e.Password).NotEmpty().MaximumLength(50);
        }
    }
}
