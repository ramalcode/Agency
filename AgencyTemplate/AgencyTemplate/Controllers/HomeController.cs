using AgencyTemplate.DAL;
using AgencyTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AgencyTemplate.Controllers
{
    public class HomeController : Controller
    {
        AppDBContext dbContext;

        public HomeController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.Employees.Include(e=>e.Position).ToList());
        }

       
    }
}