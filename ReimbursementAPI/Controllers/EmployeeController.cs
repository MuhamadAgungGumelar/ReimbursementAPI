using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IAccountRepository accountRepository)
        {
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet("employeeDetails")]
        public IActionResult GetDetails()
        {
            var employee = _employeeRepository.GetAll(); //mengambil semua data Employee
            if (!employee.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }
            var account = _accountRepository.GetAll();
            if (!account.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var details = from emp in employee
                          join ac in account on emp.Guid equals ac.Guid
                          select new EmployeeDetailDto
                          {
                              Guid = emp.Guid,
                              FirstName = emp.FirstName,
                              LastName = emp.LastName,
                              BirthDate = emp.BirthDate,
                              Gender = emp.Gender,
                              HiringDate = emp.HiringDate,
                              Email = emp.Email,
                              PhoneNumber = emp.PhoneNumber,
                              ManagerGuid = emp.ManagerGuid,
                              IsActivated = ac.IsActivated
                          };

            return Ok(new ResponseOKHandler<IEnumerable<EmployeeDetailDto>>(details, "data retrieve successfully"));
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
