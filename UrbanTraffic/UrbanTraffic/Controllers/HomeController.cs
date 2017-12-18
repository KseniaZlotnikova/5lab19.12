using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrbanTraffic.Models;
using Microsoft.AspNetCore.Identity;
using UrbanTraffic.ViewModel;

namespace UrbanTraffic.Controllers
{
    public class HomeController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;

        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }



        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";

            if (await _roleManager.FindByNameAsync("admin") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await _roleManager.FindByNameAsync("user") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("user"));
            }


            ApplicationUser user = new ApplicationUser { Email = adminEmail, UserName = adminEmail, Year = 1998 };
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "admin");


            return View();
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


    }
}
