using Microsoft.AspNetCore.Mvc;
using ReimbursementAPI.DTO.Account;
using ReimbursementWebApp.Contract;

namespace ReimbursementWebApp.Controllers
{
    public class AuthController: Controller
    {
        private readonly IAccountRepository _accountRepository;
        //private readonly ITokenHandler _tokenService;

        public AuthController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            //_tokenService = tokenService;
        }

        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPage(AccountLoginDto loginAccountDto)
        {
            // Kirim permintaan login ke server API menggunakan IAuthRepository
            var result = await _accountRepository.Login(loginAccountDto);

            if (result.Status == "OK")
            {
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(loginAccountDto);
            }
        }

        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage", "Auth");
        }
    }
}
