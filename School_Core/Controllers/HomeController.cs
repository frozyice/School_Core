using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School_Core.ViewModels;
using School_Core.ViewModels.Home;

namespace School_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeViewModel.IProvider _provider;

        public HomeController(ILogger<HomeController> logger, HomeViewModel.IProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        public IActionResult Index()
        {
            return View(_provider.Provide());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//todo:✔