using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;

namespace RP_Frontend.Services
{
    public interface IApiService
    {
        Task<HttpResponseMessage> Get(string request);
        Task<HttpResponseMessage> Post(string request, StringContent content);
    }
}
