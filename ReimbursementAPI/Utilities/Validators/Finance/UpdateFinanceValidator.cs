using FluentValidation;
using ReimbursementAPI.DTO.Finance;

namespace ReimbursementAPI.Utilities.Validators.Finance
{
    public class UpdateFinanceValidator : AbstractValidator<FinancesDto>
    {
        public UpdateFinanceValidator()
        {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.EmployeeGuid).NotEmpty();
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.ApprovedAmount).NotEmpty();
            RuleFor(e => e.ApprovedDate).NotEmpty();
        }
    }
}
