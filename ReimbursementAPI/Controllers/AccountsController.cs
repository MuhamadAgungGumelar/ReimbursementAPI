using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReimbursementAPI.Contracts;
using ReimbursementAPI.DTO.Account;
using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Handler;
using System.Net;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace ReimbursementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountsRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _rolesRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IEmailHandler _emailHandler;

        public AccountsController(IAccountRepository accountsRepository, IAccountRoleRepository accountRoleRepository, IEmployeeRepository employeeRepository, IRoleRepository rolesRepository, ITokenHandler tokenHandler, IEmailHandler emailHandler)
        {
            _accountsRepository = accountsRepository;
            _accountRoleRepository = accountRoleRepository;
            _employeeRepository = employeeRepository;
            _rolesRepository = rolesRepository;
            _tokenHandler = tokenHandler;
            _emailHandler = emailHandler;
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPasswordRequestDto forgotPasswordDto)
        {
            var employee = _employeeRepository.GetByEmail(forgotPasswordDto.Email); //mengambil data employee dari email

            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var otp = OtpHandler.GenerateRandomOtp(); //melakukan generate otp

            var accounts = _accountsRepository.GetByGuid(employee.Guid); //mengambil data account berdasar employee uid
            var otpUpdate = new Accounts();
            otpUpdate.Guid = employee.Guid;
            otpUpdate.Otp = otp; //melakukan inject otp
            otpUpdate.IsUsed = false;
            otpUpdate.Password = accounts.Password;
            otpUpdate.CreatedDate = accounts.CreatedDate;
            otpUpdate.ModifiedDate = DateTime.Now;
            otpUpdate.ExpiredTime = DateTime.Now.AddMinutes(5);
            _accountsRepository.Update(otpUpdate); //melakukan update OTP

            _emailHandler.Send("Forgot Password", $"Your Reset OTP is {otp}", forgotPasswordDto.Email); //mengirimkan otp ke email

            return Ok(new ResponseOKHandler<object>("OTP has send to your email")); //response setelah mengirim otp
        }

        [HttpPut("change-password")]
        [AllowAnonymous]
        public IActionResult ChangePassword(ChangePasswordRequestDto changePasswordRequest)
        {
            var employee = _employeeRepository.GetByEmail(changePasswordRequest.Email); //mengambil data employee berdasar email
            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("data not found"));
            }

            var account = _accountsRepository.GetByGuid(employee.Guid); //mengambil data accoun berdasar employee guid
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("account not found"));
            }

            if (changePasswordRequest.Otp != account.Otp) //mengecek kecocokan otp
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "OTP does not match"));
            }
            if (account.IsUsed == true) //mengecek otp sudah digunakan atau belum
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "This OTP has been used"));
            }
            if (DateTime.Now > account.ExpiredTime) //mengecek apakah otp sudah expired
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "This OTP has been Expired"));
            }

            var toUpdate = new Accounts();
            toUpdate.Guid = account.Guid;
            toUpdate.Password = HashHandler.HashPassword(changePasswordRequest.NewPassword); //melakukan inject password baru
            toUpdate.Otp = account.Otp;
            toUpdate.IsUsed = true;
            toUpdate.ExpiredTime = account.ExpiredTime;
            toUpdate.CreatedDate = account.CreatedDate;
            toUpdate.ModifiedDate = DateTime.Now;

            _accountsRepository.Update(toUpdate); //melakukan update accounts

            return Ok(new ResponseOKHandler<ChangePasswordRequestDto>("Password has been changed"));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterAccountsRequestDto accountRegisterRequest)
        {
            try
            {
                var register = _accountsRepository.Register(accountRegisterRequest); //memanggil function register pada account repo
                var otp = DecoderHandler.Base64Encode($"{register.Otp}");
                string body = $@"<!doctype html>
                    <html>
                      <head>
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                        <title>Simple Transactional Email</title>
                        <style>
                    @media only screen and (max-width: 620px) {{
                      table.body h1 {{
                        font-size: 28px !important;
                        margin-bottom: 10px !important;
                      }}

                      table.body p,
                    table.body ul,
                    table.body ol,
                    table.body td,
                    table.body span,
                    table.body a {{
                        font-size: 16px !important;
                      }}

                      table.body .wrapper,
                    table.body .article {{
                        padding: 10px !important;
                      }}

                      table.body .content {{
                        padding: 0 !important;
                      }}

                      table.body .container {{
                        padding: 0 !important;
                        width: 100% !important;
                      }}

                      table.body .main {{
                        border-left-width: 0 !important;
                        border-radius: 0 !important;
                        border-right-width: 0 !important;
                      }}

                      table.body .btn table {{
                        width: 100% !important;
                      }}

                      table.body .btn a {{
                        width: 100% !important;
                      }}

                      table.body .img-responsive {{
                        height: auto !important;
                        max-width: 100% !important;
                        width: auto !important;
                      }}
                    }}
                    @media all {{
                      .ExternalClass {{
                        width: 100%;
                      }}

                      .ExternalClass,
                    .ExternalClass p,
                    .ExternalClass span,
                    .ExternalClass font,
                    .ExternalClass td,
                    .ExternalClass div {{
                        line-height: 100%;
                      }}

                      .apple-link a {{
                        color: inherit !important;
                        font-family: inherit !important;
                        font-size: inherit !important;
                        font-weight: inherit !important;
                        line-height: inherit !important;
                        text-decoration: none !important;
                      }}

                      #MessageViewBody a {{
                        color: inherit;
                        text-decoration: none;
                        font-size: inherit;
                        font-family: inherit;
                        font-weight: inherit;
                        line-height: inherit;
                      }}

                      .btn-primary table td:hover {{
                        background-color: #34495e !important;
                      }}

                      .btn-primary a:hover {{
                        background-color: #34495e !important;
                        border-color: #34495e !important;
                      }}
                    }}
                    </style>
                      </head>
                      <body style=""background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                        <span class=""preheader"" style=""color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;"">This is preheader text. Some clients will show this text as a preview.</span>
                        <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""body"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #f6f6f6; width: 100%;"" width=""100%"" bgcolor=""#f6f6f6"">
                          <tr>
                            <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top;"" valign=""top"">&nbsp;</td>
                            <td class=""container"" style=""font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; max-width: 580px; padding: 10px; width: 580px; margin: 0 auto;"" width=""580"" valign=""top"">
                              <div class=""content"" style=""box-sizing: border-box; display: block; margin: 0 auto; max-width: 580px; padding: 10px;"">

                                <!-- START CENTERED WHITE CONTAINER -->
                                <table role=""presentation"" class=""main"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background: #ffffff; border-radius: 3px; width: 100%;"" width=""100%"">

                                  <!-- START MAIN CONTENT AREA -->
                                  <tr>
                                    <td class=""wrapper"" style=""font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;"" valign=""top"">
                                      <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;"" width=""100%"">
                                        <tr>
                                          <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top;"" valign=""top"">
                                            <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">Hi there,</p>
                                            <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">You've just registered an account with us, please verify your account by clicking the link below.</p>
                                            <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""btn btn-primary"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; box-sizing: border-box; width: 100%;"" width=""100%"">
                                              <tbody>
                                                <tr>
                                                  <td align=""left"" style=""font-family: sans-serif; font-size: 14px; vertical-align: top; padding-bottom: 15px;"" valign=""top"">
                                                    <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: auto;"">
                                                      <tbody>
                                                        <tr>
                                                          <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center; background-color: #3498db;"" valign=""top"" align=""center"" bgcolor=""#3498db""> <a href=""https://localhost:7257/api/Accounts/activate/{register.Email}?otp={otp}"" target=""_blank"" style=""border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; cursor: pointer; display: inline-block; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-decoration: none; text-transform: capitalize; background-color: #3498db; border-color: #3498db; color: #ffffff;"">Verify Email</a> </td>
                                                        </tr>
                                                      </tbody>
                                                    </table>
                                                  </td>
                                                </tr>
                                              </tbody>
                                            </table>
                                            <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">If the button above doesn't works please access it by clicking the link below.</p>
                                            <a href=""https://localhost:7257/api/Accounts/activate/{register.Email}?otp={otp}""></a>
                                            <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">Best Regars.</p>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>

                                <!-- END MAIN CONTENT AREA -->
                                </table>
                                <!-- END CENTERED WHITE CONTAINER -->

                                <!-- START FOOTER -->
                                <div class=""footer"" style=""clear: both; margin-top: 10px; text-align: center; width: 100%;"">
                                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;"" width=""100%"">
                                    <tr>
                                      <td class=""content-block"" style=""font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; color: #999999; font-size: 12px; text-align: center;"" valign=""top"" align=""center"">
                                        <span class=""apple-link"" style=""color: #999999; font-size: 12px; text-align: center;"">Company Inc, Street St., Indonesia 55555</span>
                                        <br> Please Don't Reply to this email.
                                      </td>
                                    </tr>
                                    <tr>
                                      <td class=""content-block powered-by"" style=""font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; color: #999999; font-size: 12px; text-align: center;"" valign=""top"" align=""center"">
                                        Powered by <a href=""http://htmlemail.io"" style=""color: #999999; font-size: 12px; text-align: center; text-decoration: none;"">HTMLemail</a>.
                                      </td>
                                    </tr>
                                  </table>
                                </div>
                                <!-- END FOOTER -->

                              </div>
                            </td>
                            <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top;"" valign=""top"">&nbsp;</td>
                          </tr>
                        </table>
                      </body>
                    </html>";

                _emailHandler.Send("Employee New Account", body, register.Email); //mengirimkan otp ke email

                return Ok(new ResponseOKHandler<RegisterAccountResponseDto>(register, "Account Creation Success")); //mengembalikan message register success
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Register employee", ex.Message)); //mengembalikan error jika account creation fail
            }
        }

        [HttpGet("activate/{parameter}")]
        [AllowAnonymous]
        public IActionResult Activate(string parameter)
        {
            var query = HttpContext.Request.Query["otp"].ToString();
            int otp;
            int.TryParse(DecoderHandler.Base64Decode(query), out otp);

            var employee = _employeeRepository.GetByEmail(parameter); //mengambil data employee berdasar email
            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("data not found"));
            }

            var account = _accountsRepository.GetByGuid(employee.Guid); //mengambil data accoun berdasar employee guid
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("account not found"));
            }

            if (otp != account.Otp) //mengecek kecocokan otp
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "OTP does not match"));
            }
            if (account.IsUsed == true) //mengecek otp sudah digunakan atau belum
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "This OTP has been used"));
            }
            if (DateTime.Now > account.ExpiredTime) //mengecek apakah otp sudah expired
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "This OTP has been Expired"));
            }

            var toUpdate = new Accounts();
            toUpdate.Guid = account.Guid;
            toUpdate.Password = account.Password; //melakukan inject password baru
            toUpdate.Otp = account.Otp;
            toUpdate.IsUsed = true;
            toUpdate.IsActivated = true;
            toUpdate.ExpiredTime = account.ExpiredTime;
            toUpdate.CreatedDate = account.CreatedDate;
            toUpdate.ModifiedDate = DateTime.Now;
            _accountsRepository.Update(toUpdate); //melakukan update accounts

            // now redirect
            string url = "https://localhost:7057/Auth/LoginPage";

            return Redirect(url);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(AccountLoginDto accountLogin)
        {
            var employee = _accountsRepository.Login(accountLogin.Email); //mengambil data employee berdasar email
            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("Account or Password is invalid")); //response jika employee tidak ditemukan
            }

            var account = _accountsRepository.GetByGuid(employee.Guid); //mengambil data account berdasar employee guid
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("Account does not Valid")); //response jika account tidak ditemukan
            }

            if (!account.IsActivated)
            {
                return BadRequest(new ResponseBadRequestHandler("Login Error", "Account is not activated, please activate it first")); //validasi jika akun belum aktif
            }

            var isValid = HashHandler.ValidatePassword(accountLogin.Password, account.Password); //melakukan validasi password yang diinput dengan yang di database
            if (!isValid) //jika false
            {
                return BadRequest(new ResponseBadRequestHandler("Login Error", "Account or Password is invalid")); //response jika password salah
            }

            //Token Handler
            var payload = new List<Claim>();
            payload.Add(new Claim("Email", employee.Email));
            payload.Add(new Claim("FullName", string.Concat(employee.FirstName, " ", employee.LastName)));

            var getRoleName = from ar in _accountRoleRepository.GetAll()
                              join r in _rolesRepository.GetAll() on ar.RoleGuid equals r.Guid
                              where ar.AccountGuid == account.Guid
                              select r.Name;

            foreach (var roleName in getRoleName)
            {
                payload.Add(new Claim(ClaimTypes.Role, roleName));
            }


            var token = _tokenHandler.GenerateToken(payload);

            return Ok(new ResponseOKHandler<object>(new { Token = token }, "User successfully Logged in")); //respponse ketika user berhasil login
        }

        //Logic untuk Get Accounts
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountsRepository.GetAll(); //mengambil semua data Accounts
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var data = result.Select(x => (AccountsDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<AccountsDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get Accounts/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountsRepository.GetByGuid(guid); //mengambil data Accounts By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            return Ok(new ResponseOKHandler<AccountsDto>((AccountsDto)result, "Data retrieve Successfully"));
        }


        //Logic untuk PUT Accounts
        [HttpPut]
        public IActionResult Update(AccountsDto accountsDto)
        {
            try
            {
                var entity = _accountsRepository.GetByGuid(accountsDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data not found"));
                }

                Accounts toUpdate = accountsDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                toUpdate.ModifiedDate = DateTime.Now;
                toUpdate.Password = HashHandler.HashPassword(accountsDto.Password);

                _accountsRepository.Update(toUpdate); //melakukan update Accounts

                return Ok(new ResponseOKHandler<AccountsDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Accounts
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var accounts = _accountsRepository.GetByGuid(guid); //mengambil accounts by GUID
                if (accounts is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data not found"));
                }

                _accountsRepository.Delete(accounts); //melakukan Delete Accounts

                return Ok(new ResponseOKHandler<AccountsDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
