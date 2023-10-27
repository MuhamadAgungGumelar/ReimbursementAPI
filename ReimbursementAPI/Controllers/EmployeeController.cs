using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementAPI.Contracts;
using ReimbursementAPI.DTO.Employee;
using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Handler;

namespace ReimbursementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //Logic untuk Get Employee
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll(); //mengambil semua data Employee
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }
            var data = result.Select(x => (EmployeesDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<EmployeesDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get Employee/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid); //mengambil data Employee By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOKHandler<EmployeesDto>((EmployeesDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post Employee/
        [HttpPost]
        public IActionResult Insert(NewEmployeesDto newEmployeesDto)
        {
            try
            {
                var result = _employeeRepository.Create(newEmployeesDto); //melakukan Create Employee

                return Ok(new ResponseOKHandler<EmployeesDto>((EmployeesDto)result, "Insert Success"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT Employee
        [HttpPut]
        public IActionResult Update(EmployeesDto employeesDto)
        {
            try
            {
                var entity = _employeeRepository.GetByGuid(employeesDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not found"));
                }

                _employeeRepository.Update(employeesDto); //melakukan update Employee

                return Ok(new ResponseOKHandler<EmployeesDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Employee
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var employee = _employeeRepository.GetByGuid(guid); //mengambil employee by GUID
                if (employee is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not found"));
                }

                _employeeRepository.Delete(employee); //melakukan Delete Employee

                return Ok(new ResponseOKHandler<EmployeesDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }

    }
}
