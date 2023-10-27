using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementAPI.Contracts;
using ReimbursementAPI.DTO.Role;
using ReimbursementAPI.Utilities.Handler;

namespace ReimbursementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // Get Role
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleRepository.GetAll(); //mengambil semua data Role
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }
            var data = result.Select(x => (RolesDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<RolesDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get Role/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roleRepository.GetByGuid(guid); //mengambil data Role By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOKHandler<RolesDto>((RolesDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post Role/
        [HttpPost]
        public IActionResult Insert(NewRolesDto newRolesDto)
        {
            try
            {
                var result = _roleRepository.Create(newRolesDto); //melakukan Create Role
                return Ok(new ResponseOKHandler<RolesDto>((RolesDto)result, "Insert Success"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT Role
        [HttpPut]
        public IActionResult Update(RolesDto roleDto)
        {
            try
            {
                var entity = _roleRepository.GetByGuid(roleDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _roleRepository.Update(roleDto); //melakukan update Role

                return Ok(new ResponseOKHandler<RolesDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Role
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var role = _roleRepository.GetByGuid(guid); //mengambil role by GUID
                if (role is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _roleRepository.Delete(role); //melakukan Delete Role

                return Ok(new ResponseOKHandler<RolesDto>("Data has been Deleted"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
