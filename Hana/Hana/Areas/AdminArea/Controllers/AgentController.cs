using Hana.Helpers;
using Hana.Interfaces;
using Hana.Models.DataModels;
using Hana.Models.ViewModels;
using Hana.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hana.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentServices _services;
        public AgentController(IAgentServices services)
        {
            _services = services;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var agents = await _services.GetListAgent();
            return View(agents);
        }

        public async Task<IActionResult> Details(int id)
        {
            var agent = await _services.GetDetails(id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        [HttpGet]
        [Route("thong-tin-ca-nhan")]
        public IActionResult UpdateProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId").Value ?? string.Empty;

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var userInfo = _services.GetUserInfo(Convert.ToInt32(userId));

            ViewBag.UpdatePassStatus = TempData["UpdatePassStatus"];

            return View(userInfo);
        }
        //----------------------------------------
        [NoDirectAccess]
        public async Task<IActionResult> ChangeRoleLV(int id)
        {
            var agent = await _services.GetDetails(id);

            if (agent == null)
            {
                return NotFound();
            }

            var levels = await _services.GetLevelList();
            ViewData["Levels"] = new SelectList(levels, "Id", "LevelName", agent.LevelId);

            var vmAgent = new VM_Agent
            {
                Id = agent.Id,
            };

            return View(vmAgent);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRoleLV(int id, [Bind("Id,LevelId")] Agent updatedAgent)
        {
                var success = await _services.UpdateLevel(id, updatedAgent.LevelId);

                if (success)
                {
                    TempData["ChangeRoleStatus"] = "Đổi chức vụ thành công!";
                    return RedirectToAction("Details", new { id });
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi đổi chức vụ.");
                }

            var agent = await _services.GetListAgent();
            ViewBag.Levels = new SelectList(await _services.GetLevelList(), "Id", "LevelName");

            return View(agent);
        }



        [HttpPost]
        [Route("thong-tin-ca-nhan")]
        public IActionResult UpdateProfile(VM_Agent updateProfile)
        {
            if (ModelState.IsValid)
            {
                var status = _services.UpdateProfile(updateProfile);

                return Json(new { status });
            }

            return View(updateProfile);
        }


        [HttpGet]
        [Route("doi-mat-khau")]
        public IActionResult UpdatePassword()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId").Value ?? string.Empty;

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var viewmodel = new VM_ChangePassword() { Id = Convert.ToInt32(userId) };
            return View(viewmodel);
        }

        [HttpPost]
        [Route("doi-mat-khau")]
        public IActionResult UpdatePassword(VM_ChangePassword data)
        {
            if (ModelState.IsValid)
            {
                int resultCode = _services.UpdatePassword(data);

                if (resultCode == 1)
                {
                    TempData["UpdatePassStatus"] = "Cập nhật mật khẩu thành công!";

                    return RedirectToAction("UpdateProfile");
                }
                else if (resultCode == 0)
                {
                    ViewBag.Message = "Mật khẩu cũ không chính xác!";
                    ViewBag.MesasageCode = 0;
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra, vui lòng thử lại!";
                    ViewBag.MesasageCode = -1;
                }
                return View(data);
            }

            return View(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Disable")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableConfirm(int id)
        {
            var isSuccess = await _services.Disable(id);
            return Json(new { isSuccess, html = Helper.RenderRazorViewToString(this, "_ViewAllAgents", await _services.GetListAgent()) });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Enable")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableConfirm(int id)
        {
            var isSuccess = await _services.Enable(id);
            return Json(new { isSuccess, html = Helper.RenderRazorViewToString(this, "_ViewAllAgents", await _services.GetListAgent()) });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var isSuccess = await _services.Delete(id);
            return Json(new { isSuccess, html = Helper.RenderRazorViewToString(this, "_ViewAllAgents", await _services.GetListAgent()) });
        }
    }
}