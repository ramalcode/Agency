using AgencyTemplate.DAL;
using AgencyTemplate.Models;
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
            ICollection<Position> positions = _context.Positions.Skip((page - 1) * 3).Take(3).ToList();

            PaginationVM<Position> paginationVM = new PaginationVM<Position>
            {
                MaxPageCount = (int)Math.Ceiling((decimal)_context.Positions.Count()/3),
                CurrentPage = page,
                Items = positions
            };
            //ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.Positions.Count() / 3);
            //ViewBag.CurrentPage=page;
            return View(paginationVM);
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
