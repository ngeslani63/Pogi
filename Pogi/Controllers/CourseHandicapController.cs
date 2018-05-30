using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models.CourseHandicapViewModel;

namespace Pogi.Controllers
{
    public class CourseHandicapController : Controller
    {
        private readonly PogiDbContext _context;

        public CourseHandicapController(PogiDbContext context)
        {
            _context = context;
        }

        // GET: CourseHandicap
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseHandicap.ToListAsync());
        }

        // GET: CourseHandicap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseHandicap = await _context.CourseHandicap
                .SingleOrDefaultAsync(m => m.CourseHandicapId == id);
            if (courseHandicap == null)
            {
                return NotFound();
            }

            return View(courseHandicap);
        }

        // GET: CourseHandicap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseHandicap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseHandicapId,CourseId,MenHcp01,MenHcp02,MenHcp03,MenHcp04,MenHcp05,MenHcp06,MenHcp07,MenHcp08,MenHcp09,MenHcp10,MenHcp11,MenHcp12,MenHcp13,MenHcp14,MenHcp15,MenHcp16,MenHcp17,MenHcp18,LadiesHcp01,LadiesHcp02,LadiesHcp03,LadiesHcp04,LadiesHcp05,LadiesHcp06,LadiesHcp07,LadiesHcp08,LadiesHcp09,LadiesHcp10,LadiesHcp11,LadiesHcp12,LadiesHcp13,LadiesHcp14,LadiesHcp15,LadiesHcp16,LadiesHcp17,LadiesHcp18")] CourseHandicap courseHandicap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseHandicap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseHandicap);
        }

        // GET: CourseHandicap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseHandicap = await _context.CourseHandicap.SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseHandicap == null)
            {
                courseHandicap = new CourseHandicap();
                courseHandicap.CourseId = (int)id;
                _context.Add(courseHandicap);
                await _context.SaveChangesAsync();
            }
            var model = new CourseHandicapEditViewModel(courseHandicap);

            return View(model);
        }

        // POST: CourseHandicap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseHandicapId,CourseId,MenHcp01,MenHcp02,MenHcp03,MenHcp04,MenHcp05,MenHcp06,MenHcp07,MenHcp08,MenHcp09,MenHcp10,MenHcp11,MenHcp12,MenHcp13,MenHcp14,MenHcp15,MenHcp16,MenHcp17,MenHcp18,LadiesHcp01,LadiesHcp02,LadiesHcp03,LadiesHcp04,LadiesHcp05,LadiesHcp06,LadiesHcp07,LadiesHcp08,LadiesHcp09,LadiesHcp10,LadiesHcp11,LadiesHcp12,LadiesHcp13,LadiesHcp14,LadiesHcp15,LadiesHcp16,LadiesHcp17,LadiesHcp18")] CourseHandicap courseHandicap)
        {
            if (id != courseHandicap.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseHandicap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseHandicapExists(courseHandicap.CourseHandicapId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit","Course", new { id = id });
            }
            var model = new CourseHandicapEditViewModel(courseHandicap);
            return View(model);
        }   

        // GET: CourseHandicap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseHandicap = await _context.CourseHandicap
                .SingleOrDefaultAsync(m => m.CourseHandicapId == id);
            if (courseHandicap == null)
            {
                return NotFound();
            }

            return View(courseHandicap);
        }

        // POST: CourseHandicap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseHandicap = await _context.CourseHandicap.SingleOrDefaultAsync(m => m.CourseHandicapId == id);
            _context.CourseHandicap.Remove(courseHandicap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseHandicapExists(int id)
        {
            return _context.CourseHandicap.Any(e => e.CourseHandicapId == id);
        }
        private bool CourseIdExists(int id)
        {
            return _context.CourseHandicap.Any(e => e.CourseId == id);
        }
    }
}
