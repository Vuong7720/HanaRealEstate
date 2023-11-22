using Hana.Interfaces;
using Hana.Models;
using Hana.Models.DataModels;
using Hana.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;


namespace Hana.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRealEstateServices _services;
        public HomeController(ILogger<HomeController> logger, IRealEstateServices services)
        {
            _logger = logger;
            _services = services;
        }

        public IActionResult Index()
        {
            var cityList = _services.GetCityList();
            cityList = cityList.Concat(new[] { new City { Id = 0, CityName = "Chọn thành phố" } });
            cityList = cityList.OrderBy(c => c.Id);

            var typeList = _services.GetRealEstateTypeList();
            typeList = typeList.Concat(new[] { new RealEstateType { Id = 0, RealEstateTypeName = "Loại phòng" } });
            typeList = typeList.OrderBy(t => t.Id);

            ViewData["Cities"] = new SelectList(cityList, "Id", "CityName");
            ViewData["Types"] = new SelectList(typeList, "Id", "RealEstateTypeName");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
