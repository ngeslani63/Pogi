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
    public class TourDayController : Controller
    {
        private readonly PogiDbContext _context;

        public TourDayController(PogiDbContext context)
        {
            _context = context;
        }

        // GET: TourDay
        public async Task<IActionResult> Index()
        {
            return View(await _context.TourDay.ToListAsync());
        }

        // GET: TourDay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDay = await _context.TourDay
                .SingleOrDefaultAsync(m => m.TourDayId == id);
            if (tourDay == null)
            {
                return NotFound();
            }

            return View(tourDay);
        }

        // GET: TourDay/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TourDay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourDayId,TourId,TourDate")] TourDay tourDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourDay);
        }

        // GET: TourDay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDay = await _context.TourDay.SingleOrDefaultAsync(m => m.TourDayId == id);
            if (tourDay == null)
            {
                return NotFound();
            }
            return View(tourDay);
        }

        // POST: TourDay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TourDayId,TourId,TourDate")] TourDay tourDay)
        {
            if (id != tourDay.TourDayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourDayExists(tourDay.TourDayId))
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
            return View(tourDay);
        }

        // GET: TourDay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourDay = await _context.TourDay
                .SingleOrDefaultAsync(m => m.TourDayId == id);
            if (tourDay == null)
            {
                return NotFound();
            }

            return View(tourDay);
        }

        // POST: TourDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourDay = await _context.TourDay.SingleOrDefaultAsync(m => m.TourDayId == id);
            _context.TourDay.Remove(tourDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourDayExists(int id)
        {
            return _context.TourDay.Any(e => e.TourDayId == id);
        }
    }
}
