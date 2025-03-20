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
            await LoadViewBagData();
            return View();
        }

        public async Task<IActionResult> Index()
        {
            await LoadViewBagData();
            return View();
        }
        [HttpGet]
        public JsonResult GetChartData(int? lineProductionId)
        {
            var data = _context.DefectReports
                .Where(d => !lineProductionId.HasValue || d.LineProductionId == lineProductionId)
                .GroupBy(d => new { d.DefectId, d.Defect.DefectName })
                .Select(g => new
                {
                    Label = g.Key.DefectName,
                    Value = g.Count()
                })
                .ToList();

            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> Input(DefectReport defectReport)
        {

            if (ModelState.IsValid)
            {
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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var defectReport = await _context.DefectReports
                .Include(d => d.Defect)
                .Include(d => d.LineProduction)
                .Include(d => d.Role)
                .FirstOrDefaultAsync(d => d.ReportId == id);

            if (defectReport == null)
            {
                return NotFound();
            }

            await LoadViewBagData();
            return View(defectReport);
        }
        [HttpPost]
        public async Task<IActionResult> Update(DefectReport defectReport)
        {
            if (ModelState.IsValid)
            {
                await LoadViewBagData();
                return View(defectReport);
            }

            var existingReport = await _context.DefectReports.FindAsync(defectReport.ReportId);
            if (existingReport == null)
            {
                return NotFound();
            }
            existingReport.Reporter = defectReport.Reporter;
            existingReport.RoleId = defectReport.RoleId;
            existingReport.LineProductionId = defectReport.LineProductionId;
            existingReport.DefectId = defectReport.DefectId;
            existingReport.Description = defectReport.Description;
            existingReport.Status = defectReport.Status;

            _context.DefectReports.Update(existingReport);
            await _context.SaveChangesAsync();

            return RedirectToAction("Record");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var defectReport = await _context.DefectReports.FindAsync(id);
            if (defectReport == null)
            {
                return Json(new { success = false, message = "Data not found" });
            }

            _context.DefectReports.Remove(defectReport);
            await _context.SaveChangesAsync();
            Console.WriteLine($"DefectReport with ID {id} deleted successfully!");

            return Json(new { success = true, message = "Record deleted successfully" });
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
