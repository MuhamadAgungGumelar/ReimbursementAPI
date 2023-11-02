using FluentValidation;
using ReimbursementAPI.DTO.Finance;

namespace ReimbursementAPI.Utilities.Validators.Finance
{
    public class CreateFinanceValidator : AbstractValidator<NewFinancesDto>
    {
        public CreateFinanceValidator()
        {
            RuleFor(e => e.EmployeeGuid).NotEmpty();
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.ApprovedAmount).NotEmpty();
            RuleFor(e => e.ApprovedDate).NotEmpty();
        }
    }
}
