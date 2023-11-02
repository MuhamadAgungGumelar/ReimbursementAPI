using FluentValidation;
using ReimbursementAPI.DTO.Reimbursement;

namespace ReimbursementAPI.Utilities.Validators.Reimbursement
{
    public class CreateReimbursementValidator : AbstractValidator<NewReimbursementsDto>
    {
        public CreateReimbursementValidator()
        {
            RuleFor(e => e.EmployeeGuid).NotEmpty();
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Value).NotEmpty();
            RuleFor(e => e.ImageType).NotEmpty();
            RuleFor(e => e.Image).NotEmpty();
            RuleFor(e => e.Status).NotEmpty().IsInEnum();
        }
    }
}
