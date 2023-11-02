using Newtonsoft.Json;
using ReimbursementAPI.DTO.Reimbursement;
using ReimbursementAPI.Models;
using ReimbursementAPI.Utilities.Handler;
using ReimbursementWebApp.Contract;
using System.Net.Http;
using System.Text;

namespace ReimbursementWebApp.Repository
{
    public class ReimbursementRepository : IReimbursementRepository
    {
        protected readonly string request;
        private readonly HttpContextAccessor contextAccessor;
        protected HttpClient httpClient;

        //constructor
        public ReimbursementRepository(string request = "Reimbursement")
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7257/api/")
            };
            //contextAccessor = new HttpContextAccessor();
            // Ini yg bawah skip dulu
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
        }

        public async Task<ResponseOKHandler<NewReimbursementsDto>> Post(NewReimbursementsDto entity)
        {
            ResponseOKHandler<NewReimbursementsDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<NewReimbursementsDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
