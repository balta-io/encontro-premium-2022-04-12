using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspNetIdentityTest02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentityTest02.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public async Task<IActionResult> Index(
        [FromServices]UserManager<IdentityUser<Guid>> userManager,
        [FromServices]RoleManager<IdentityRole<Guid>> roleManager)
    {
        var user = await userManager
            .FindByEmailAsync("");

        var roles = 
            await userManager.GetRolesAsync(user);
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
