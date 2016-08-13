using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using RP_Frontend.Services;

namespace RP_Frontend.Controllers
{
    public class HomeController : Controller
    {
        IApiService api;

        public HomeController(IApiService _api)
        {
            api = _api;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
