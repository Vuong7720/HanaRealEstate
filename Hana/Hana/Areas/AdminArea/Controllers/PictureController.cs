using Hana.Interfaces;
using Hana.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Hana.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize]
    public class PictureController : Controller
    {
        private readonly IFileServices _services;
        public PictureController(IFileServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoadData(int realEstateId)
        {
            var pictures = await _services.GetPicturesForRealEstate(realEstateId);

            if (pictures != null)
            {
                return Json(new
                {
                    count = pictures.Count(),
                    data = pictures,
                    status = true
                });
            }
            else return Json(new { status = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int pictureId)
        {
            var status = await _services.RemovePictureFromRealEstate(pictureId);
            return Json(new { status });
        }

    }
}