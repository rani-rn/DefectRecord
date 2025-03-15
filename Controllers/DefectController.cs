using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
            await LoadViewBagData(); // Load dropdown data
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Input( DefectReport defectReport)
        {
            Console.WriteLine("POST method triggered!");
            Console.WriteLine($"SerialNumber: {defectReport.SerialNumber}, Reporter: {defectReport.Reporter}");

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid!");

                foreach (var error in ModelState)
                {
                    foreach (var err in error.Value.Errors)
                    {
                        Console.WriteLine($"Field: {error.Key}, Error: {err.ErrorMessage}");
                    }
                }

                await LoadViewBagData();
                return View(defectReport);
            }

            var report = new DefectReport()
            {
                SerialNumber = defectReport.SerialNumber,
                Reporter = defectReport.Reporter,
                RoleId = defectReport.RoleId,
                DefectId = defectReport.DefectId,
                LineProductionId = defectReport.LineProductionId,
                Description = defectReport.Description,
                Status = defectReport.Status,
                ReportDate = DateTime.Now,
            };

            _context.DefectReports.Add(report);
            await _context.SaveChangesAsync();
            Console.WriteLine("Data saved successfully!");

            return RedirectToAction("Record", "Defect");
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
public async Task<IActionResult> Delete(int id)
{
    var defectReport = await _context.DefectReports.FindAsync(id);
    if (defectReport == null)
    {
        return NotFound();
    }

    _context.DefectReports.Remove(defectReport);
    await _context.SaveChangesAsync();
    Console.WriteLine($"DefectReport with ID {id} deleted successfully!");

    return RedirectToAction("Record");
}


        private async Task LoadViewBagData()
        {
            ViewBag.UserRoles = await _context.UserRoles
                .Select(r => new { r.RoleId, r.RoleName })
                .ToListAsync();

            ViewBag.Defects = await _context.Defect
                .Select(d => new { d.DefectId, d.DefectName })
                .ToListAsync();

            ViewBag.LineProductions = await _context.LineProductions
                .Select(lp => new { lp.Id, lp.LineProductionName })
                .ToListAsync();
        }
    }
}
