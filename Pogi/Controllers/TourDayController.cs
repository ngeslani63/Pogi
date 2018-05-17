using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models.TourDayViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    [Authorize(Roles = "Member")]
    public class TourDayController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly ITourDay _tourDay;

        public TourDayController(PogiDbContext context,
            ITourDay sqlTourDay)
        {
            _context = context;
            _tourDay = sqlTourDay;
        }

        // GET: TourDay
        public async Task<IActionResult> Index(int? Id)
        {
            var Tour = await _context.Tour
                .SingleOrDefaultAsync(t => t.TourId == Id);
            if (Tour == null)
            {
                return NotFound();
            }
            var model = new TourDayIndexViewModel();
            model.Tour = Tour;
            model.TourDays = _tourDay.getForTour(Tour.TourId);
            return View(model);
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
        [Authorize(Roles = "AdminRoot,AdminScore,AdminTour")]
        // GET: TourDay/Create
        public IActionResult Create(int? Id)
        {
            var Tour = _context.Tour
            .SingleOrDefault(m => m.TourId == Id);
            if (Tour == null)
            {
                return NotFound();
            }
            var model = new TourDayEditViewModel();
            model.Tour = Tour;
            model.TourId = Tour.TourId;
            return View(model);

        }
        [Authorize(Roles = "AdminRoot,AdminScore,AdminTour")]
        // POST: TourDay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? Id, [Bind("TourDayId,TourId,TourDate")] TourDay tourDay)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var Tour = _context.Tour.SingleOrDefault(m => m.TourId == Id);
            if (ModelState.IsValid)
            {

                _context.Add(tourDay);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "TourDay", new { id = Id });
            }
            var model = new TourDayEditViewModel();
            model.Tour = Tour;
            model.TourDate = tourDay.TourDate;
            model.TourId = tourDay.TourId;
            model.TourDayId = tourDay.TourDayId;
            return View(model);
        }
        [Authorize(Roles = "AdminRoot,AdminScore,AdminTour")]
        // GET: TourDay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TourDay = await _context.TourDay.SingleOrDefaultAsync(m => m.TourDayId == id);
            if (TourDay == null)
            {
                return NotFound();
            }
            var Tour = _context.Tour
            .SingleOrDefault(m => m.TourId == TourDay.TourId);
            var model = new TourDayEditViewModel();
            model.Tour = Tour;
            model.TourDate = TourDay.TourDate;
            model.TourDayId = TourDay.TourDayId;
            model.TourId = TourDay.TourId;
            return View(model);
        }
        [Authorize(Roles = "AdminRoot,AdminScore,AdminTour")]
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
            var Tour = _context.Tour
                    .SingleOrDefault(m => m.TourId == tourDay.TourId);
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

                return RedirectToAction(nameof(Index), "TourDay", new { id = Tour.TourId });
            }
            var model = new TourDayEditViewModel();
            model.Tour = Tour;
            model.TourDate = tourDay.TourDate;
            model.TourDayId = tourDay.TourDayId;
            model.TourId = tourDay.TourId;

            return View(model);
        }
        [Authorize(Roles = "AdminRoot,AdminScore,AdminTour")]
        // GET: TourDay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var TourDay = await _context.TourDay.SingleOrDefaultAsync(m => m.TourDayId == id);
            if (TourDay == null)
            {
                return NotFound();
            }
            var Tour = _context.Tour
             .SingleOrDefault(m => m.TourId == TourDay.TourId);
            var model = new TourDayEditViewModel();
            model.Tour = Tour;
            model.TourDate = TourDay.TourDate;
            model.TourDayId = TourDay.TourDayId;
            model.TourId = TourDay.TourId;

            return View(model);
        }
        [Authorize(Roles = "AdminRoot,AdminScore,AdminTour")]
        // POST: TourDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourDay = await _context.TourDay.SingleOrDefaultAsync(m => m.TourDayId == id);
            var TourId = tourDay.TourId;
            _context.TourDay.Remove(tourDay);
            await _context.SaveChangesAsync();
            var Tour = _context.Tour
                .SingleOrDefault(m => m.TourId == TourId);
            return RedirectToAction(nameof(Index), "TourDay", new { id = Tour.TourId });

        }

        private bool TourDayExists(int id)
        {
            return _context.TourDay.Any(e => e.TourDayId == id);
        }
    }
}
