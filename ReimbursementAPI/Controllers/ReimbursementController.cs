using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementAPI.Contracts;
using ReimbursementAPI.DTO.Reimbursement;
using ReimbursementAPI.Repository;
using ReimbursementAPI.Utilities.Handler;

namespace ReimbursementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReimbursementController : ControllerBase
    {
        private readonly IReimbursementRepository _reimbursementRepository;

        public ReimbursementController(IReimbursementRepository reimbursementRepository)
        {
            _reimbursementRepository = reimbursementRepository;
        }

        // Get Reimbursement
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _reimbursementRepository.GetAll(); //mengambil semua data Reimbursement
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }
            var data = result.Select(x => (ReimbursementsDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<ReimbursementsDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get Reimbursement/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _reimbursementRepository.GetByGuid(guid); //mengambil data Reimbursement By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOKHandler<ReimbursementsDto>((ReimbursementsDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post Reimbursement/
        [HttpPost]
        public IActionResult Insert(NewReimbursementsDto newReimbursementsDto)
        {
            try
            {
                var result = _reimbursementRepository.Create(newReimbursementsDto); //melakukan Create Reimbursement
                return Ok(new ResponseOKHandler<ReimbursementsDto>((ReimbursementsDto)result, "Insert Success"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT Reimbursement
        [HttpPut]
        public IActionResult Update(ReimbursementsDto roleDto)
        {
            try
            {
                var entity = _reimbursementRepository.GetByGuid(roleDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _reimbursementRepository.Update(roleDto); //melakukan update Reimbursement

                return Ok(new ResponseOKHandler<ReimbursementsDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Reimbursement
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var role = _reimbursementRepository.GetByGuid(guid); //mengambil finance by GUID
                if (role is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _reimbursementRepository.Delete(role); //melakukan Delete Reimbursement

                return Ok(new ResponseOKHandler<ReimbursementsDto>("Data has been Deleted"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
