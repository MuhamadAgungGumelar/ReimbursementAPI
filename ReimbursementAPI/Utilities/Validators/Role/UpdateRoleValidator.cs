using FluentValidation;
using ReimbursementAPI.DTO.Role;

namespace ReimbursementAPI.Utilities.Validators.Role
{
    public class UpdateRoleValidator : AbstractValidator<RolesDto>
    {
        public UpdateRoleValidator()
        {
            RuleFor(e => e.Guid).NotEmpty(); //rule validator guid
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100); //rule validator name
        }
    }
}
