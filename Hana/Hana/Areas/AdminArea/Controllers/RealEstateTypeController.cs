using Hana.Helpers;
using Hana.Interfaces;
using Hana.Models.DataModels;
using Hana.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hana.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin,Manager")]
    public class RealEstateTypeController : Controller
    {
        private readonly IRealEstateTypeServices _services;

        public RealEstateTypeController(IRealEstateTypeServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            var list = _services.GetList();
            return View(list);
        }


        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new RealEstateType());
            }
            else
            {
                var type = _services.GetDetails(id);
                if (type == null)
                {
                    return NotFound();
                }
                return View(type);
            }

        }

        [HttpPost]
        public IActionResult CreateOrEdit(int id, [Bind("Id,RealEstateTypeName")]RealEstateType type)
        {
            if (ModelState.IsValid)
            {
                //insert
                if (id == 0)
                {
                    _services.Create(type);
                }

                //update
                else
                {
                    try
                    {
                        _services.UpdateType(type);
                    }
                    catch (Exception)
                    {
                        if (!_services.IsExist(type.Id))
                        {
                            return NotFound();
                        }
                        else throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllTypes", _services.GetList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEdit", type) });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            _services.DeleteType(id);
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllTypes", _services.GetList()) });
        }

    }
}