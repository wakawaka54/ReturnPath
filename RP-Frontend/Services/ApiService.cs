using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RP_Frontend.Services
{
    public class ApiService : IApiService
    {
        public static string ApiAddress = "http://*:1479/api/sentences";

        public async Task<HttpResponseMessage> Get(string request)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiAddress);

                response = await client.GetAsync(request);
            }

            return response;
        }

        public async Task<HttpResponseMessage> Post(string request, StringContent content)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiAddress);

                response = await client.PostAsync(request, content);
            }

            return response;
        }
    }
}
