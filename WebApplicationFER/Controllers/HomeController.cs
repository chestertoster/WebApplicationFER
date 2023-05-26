using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationFER.Models;
using Microsoft.EntityFrameworkCore;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    public class HomeController : Controller
    {
        NewDbPracContext db;
        public HomeController(NewDbPracContext context)
        {
            db = context;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult IndexR()
        {
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
}