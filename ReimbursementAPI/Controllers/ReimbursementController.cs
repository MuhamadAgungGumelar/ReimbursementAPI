using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReimbursementAPI.Contracts;
using ReimbursementAPI.DTO.Reimbursement;
using ReimbursementAPI.Models;
using ReimbursementAPI.Repository;
using ReimbursementAPI.Utilities.Handler;
using System.Security.Claims;

namespace ReimbursementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReimbursementController : ControllerBase
    {
        private readonly IReimbursementRepository _reimbursementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenHandler _tokenHandler;

        public ReimbursementController(IReimbursementRepository reimbursementRepository, ITokenHandler tokenHandler, IEmployeeRepository employeeRepository)
        {
            _reimbursementRepository = reimbursementRepository;
            _tokenHandler = tokenHandler;
            _employeeRepository = employeeRepository;
        }

        // Get Reimbursement
        [HttpGet]
        public IActionResult GetAll()
        {
            String header = Request.Headers["Authorization"];
            header = header.Replace("Bearer ", "");
            if (header == "")
            {
                return Unauthorized();
            }
            var token = _tokenHandler.DecodeToken(header);
            var id = new Guid(token.Claims.First(claim => claim.Type == "Id").Value);
            var role = token?.Claims.Last(claim => claim.Type == ClaimTypes.Role).Value;

            var result = _reimbursementRepository.GetAll(); //mengambil semua data Reimbursement
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            if (role == "Manager")
            {
                var manager = from em in _employeeRepository.GetAll()
                              join r in result on em.Guid equals r.EmployeeGuid
                              where em.ManagerGuid == id
                              select new
                              {
                                  r.Guid,
                                  r.EmployeeGuid,
                                  EmployeeName = string.Concat(em.FirstName, " ", em.LastName),
                                  r.Name,
                                  r.Description,
                                  r.Value,
                                  r.ImageType,
                                  r.Image,
                                  r.Status,
                                  r.CreatedDate
                              };
                if (!manager.Any())
                {
                    return NotFound(new ResponseNotFoundHandler("Data not found"));
                }

                var managerData = manager.Select(x => (object)x);
                return Ok(new ResponseOKHandler<IEnumerable<object>>(managerData, "Data retrieve Successfully"));
            }

            if (role == "Finances")
            {
                var finance = from em in _employeeRepository.GetAll()
                              join r in result on em.Guid equals r.EmployeeGuid
                              where r.Status != Utilities.Enums.StatusLevel.reimburse_rejected_by_manager && r.Status != Utilities.Enums.StatusLevel.waiting_manager_approval_reimburse
                              orderby r.CreatedDate descending
                              select new
                              {
                                  r.Guid,
                                  r.EmployeeGuid,
                                  EmployeeName = string.Concat(em.FirstName, " ", em.LastName),
                                  r.Name,
                                  r.Description,
                                  r.Value,
                                  r.ImageType,
                                  r.Image,
                                  r.Status,
                                  r.CreatedDate
                              };
                if (!finance.Any())
                {
                    return NotFound(new ResponseNotFoundHandler("Data not found"));
                }

                var financeData = finance.Select(x => (object)x);
                return Ok(new ResponseOKHandler<IEnumerable<object>>(finance, "Data retrieve Successfully"));
            }

            var employee = from em in _employeeRepository.GetAll()
                           join r in result on em.Guid equals r.EmployeeGuid
                           where em.Guid == id
                           select r;
            var data = employee.Select(x => (ReimbursementsDto)x);

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
        public IActionResult Update(ReimbursementsDto reimbursementDto)
        {
            try
            {
                var entity = _reimbursementRepository.GetByGuid(reimbursementDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                var toUpdate = new Reimbursements
                {
                    Guid = reimbursementDto.Guid,
                    EmployeeGuid = reimbursementDto.EmployeeGuid,
                    Name = reimbursementDto.Name,
                    Description = reimbursementDto.Description,
                    Value = reimbursementDto.Value,
                    ImageType = reimbursementDto.ImageType,
                    Image = reimbursementDto.Image,
                    Status = reimbursementDto.Status,
                    CreatedDate = entity.CreatedDate,
                    ModifiedDate = DateTime.Now,
                };


                _reimbursementRepository.Update(toUpdate); //melakukan update Reimbursement

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
