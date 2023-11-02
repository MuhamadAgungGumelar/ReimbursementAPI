using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementAPI.Contracts;
using ReimbursementAPI.DTO.AccountRole;
using ReimbursementAPI.Utilities.Handler;

namespace ReimbursementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        //Logic untuk Get AccountRole
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRoleRepository.GetAll(); //mengambil semua data AccountRole
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var data = result.Select(x => (AccountRolesDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<AccountRolesDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get AccountRole/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRoleRepository.GetByGuid(guid); //mengambil data AccountRole By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOKHandler<AccountRolesDto>((AccountRolesDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post AccountRole/
        [HttpPost]
        public IActionResult Insert(NewAccountRolesDto newAccountRole)
        {
            try
            {
                var result = _accountRoleRepository.Create(newAccountRole); //melakukan Create AccountRole
                return Ok(new ResponseOKHandler<AccountRolesDto>((AccountRolesDto)result, "Insert Success"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT AccountRole
        [HttpPut]
        public IActionResult Update(AccountRolesDto accountRolesDto)
        {
            try
            {
                var entity = _accountRoleRepository.GetByGuid(accountRolesDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _accountRoleRepository.Update(accountRolesDto); //melakukan update AccountRole

                return Ok(new ResponseOKHandler<AccountRolesDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete AccountRole
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var accountRole = _accountRoleRepository.GetByGuid(guid); //mengambil role by GUID
                if (accountRole is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _accountRoleRepository.Delete(accountRole); //melakukan Delete AccountRole

                return Ok(new ResponseOKHandler<AccountRolesDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
