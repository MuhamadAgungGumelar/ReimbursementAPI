using FluentValidation;
using ReimbursementAPI.DTO.Account;

namespace ReimbursementAPI.Utilities.Validators.Account
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequestDto>
    {
        //public string Email { get; set; }
        //public int Otp { get; set; }
        //public string NewPassword { get; set; }
        //public string ConfirmPassword { get; set; }
        public ChangePasswordValidator()
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.Otp).NotEmpty();
            RuleFor(e => e.NewPassword).NotEmpty().Length(8, 50);
            RuleFor(e => e.ConfirmPassword).NotEmpty().Equal(e => e.NewPassword);
        }
    }
}
