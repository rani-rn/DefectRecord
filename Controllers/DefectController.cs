using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using DefectRecord.Models;

namespace DefectRecord.Controllers
{
    public class DefectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DefectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Input()
        {
            ViewBag.UserRoles = await _context.UserRoles.ToListAsync();
            ViewBag.Defects = await _context.Defect.ToListAsync();
            ViewBag.LineProductions = await _context.LineProductions.ToListAsync();
            return View();
        }

        public IActionResult Record()
        {
            var defectReports = _context.DefectReports
                                .Include(d => d.Defect)
                                .Include(d => d.LineProduction)
                                .Include(d => d.Role)
                                .ToList();
            return View(defectReports);
        }

        [HttpPost]
        public async Task<IActionResult> InputDefect(DefectReport defectReport)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    defectReport.ReportDate = DateTime.Now;
                    _context.DefectReports.Add(defectReport);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Defect successfully recorded.";
                    return RedirectToAction("Input");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error saving defect: " + ex.Message;
                }
            }

            ViewBag.UserRoles = await _context.UserRoles.ToListAsync();
            ViewBag.Defects = await _context.Defect.ToListAsync();
            ViewBag.LineProductions = await _context.LineProductions.ToListAsync();

            return View("Input", defectReport);
        }

    }
}
