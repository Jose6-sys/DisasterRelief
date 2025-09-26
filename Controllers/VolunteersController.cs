using DisasterRelief.Data;
using DisasterRelief.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterRelief.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Volunteers
        public async Task<IActionResult> Index()
        {
            var volunteers = await _context.Volunteers.ToListAsync();
            return View(volunteers);
        }

        // GET: Volunteers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var volunteer = await _context.Volunteers
                .FirstOrDefaultAsync(v => v.Id == id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // GET: Volunteers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Volunteers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volunteer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Volunteer added successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Failed to add volunteer. Please check input.";
            return View(volunteer);
        }

        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Volunteer volunteer)
        {
            if (id != volunteer.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteer);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Volunteer updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Volunteers.Any(v => v.Id == volunteer.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(volunteer);
        }

        // GET: Volunteers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var volunteer = await _context.Volunteers
                .FirstOrDefaultAsync(v => v.Id == id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Volunteer deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Volunteer not found!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
