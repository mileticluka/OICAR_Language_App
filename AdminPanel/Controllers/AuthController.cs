using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;
using DAL.Interfaces;
using DAL.DTO;
using Microsoft.AspNetCore.Authorization;

namespace AdminPanel.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAdminUserRepository userRepository;
        private readonly IMapper mapper;

        public AuthController(IAdminUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var user = userRepository.GetUser(login.Username);

            if (user == null || !userRepository.Authenticate(login))
            {
                ModelState.AddModelError("Username", "Invalid username or password");
                return View(login);
            }

            var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.Username)};

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties
            {
                IsPersistent = false
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties).Wait();

            return Redirect("/Home/");
        }
        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        }
    }
}
