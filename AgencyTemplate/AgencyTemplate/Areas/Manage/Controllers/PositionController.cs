using AgencyTemplate.DAL;
using AgencyTemplate.Models;
using AgencyTemplate.Utilies.Enums;
using AgencyTemplate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyTemplate.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]

    public class PositionController : Controller
    {
        AppDBContext _context;

        public PositionController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.Positions.Count() / 2);
            ViewBag.CurrentPage=page;
            return View(_context.Positions.Skip((page-1)*2).Take(2).ToList());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVM positionVM)
        {
            if (!ModelState.IsValid) return View(positionVM);
            if (positionVM is null) return NotFound();
            Position position = new Position
            {
                Name = positionVM.Name,
            };
            await _context.Positions.AddAsync(position);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            Position position = await _context.Positions.FindAsync(id);
            if (position is null) return NotFound();
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return NotFound();
            Position position = await _context.Positions.FindAsync(id);
            if (position is null) return NotFound();
            UpdatePositionVM vM = new UpdatePositionVM
            {
                Name = position.Name,
            };

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdatePositionVM updatePosition)
        {
            if (id is null || id!=updatePosition.Id) return NotFound();
            Position position = await _context.Positions.FindAsync(id);
            if (position is null) return NotFound();
            position.Name = updatePosition.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
