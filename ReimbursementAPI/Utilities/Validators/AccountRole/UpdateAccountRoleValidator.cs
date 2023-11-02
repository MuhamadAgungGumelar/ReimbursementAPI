using FluentValidation;
using ReimbursementAPI.DTO.AccountRole;

namespace ReimbursementAPI.Utilities.Validators.AccountRole
{
    public class UpdateAccountRoleValidator : AbstractValidator<AccountRolesDto>
    {
        public UpdateAccountRoleValidator()
        {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.AccountGuid).NotEmpty();
            RuleFor(e => e.RoleGuid).NotEmpty();
        }
    }
}
