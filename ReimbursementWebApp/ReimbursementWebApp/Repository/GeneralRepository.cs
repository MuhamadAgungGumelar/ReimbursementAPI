﻿using Newtonsoft.Json;
using ReimbursementAPI.Utilities.Handler;
using ReimbursementWebApp.Contract;
using System.Text;

namespace ReimbursementWebApp.Repository
{
    public class GeneralRepository<Entity, TId> : IRepository<Entity, TId>
                where Entity : class
    {
        protected readonly string request;
        private readonly HttpContextAccessor contextAccessor;
        protected HttpClient httpClient;

        //constructor
        public GeneralRepository(string request)
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


        public async Task<ResponseOKHandler<IEnumerable<Entity>>> Get()
        {
            ResponseOKHandler<IEnumerable<Entity>> entityVM = null;
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<Entity>>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOKHandler<Entity>> Get(TId id)
        {
            ResponseOKHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOKHandler<Entity>> Post(Entity entity)
        {
            ResponseOKHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOKHandler<Entity>> Put(TId id, Entity entity)
        {
            ResponseOKHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync(request, content)) // Menggunakan HTTP PUT
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }
        public async Task<ResponseOKHandler<Entity>> Delete(TId id)
        {
            ResponseOKHandler<Entity> entityVM = null;
            using (var response = httpClient.DeleteAsync(request + id).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }
    }
}
