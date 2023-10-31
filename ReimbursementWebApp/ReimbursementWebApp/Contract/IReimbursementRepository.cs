using ReimbursementAPI.DTO.Reimbursement;
using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Handler;

namespace ReimbursementWebApp.Contract
{
    public interface IReimbursementRepository
    {
        Task<ResponseOKHandler<NewReimbursementsDto>> Post(Reimbursements entity);
    }
}
