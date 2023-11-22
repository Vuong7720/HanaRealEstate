using Hana.Helpers;
using Hana.Interfaces;
using Hana.Models.DataModels;
using Hana.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Hana.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class AboutUsController : Controller
    {
        private readonly IAboutUsServices _services;
        public AboutUsController(IAboutUsServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            var list = _services.GetListAboutUs();
            return View(list);
        }

        /// <summary>
        /// dung cho pagin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LoadData(int pageIndex)
        {
            var source = _services.GetData();
            var count = await source.CountAsync();

            // dua vao pageIndex de get ra tu source bao nhieu item
            // o day mac dinh lay 5 item 1 trang
            var items = await source.Skip((pageIndex - 1) * 5).Take(5).ToListAsync();

            return Json(new { data = items, totalRow = count });
        }

        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new AboutUs());
            }
            else
            {
                var about = _services.GetDetails(id);
                if (about == null)
                {
                    return NotFound();
                }
                return View(about);
            }

        }

        [HttpPost]
        public IActionResult CreateOrEdit(int id, [Bind("Id,Content")]AboutUs about)
        {
            if (ModelState.IsValid)
            {
                //insert
                if (id == 0)
                {
                    _services.Create(about);
                }

                //update
                else
                {
                    try
                    {
                        _services.UpdateAboutUs(about);
                    }
                    catch (Exception)
                    {
                        if (!_services.IsExist(about.Id))
                        {
                            return NotFound();
                        }
                        else throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllAboutUs", _services.GetListAboutUs()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEdit", about) });
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            _services.DeleteAboutUs(id);
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllAboutUs", _services.GetListAboutUs()) });
        }


    }
}