using DisasterRelief.Data;
using DisasterRelief.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterRelief.Controllers
{
    public class VolunteerTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteerTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VolunteerTasks
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.VolunteerTasks.ToListAsync();
            return View(tasks);
        }

        // GET: VolunteerTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.VolunteerTasks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null) return NotFound();

            return View(task);
        }

        // GET: VolunteerTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VolunteerTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VolunteerTask task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Task created successfully!";
                return RedirectToAction(nameof(Index));
            }

            // 🔍 Show detailed validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            TempData["Error"] = "Failed to create task. Errors: " + string.Join(" | ", errors);

            return View(task);
        }

        // GET: VolunteerTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.VolunteerTasks.FindAsync(id);
            if (task == null) return NotFound();

            return View(task);
        }

        // POST: VolunteerTasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VolunteerTask task)
        {
            if (id != task.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.VolunteerTasks.Any(e => e.Id == task.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: VolunteerTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.VolunteerTasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null) return NotFound();

            return View(task);
        }

        // POST: VolunteerTasks/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.VolunteerTasks.FindAsync(id);
            if (task != null)
            {
                _context.VolunteerTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
