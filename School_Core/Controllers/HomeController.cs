using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using School_Core.ViewModels;
using School_Core.ViewModels.Home;

namespace School_Core.Controllers
{
    public class HomeController : Controller // see on chart.js kasutamiseks, hetkel kontrolleri nimi vajaks muutmist ja chart üldisesse komponenti tõstmist 
    {
        private readonly HomeViewModel.IProvider _provider;

        public HomeController(HomeViewModel.IProvider provider)
        {
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