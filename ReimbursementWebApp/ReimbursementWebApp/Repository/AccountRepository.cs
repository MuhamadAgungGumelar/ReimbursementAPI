using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using ReimbursementAPI.DTO.Account;
using ReimbursementAPI.Utilities.Handler;
using ReimbursementWebApp.Models;
using ReimbursementWebApp.Contract;

namespace ReimbursementWebApp.Repository
{
    public class AccountRepository : GeneralRepository<AccountsDto, Guid>, IAccountRepository
    {
        public AccountRepository(string request = "Accounts/") : base(request)
        {

        }
        public async Task<ResponseOKHandler<TokenDto>> Login(AccountLoginDto login)
        {
            string jsonEntity = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{request}Login", content))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                var entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<TokenDto>>(apiResponse);
                return entityVM;
            }
        }
    }
}
