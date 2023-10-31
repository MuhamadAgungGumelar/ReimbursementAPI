using Microsoft.AspNetCore.Mvc;
using ReimbursementAPI.DTO.Reimbursement;
using ReimbursementAPI.Models;
using ReimbursementWebApp.Contract;
using ReimbursementWebApp.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace ReimbursementWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReimbursementRepository _reimbursementRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IReimbursementRepository reimbursementRepository)
        {
            _logger = logger;
            _reimbursementRepository = reimbursementRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }

        public IActionResult Reimbursement()
        {
            return View();
        }

        public IActionResult ReimbursementForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReimbursement(CreateReimburseViewModel createReimbursementsDto)
        {
            var newReimbursement = new NewReimbursementsDto
            {
                EmployeeGuid = createReimbursementsDto.EmployeeGuid,
                Name = createReimbursementsDto.Name,
                Value = createReimbursementsDto.Value,
                Status = createReimbursementsDto.Status,
                Description = createReimbursementsDto.Description,
            };

            if (ModelState.IsValid)
            {
                using (var _MemoryStream = new MemoryStream())
                {
                    createReimbursementsDto.ImageFile.CopyTo(_MemoryStream);
                    newReimbursement.Image = _MemoryStream.ToArray();
                }
                newReimbursement.ImageType = createReimbursementsDto.ImageFile.ContentType;
            }
            var result = await _reimbursementRepository.Post(newReimbursement);

            if (result != null && result.Code == 200)
            {
                return RedirectToAction();
            }

            return View(createReimbursementsDto);
        }

        public IActionResult Manager()
        {
            return View();
        }

        public IActionResult Finance()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}