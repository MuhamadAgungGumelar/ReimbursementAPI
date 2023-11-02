using FluentValidation;
using ReimbursementAPI.DTO.Role;

namespace ReimbursementAPI.Utilities.Validators.Role
{
    public class CreateRoleValidator : AbstractValidator<NewRolesDto>
    {
        public CreateRoleValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100); //rule validator name
        }
    }
}
