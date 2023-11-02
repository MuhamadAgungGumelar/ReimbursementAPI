using FluentValidation;
using ReimbursementAPI.DTO.Account;

namespace ReimbursementAPI.Utilities.Validators.Account
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequestDto>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
        }
    }
}
