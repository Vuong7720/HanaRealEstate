//using Hana.Helpers;
//using Hana.Models.ViewModels;
//using Hana.Services;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using Twilio.Rest.Verify.V2.Service;

//namespace Hana.Controllers
//{

//    public class AccountController : Controller
//    {
//        private readonly IAccountServices _services;
//        private readonly IVerification _phoneServices;
//        public AccountController(IAccountServices services, IVerification phoneServices)
//        {
//            _services = services;
//            _phoneServices = phoneServices;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }
//        public IActionResult Login(string returnUrl = "")
//        {
//            var model = new VM_Login { ReturnUrl = returnUrl };
//            return View(model);
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        public async Task<IActionResult> Login(VM_Login account, string returnUrl)
//        {
//            if (ModelState.IsValid)
//            {
//                var member = _services.GetUser(account);
//                if (member != null)
//                {
//                    var userPrincipal = Helper.GenerateIdentity(member);

//                    var props = new AuthenticationProperties
//                    {
//                        IsPersistent = account.IsRememberMe
//                    };

//                    //sign in
//                    await HttpContext.SignInAsync(
//                        scheme: "MyCookieScheme",
//                        principal: userPrincipal,
//                        properties: props
//                        );

//                    if (!string.IsNullOrEmpty(returnUrl)
//                        && Url.IsLocalUrl(returnUrl))
//                        return Redirect(returnUrl);
//                    else
//                        return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    ViewBag.ErrorMessage = "Invalid user or password!";
//                }
//            }
//            return View(account);
//        }

//        [Route("/xac-minh")]
//        public IActionResult Register(string returnUrl = "")
//        {
//            var model = new VM_Register { ReturnUrl = returnUrl };
//            return View(model);
//        }

//        [HttpPost]
//        [AllowAnonymous]
        
//        public async Task<IActionResult> Register(VM_Register registerAccount)
//        {
//            if (ModelState.IsValid)
//            {
//                var isSuccess = await _services.RegisterUser(registerAccount);
//                if (isSuccess)
//                {
//                    try
//                    {
//                        var verification = await VerificationResource.CreateAsync(
//                            to: registerAccount.PhoneNumber,
//                            channel: "sms",
//                            pathServiceSid: "XXX"
//                        );
//                        if (verification.Status == "pending")
//                        {
//                            return RedirectToAction("ConfirmPhone", "Account");
//                        }
//                    }
//                    catch (System.Exception ex)
//                    {
//                        ViewBag.ErrorMessage = ex.Message;
//                        return View(registerAccount);
//                    }
//                    //await _phoneServices.StartVerificationAsync(registerAccount.PhoneNumber);

//                    return RedirectToAction("Index", "Home");
//                }
//            }
//            return View(registerAccount);
//        }

//        public async Task<IActionResult> ConfirmPhone()
//        {
//            return View();
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        public async Task<IActionResult> ConfirmPhone(string ConfirmCode)
//        {
//            try
//            {
//                var verification = await VerificationCheckResource.CreateAsync(
//                  to: "+84949891806",
//                  code: "439294",
//                  pathServiceSid: "XXX"
//              );

//                if (verification.Status == "approved")
//                {
//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    return View(ConfirmCode);
//                }
//            }
//            catch (System.Exception ex)
//            {
//                ViewBag.ErrorMessage = ex.Message;
//                return View(ConfirmCode);
//            }
//        }

//        public IActionResult Denied()
//        {
//            return View();
//        }

//        public async Task<IActionResult> Logout()
//        {
//            await HttpContext.SignOutAsync(
//            scheme: "MyCookieScheme"
//            );

//            return RedirectToAction("Index", "Home");
//        }
//    }
//}