using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementAPI.Contracts;
using ReimbursementAPI.DTO.Finance;
using ReimbursementAPI.Utilities.Handler;

namespace ReimbursementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Finances, Manager")]
    public class FinancesController : ControllerBase
    {
        private readonly IFinanceRepository _financeRepository;

        public FinancesController(IFinanceRepository financeRepository)
        {
            _financeRepository = financeRepository;
        }

        // Get Finance
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _financeRepository.GetAll(); //mengambil semua data Finance
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }
            var data = result.Select(x => (FinancesDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<FinancesDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get Finance/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _financeRepository.GetByGuid(guid); //mengambil data Finance By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOKHandler<FinancesDto>((FinancesDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post Finance/
        [HttpPost]
        public IActionResult Insert(NewFinancesDto newFinancesDto)
        {
            try
            {
                var result = _financeRepository.Create(newFinancesDto); //melakukan Create Finance
                return Ok(new ResponseOKHandler<FinancesDto>((FinancesDto)result, "Insert Success"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT Finance
        [HttpPut]
        public IActionResult Update(FinancesDto roleDto)
        {
            try
            {
                var entity = _financeRepository.GetByGuid(roleDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _financeRepository.Update(roleDto); //melakukan update Finance

                return Ok(new ResponseOKHandler<FinancesDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Finance
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var role = _financeRepository.GetByGuid(guid); //mengambil finance by GUID
                if (role is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _financeRepository.Delete(role); //melakukan Delete Finance

                return Ok(new ResponseOKHandler<FinancesDto>("Data has been Deleted"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
