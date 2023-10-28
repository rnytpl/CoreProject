using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace CoreProject.Controllers
{
    public class LoginController : Controller
    {
        Context db = new Context();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Giris(Admin a)
        {
            var bilgiler = db.Admins.FirstOrDefault(x => x.Kullanici == a.Kullanici && x.Sifre == a.Sifre);

            if (bilgiler != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, a.Kullanici)
                };

                var userIdentity = new ClaimsIdentity(claims, "Login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);

                return RedirectToAction("PersonelGetir", "Personel");
            }

            return View();
        }

    }
}
