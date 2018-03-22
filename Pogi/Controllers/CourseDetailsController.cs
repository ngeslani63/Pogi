using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;

namespace Pogi.Controllers
{
    public class CourseDetailsController : Controller
    {
        private readonly PogiDbContext _context;

        public CourseDetailsController(PogiDbContext context)
        {
            _context = context;
        }

        // GET: CourseDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseDetail.ToListAsync());
        }

        // GET: CourseDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseDetail = await _context.CourseDetail
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseDetail == null)
            {
                return NotFound();
            }

            return View(courseDetail);
        }

        // GET: CourseDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Color,Yards01,Yards02,Yards03,Yards04,Yards05,Yards06,Yards07,Yards08,Yards09,Yards10,Yards11,Yards12,Yards13,Yards14,Yards15,Yards16,Yards17,Yards18,YardsIn,YardsOut,YardsTotal,Rating,Slope,CreatedBy,CreatedTs,LastUpdatedBy,LastUpdatedTs")] CourseDetail courseDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseDetail);
        }

        // GET: CourseDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseDetail = await _context.CourseDetail.SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseDetail == null)
            {
                return NotFound();
            }
            return View(courseDetail);
        }

        // POST: CourseDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Color,Yards01,Yards02,Yards03,Yards04,Yards05,Yards06,Yards07,Yards08,Yards09,Yards10,Yards11,Yards12,Yards13,Yards14,Yards15,Yards16,Yards17,Yards18,YardsIn,YardsOut,YardsTotal,Rating,Slope,CreatedBy,CreatedTs,LastUpdatedBy,LastUpdatedTs")] CourseDetail courseDetail)
        {
            if (id != courseDetail.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseDetailExists(courseDetail.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseDetail);
        }

        // GET: CourseDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseDetail = await _context.CourseDetail
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (courseDetail == null)
            {
                return NotFound();
            }

            return View(courseDetail);
        }

        // POST: CourseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseDetail = await _context.CourseDetail.SingleOrDefaultAsync(m => m.CourseId == id);
            _context.CourseDetail.Remove(courseDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseDetailExists(int id)
        {
            return _context.CourseDetail.Any(e => e.CourseId == id);
        }
    }
}
