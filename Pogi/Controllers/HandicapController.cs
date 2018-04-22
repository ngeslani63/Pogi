using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models.HandicapViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    public class HandicapController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly IHandicap _handicap;

        public HandicapController(PogiDbContext context,
            IHandicap sqlHandicap)
        {
            _context = context;
            _handicap = sqlHandicap;
        }

        // GET: Handicap
        public async Task<IActionResult> Index(int? Id)
        {
            var Member = await _context.Member
                .SingleOrDefaultAsync(m => m.MemberId == Id);
            if (Member == null)
            {
                return NotFound();
            }
            var model = new HandicapIndexViewModel();
            model.Member = Member;
            model.Handicaps = _handicap.getForGhin(Member.GhinNumber);

            return View(model);
        }

        // GET: Handicap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var handicap = await _context.Handicap
                .SingleOrDefaultAsync(m => m.HandicapId == id);
            if (handicap == null)
            {
                return NotFound();
            }

            return View(handicap);
        }

        // GET: Handicap/Create
        public IActionResult Create(int? Id)
        {
             var Member = _context.Member
             .SingleOrDefault(m => m.MemberId == Id);
            if (Member == null)
            {
                return NotFound();
            }
            var model = new HandicapEditViewModel();
            model.Member = Member;
            
            model.GhinNumber = Member.GhinNumber;
            model.HcpIndex = Member.CurrHandicap;
            if (model.GhinNumber > 0)
            {
                model.Date = _handicap.getNextDate(Member.GhinNumber);
                model.ActiveDates = _handicap.getActiveDates(model.Date.ToShortDateString().ToString());
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Handicap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? Id,[Bind("HandicapId,GhinNumber,Date,HcpIndex")] Handicap handicap)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var Member = _context.Member.SingleOrDefault(m => m.MemberId == Id);
            if (ModelState.IsValid)
            {
 
                _context.Add(handicap);
                
                if (handicap.Date == _handicap.getCurrEffDate())
                {
                    Member.CurrHandicap = handicap.HcpIndex;
                    _context.Update(Member);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"Handicap", new { id = Id });
            }
            var model = new HandicapEditViewModel();
            model.Member = Member;
            model.Date = handicap.Date;
            model.ActiveDates = _handicap.getActiveDates(model.Date.ToShortDateString().ToString());
            model.HandicapId = handicap.HandicapId;
            model.GhinNumber = Member.GhinNumber;
            model.HcpIndex = handicap.HcpIndex;

            return View(model);
        }

        // GET: Handicap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Handicap = await _context.Handicap.SingleOrDefaultAsync(m => m.HandicapId == id);
            if (Handicap == null)
            {
                return NotFound();
            }
            var Member = _context.Member
             .SingleOrDefault(m => m.GhinNumber == Handicap.GhinNumber);
            var model = new HandicapEditViewModel();
            model.Member = Member;
            model.Date = Handicap.Date;
            model.ActiveDates = _handicap.getActiveDates(model.Date.ToShortDateString().ToString());
            model.HandicapId = Handicap.HandicapId;
            model.GhinNumber = Member.GhinNumber;
            model.HcpIndex = Handicap.HcpIndex;
            return View(model);
        }

        // POST: Handicap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HandicapId,GhinNumber,Date,HcpIndex")] Handicap handicap)
        {
            if (id != handicap.HandicapId)
            {
                return NotFound();
            }
            var Member = _context.Member
                    .SingleOrDefault(m => m.GhinNumber == handicap.GhinNumber);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(handicap);
                    if (handicap.Date == _handicap.getCurrEffDate())
                    {
                        Member.CurrHandicap = handicap.HcpIndex;
                        _context.Update(Member);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HandicapExists(handicap.HandicapId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), "Handicap", new { id = Member.MemberId });
            }
            var model = new HandicapEditViewModel();
            model.Member = Member;
            model.Date = handicap.Date;
            model.ActiveDates = _handicap.getActiveDates(model.Date.ToShortDateString().ToString());
            model.HandicapId = handicap.HandicapId;
            model.GhinNumber = Member.GhinNumber;
            model.HcpIndex = handicap.HcpIndex;
            
            return View(model);
        }
    
        // GET: Handicap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Handicap = await _context.Handicap.SingleOrDefaultAsync(m => m.HandicapId == id);
            if (Handicap == null)
            {
                return NotFound();
            }
            var Member = _context.Member
             .SingleOrDefault(m => m.GhinNumber == Handicap.GhinNumber);
            var model = new HandicapEditViewModel();
            model.Member = Member;
            model.Date = Handicap.Date;
            model.ActiveDates = _handicap.getActiveDates(model.Date.ToShortDateString().ToString());
            model.HandicapId = Handicap.HandicapId;
            model.GhinNumber = Member.GhinNumber;
            model.HcpIndex = Handicap.HcpIndex;
            
            return View(model);
        }

        // POST: Handicap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var handicap = await _context.Handicap.SingleOrDefaultAsync(m => m.HandicapId == id);
            var GhinNumber = handicap.GhinNumber;
            _context.Handicap.Remove(handicap);
            await _context.SaveChangesAsync();
            var Member = _context.Member
                .SingleOrDefault(m => m.GhinNumber == GhinNumber);
            return RedirectToAction(nameof(Index), "Handicap", new { id = Member.MemberId });
        }

        private bool HandicapExists(int id)
        {
            return _context.Handicap.Any(e => e.HandicapId == id);
        }
    }
}
