using CrimeReportingSystem.Data;
using CrimeReportingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrimeReportingSystem.Controllers
{
    public class CrimeReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CrimeReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Crime Report Form
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Submit Crime Report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrimeReport crimeReport)
        {
            if (ModelState.IsValid)
            {
                crimeReport.Status = "Pending";
                crimeReport.CreatedAt = DateTime.Now;

                _context.CrimeReports.Add(crimeReport);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] =
                    "Crime report submitted successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View("Index", crimeReport);
        }

        // Admin Dashboard
        [HttpGet]
        public async Task<IActionResult> Admin()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var reports = await _context.CrimeReports
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return View(reports);
        }

        // View Report Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var report = await _context.CrimeReports
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // Delete Confirmation
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var report = await _context.CrimeReports
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // Delete Report
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            var report = await _context.CrimeReports
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report != null)
            {
                _context.CrimeReports.Remove(report);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Admin));
        }
    }
}