using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Zink.MVC.Data;
using Zink.MVC.Enums;
using Zink.MVC.Models;
using Zink.MVC.ServiceModel.Request;
using Microsoft.AspNetCore.Authorization;

namespace Zink.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContext _httpContext;
        public AccountController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if(result == null)
            {
                ModelState.AddModelError("", "Hesab tapilmadi.");
                return View();
            }

            if(result.UserRoleId != (int)UserRoleEnum.Admin)
            {
                ModelState.AddModelError("", "Hesab tapilmadi.");
                return View();
            }

            if(request.Password == null)
            {
                ModelState.AddModelError("Password", "Sifre yalnisdir.");
                return View();
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(request.Password);
                var hash = SHA256.HashData(buffer);
                if (!result.Password.SequenceEqual(hash))
                {
                    ModelState.AddModelError("Password", "Sifre yalnisdir.");
                    return View();
                }
            };

           

            var claims = new List<Claim>
                {
                new Claim("Email", result.Email),
                new Claim("Name", result.Name),
                new Claim("Id",result.Id.ToString()),
                new Claim("RoleId",result.UserRoleId.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);


            var authProperties = new AuthenticationProperties
            {
                // expiry of the cookie 
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
            };

            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            _httpContext.Session.Remove("loginRequest");

            return RedirectToAction("index", "dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("login", "Account");
        }

        //public async Task<IActionResult> CeateAdmin()
        //{
        //User admin = new User
        //{
        //    Name = "Admin",
        //    Surname = "AdminSurname",
        //    Email = "admin@gmail.com",
        //    UserRoleId = (int)UserRoleEnum.Admin,
        //};

        //using (SHA256 sha256 = SHA256.Create())
        //{
        //    string password = "12345678";
        //    byte[] buffer = Encoding.UTF8.GetBytes(password);
        //    byte[] hashedPassword = sha256.ComputeHash(buffer);
        //    admin.Password = hashedPassword;
        //}

        //await _context.Users.AddAsync(admin);
        //await _context.SaveChangesAsync();

        //return Ok(admin);
        //User user = new User
        //{
        //    Name = "User",
        //    Surname = "UserSurname",
        //    Email = "user@gmail.com",
        //    UserRoleId = (int)UserRoleEnum.User,
        //};

        //using (SHA256 sha256 = SHA256.Create())
        //{
        //    string password = "12345678";
        //    byte[] buffer = Encoding.UTF8.GetBytes(password);
        //    byte[] hashedPassword = sha256.ComputeHash(buffer);
        //    user.Password = hashedPassword;
        //}

        //await _context.Users.AddAsync(user);
        //await _context.SaveChangesAsync();

        //return Ok(user);
        //}
    }
}
