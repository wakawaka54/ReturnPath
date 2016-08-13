using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RP_Frontend.Services
{
    public class ApiService : IApiService
    {
        string apiAddress = "http://localhost:1479";

        public async Task<HttpResponseMessage> Get(string request)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiAddress);

                response = await client.GetAsync(request);
            }

            return response;
        }

        public async Task<HttpResponseMessage> Post(string request, StringContent content)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiAddress);

                response = await client.PostAsync(request, content);
            }

            return response;
        }
    }
}
