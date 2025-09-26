using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisasterRelief.Data;
using DisasterRelief.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterRelief.Controllers
{
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var donations = await _context.Donations
                .Include(d => d.Donor)
                .ToListAsync();
            return View(donations);
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var donation = await _context.Donations
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (donation == null) return NotFound();

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    TempData["Error"] = "You must be logged in to make a donation.";
                    return RedirectToAction("Login", "Account");
                }

                donation.DonorId = user.Id;
                donation.Donor = user;

                _context.Add(donation);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Donation added successfully!";
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
            TempData["Error"] = "Failed to add donation. Errors: " + string.Join(" | ", errors);

            return View(donation);
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var donation = await _context.Donations.FindAsync(id);
            if (donation == null) return NotFound();

            return View(donation);
        }

        // POST: Donations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Donation donation)
        {
            if (id != donation.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Donation updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Donations.Any(e => e.Id == donation.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var donation = await _context.Donations
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (donation == null) return NotFound();

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Donation deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
