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
    public class PolicyController : Controller
    {
        private readonly IPolicyServices _services;
        public PolicyController(IPolicyServices services)
        {
            _services = services;
        }
        public IActionResult Index()
        {
            var list = _services.GetListPolicies();
            return View(list);
        }

        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Policy());
            }
            else
            {
                var policy = _services.GetDetails(id);
                if (policy == null)
                {
                    return NotFound();
                }
                return View(policy);
            }

        }

        [HttpPost]
        public IActionResult CreateOrEdit(int id, [Bind("Id,PolicyContent")]Policy policy)
        {
            if (ModelState.IsValid)
            {
                //insert
                if (id == 0)
                {
                    _services.Create(policy);
                }

                //update
                else
                {
                    try
                    {
                        _services.UpdatePolicy(policy);
                    }
                    catch (Exception)
                    {
                        if (!_services.IsExist(policy.Id))
                        {
                            return NotFound();
                        }
                        else throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllPolicies", _services.GetListPolicies()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEdit", policy) });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            _services.DeletePolicy(id);
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllPolicies", _services.GetListPolicies()) });
        }

    }
}