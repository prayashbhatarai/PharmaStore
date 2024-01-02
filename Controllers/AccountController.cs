using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmaStore.Data.Identity;
using PharmaStore.Modules.Dtos;

namespace PharmaStore.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Customer"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (roles.Contains("Admin"))
                {
                    return LocalRedirect("/admin");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto, string? returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, loginDto.RememberMe, true);
                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByNameAsync(loginDto.Username);
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Customer"))
                        {
                            //if (!String.IsNullOrEmpty(returnUrl))
                            //{
                            //    return LocalRedirect(returnUrl);
                            //}
                            return RedirectToAction("index", "Home");
                        }
                        if (roles.Contains("Admin"))
                        {
                            if (!String.IsNullOrEmpty(returnUrl))
                            {
                                if(returnUrl == "/")
                                {
                                    return LocalRedirect("/admin");
                                }
                                //return LocalRedirect(returnUrl);
                            }
                            return LocalRedirect("/admin");
                        }
                    }
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "User account locked out.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError("", "User is not allowed.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username & Password.");
                    }
                    ViewData["ReturnUrl"] = returnUrl;
                    return View(loginDto);
                }
                else
                {
                    ViewData["ReturnUrl"] = returnUrl;
                    return View(loginDto);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                ViewData["ReturnUrl"] = returnUrl;
                ModelState.AddModelError("", "Error Occured : " + ex.Message);
                return View(loginDto);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult AccessDenied(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }

    }
}
