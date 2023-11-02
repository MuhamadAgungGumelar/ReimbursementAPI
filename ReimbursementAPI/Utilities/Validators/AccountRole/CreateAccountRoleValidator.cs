using FluentValidation;
using ReimbursementAPI.DTO.AccountRole;

namespace ReimbursementAPI.Utilities.Validators.AccountRole
{
    public class CreateAccountRoleValidator : AbstractValidator<NewAccountRolesDto>
    {
        public CreateAccountRoleValidator()
        {
            RuleFor(e => e.AccountGuid).NotEmpty();
            RuleFor(e => e.RoleGuid).NotEmpty();
        }
    }
}
