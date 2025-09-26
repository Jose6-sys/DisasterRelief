using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisasterRelief.Data;

namespace DisasterRelief.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stats = new
            {
                Volunteers = await _context.Volunteers.CountAsync(),
                Tasks = await _context.VolunteerTasks.CountAsync(),
                Incidents = await _context.IncidentReports.CountAsync(),
                Donations = await _context.Donations.CountAsync()
            };

            return View(stats);
        }
    }
}
