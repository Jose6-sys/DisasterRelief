using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisasterRelief.Data;
using DisasterRelief.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterRelief.Controllers
{
    public class IncidentReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IncidentReportsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET IncidentReports
        public async Task<IActionResult> Index()
        {
            var reports = await _context.IncidentReports
                                        .Include(r => r.Reporter) //  load reporter info
                                        .ToListAsync();
            return View(reports);
        }

        // GET IncidentReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var report = await _context.IncidentReports
                                       .Include(r => r.Reporter) // show reporter in details
                                       .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null) return NotFound();

            return View(report);
        }

        // GET IncidentReports/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentReport report)
        {
            // Remove validation errors for Reporter/ReporterId since they're set by code
            ModelState.Remove("ReporterId");
            ModelState.Remove("Reporter");

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    TempData["Error"] = "You must be logged in to create an incident report.";
                    return RedirectToAction(nameof(Index));
                }

                report.ReporterId = user.Id;
                report.Reporter = user;
                report.CreatedAt = DateTime.UtcNow;

                _context.Add(report);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Incident report created successfully!";
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            TempData["Error"] = "Failed to create incident report: " + string.Join(" | ", errors);

            return View(report);
        }

        // GET IncidentReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var report = await _context.IncidentReports.FindAsync(id);
            if (report == null) return NotFound();

            return View(report);
        }

        // POST IncidentReports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Location,DateOccurred,DateReported,Severity,Status")] IncidentReport report)
        {
            if (id != report.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.IncidentReports.Any(e => e.Id == report.Id))
                        return NotFound();
                    else
                        throw;
                }
                TempData["Success"] = "Incident report updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Failed to update incident report.";
            return View(report);
        }

        // GET IncidentReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var report = await _context.IncidentReports
                                       .Include(r => r.Reporter)
                                       .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null) return NotFound();

            return View(report);
        }

        // POST IncidentReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.IncidentReports.FindAsync(id);
            if (report != null)
            {
                _context.IncidentReports.Remove(report);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Incident report deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Incident report not found.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
