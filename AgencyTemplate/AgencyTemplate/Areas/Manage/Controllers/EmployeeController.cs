using AgencyTemplate.DAL;
using AgencyTemplate.Models;
using AgencyTemplate.Utilies;
using AgencyTemplate.Utilies.Enums;
using AgencyTemplate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgencyTemplate.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class EmployeeController : Controller
    {
        AppDBContext _context;
        IWebHostEnvironment _env;

        public EmployeeController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Employees.Include(p => p.Position));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            if (employeeVM.Image != null)
            {
                string result = employeeVM.Image.CheckValidate("image/", 500);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result);
                }
            }
            if (!_context.Positions.Any(p => p.Id == employeeVM.PositionId))
            {
                ModelState.AddModelError("PositionId", "Bele vezife movcud deyil");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                return View();

            }

            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                PositionId = employeeVM.PositionId,
                Twitter = employeeVM.Twitter,
                Facebook = employeeVM.Facebook,
                Linkedn = employeeVM.Linkedn,
                Salary = employeeVM.Salary,
                ImageUrl = employeeVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "assets" ,"img" ,"team")),
            };

            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            employee.ImageUrl.DeleteFile(_env.WebRootPath, "assets/img/team");
             _context.Employees.Remove(employee);
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async  Task<IActionResult> Update(int? id)
        {
            if (id is null) return NotFound();
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Name = employee.Name,
                PositionId = employee.PositionId,
                Twitter = employee.Twitter,
                Facebook = employee.Facebook,
                Linkedn = employee.Linkedn,
                Salary = employee.Salary,
                ImageUrl = employee.ImageUrl
            };

            ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            return View(employeeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateEmployeeVM employeeVM)
        {
            if (id is null || id!=employeeVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            if (employeeVM.Image != null)
            {
                string result = employeeVM.Image.CheckValidate("image/", 500);
                if (result.Length > 0)
                {
                    ModelState.AddModelError("Image", result); return View();
                }
                    employee.ImageUrl.DeleteFile(_env.WebRootPath, "assets/img/team");
                    employee.ImageUrl = employeeVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "assets/img/team"));
            }
            if (!_context.Positions.Any(p => p.Id == employeeVM.PositionId))
            {
                ModelState.AddModelError("PositionId", "Bele vezife movcud deyil");
            }
            employee.Name = employeeVM.Name;
            employee.Salary = employeeVM.Salary;
            employee.Linkedn = employeeVM.Linkedn;
            employee.Facebook = employeeVM.Facebook;
            employee.Twitter = employeeVM.Twitter;
            employee.PositionId = employeeVM.PositionId;
             await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }
    }
}
