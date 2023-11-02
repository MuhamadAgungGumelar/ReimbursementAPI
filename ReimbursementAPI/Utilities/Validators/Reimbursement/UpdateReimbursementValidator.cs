using FluentValidation;
using ReimbursementAPI.DTO.Reimbursement;

namespace ReimbursementAPI.Utilities.Validators.Reimbursement
{
    public class UpdateReimbursementValidator : AbstractValidator<ReimbursementsDto>
    {
        public UpdateReimbursementValidator()
        {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.EmployeeGuid).NotEmpty();
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Value).NotEmpty();
            RuleFor(e => e.ImageType).NotEmpty();
            RuleFor(e => e.Image).NotEmpty();
            RuleFor(e => e.Status).NotNull().IsInEnum();
        }
    }
}
