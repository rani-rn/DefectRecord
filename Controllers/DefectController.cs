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
        public async Task<IActionResult> GetDefectList(string searchTerm)
        {
            var defects = await _context.Defect
                .Where(d => string.IsNullOrEmpty(searchTerm) || d.DefectName.Contains(searchTerm))
                .Select(d => new { id = d.DefectId.ToString(), text = d.DefectName })
                .Take(10)
                .ToListAsync();

            return Json(defects);
        }

        [HttpGet]
        public async Task<IActionResult> Input()
        {
            ViewBag.Defects = await _context.Defect.ToListAsync();
            ViewBag.LineProductions = await _context.LineProductions.ToListAsync();
            return View();
        }


    }
}
