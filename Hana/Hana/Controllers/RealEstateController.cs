using Commom;
using Hana.Helpers;
using Hana.Interfaces;
using Hana.Models.DataModels;
using Hana.Models.ViewModels;
using Hana.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Hana.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly IRealEstateServices _realEstateServices;
        private readonly IFileServices _fileServices;
        private readonly ICommonServices _commonServices;

        public RealEstateController(IRealEstateServices realEstateServices,
                                    IFileServices fileServices,
                                    ICommonServices commonServices)
        {
            _realEstateServices = realEstateServices;
            _fileServices = fileServices;
            _commonServices = commonServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? page, int? type, int? city, int? district, int? priceRange, int? acreageRange, string? searchString)
        {
            int pageSize = 5;

            var condition = new Condition
            {
                Type = type ?? 0,
                City = city ?? 0,
                District = district ?? 0,
                PriceRange = priceRange ?? 0,
                AcreageRange = acreageRange ?? 0,
                SearchString = searchString ?? string.Empty
            };

            var source = _realEstateServices.Filter(condition);

            var types = _realEstateServices.GetRealEstateTypeList();
            types = types.Concat(new[] { new RealEstateType { Id = 0, RealEstateTypeName = "Tất cả" } }).OrderBy(t => t.Id);

            var cities = _realEstateServices.GetCityList();
            cities = cities.Concat(new[] { new City { Id = 0, CityName = "Tất cả" } }).OrderBy(c => c.Id);

            var districts = await _commonServices.GetDistrictsByCity(city);
            districts = districts.Concat(new[] { new District { Id = 0, DistrictName = "Tất cả" } }).OrderBy(d => d.Id);

            var priceRanges = Helper.GetPriceRangeForView();
            var acreageRanges = Helper.GetAcreageRangeForView();

            ViewData["Types"] = new SelectList(types, "Id", "RealEstateTypeName", condition.Type);
            ViewData["Cities"] = new SelectList(cities, "Id", "CityName", condition.City);
            ViewData["Districts"] = new SelectList(districts, "Id", "DistrictName", condition.District);
            ViewData["PriceRanges"] = new SelectList(priceRanges, "Value", "Key", condition.PriceRange);
            ViewData["AcreageRanges"] = new SelectList(acreageRanges, "Value", "Key", condition.AcreageRange);

            var result = await CustomPagination.CreateAsync(source, condition, page ?? 1, pageSize);

            return View(result);
        }

        [AllowAnonymous]
        [Route("thong-tin-chi-tiet")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _realEstateServices.GetRealEstateDetails(id);
            if (details == null)
            {
                return NotFound();
            }
            else
            {
                //get pictures
                var pictures = await _fileServices.GetPicturesForRealEstate(details.Id);
                details.ImageUrls = Helper.GetImageUrls(pictures);

                //get recommend list
                var recommendList = _realEstateServices.GetRecommendList(id);
                details.RecommmendList = recommendList;

                //get District list
                var districtList = _realEstateServices.GetDistrictList();
                ViewData["DistrictList"] = districtList;

                //get ReealEstate list
                var realEstateList = _realEstateServices.GetRealEstateTypeList();
                ViewData["RealEstateList"] = realEstateList;

            }
            var realEstateTypeList = _realEstateServices.GetRealEstateTypeList();
            ViewData["GOOGLE_MAP_API"] = Constants.GOOGLE_MAP_MARKER_API;
            return View(details);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistricts(int? cityId, int? currentDistrictId)
        {
            if (cityId == null)
            {
                return Json(new { status = false, messsage = "Không tìm thấy thành phố!" });
            }
            else
            {
                var districts = await _commonServices.GetDistrictsByCity(cityId);
                districts = districts.Concat(new[] { new District { Id = 0, DistrictName = "Tất cả" } });
                districts = districts.OrderBy(d => d.Id);
                return Json(new { status = true, data = districts, current = currentDistrictId ?? 0, message = string.Format("Get district list by city id {0}", cityId) });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetAllActiveLocation()
        {
            var data = _realEstateServices.GetAllActiveLocation();
            return Json(new { data });
        }

        
    }
}