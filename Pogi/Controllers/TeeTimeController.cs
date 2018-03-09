using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models.TeeTimeViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    public class TeeTimeController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly ITeeTimeInfo _teeTimeInfo;
        private readonly IPlayerInfo _playerInfo;
        private readonly ITeeAssignInfo _teeAssignInfo;

        public TeeTimeController(ITeeTimeInfo teeTimeInfo, IPlayerInfo playerInfo, ITeeAssignInfo teeAssignInfo, PogiDbContext context)
        {
            _context = context;
            _teeTimeInfo = teeTimeInfo;
            _playerInfo = playerInfo;
            _teeAssignInfo = teeAssignInfo;
        }

        // GET: TeeTime
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            var model = new TeeTimeViewModel();
            model.TeeTimeInfos = _teeTimeInfo.getAll();
            model.PlayerInfos = _playerInfo.getRoster();
            //model.TeeTimeInfos = _teeAssignInfo.getForTeeTime()

            return View(model);
            //return View(await _context.TeeTime.ToListAsync());
        }

        // GET: TeeTime/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeTime = await _context.TeeTime
                .SingleOrDefaultAsync(m => m.TeeTimeId == id);
            if (teeTime == null)
            {
                return NotFound();
            }

            return View(teeTime);
        }

        // GET: TeeTime/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeeTime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeeTimeId,ReservedById,TeeTimeTS,CourseId,NumPlayers,AutoAssign")] TeeTime teeTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teeTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teeTime);
        }

        // GET: TeeTime/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeTime = await _context.TeeTime.SingleOrDefaultAsync(m => m.TeeTimeId == id);
            if (teeTime == null)
            {
                return NotFound();
            }
            return View(teeTime);
        }

        // POST: TeeTime/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeeTimeId,ReservedById,TeeTimeTS,CourseId,NumPlayers,AutoAssign")] TeeTime teeTime)
        {
            if (id != teeTime.TeeTimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teeTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeeTimeExists(teeTime.TeeTimeId))
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
            return View(teeTime);
        }

        // GET: TeeTime/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeTime = await _context.TeeTime
                .SingleOrDefaultAsync(m => m.TeeTimeId == id);
            if (teeTime == null)
            {
                return NotFound();
            }

            return View(teeTime);
        }

        // POST: TeeTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teeTime = await _context.TeeTime.SingleOrDefaultAsync(m => m.TeeTimeId == id);
            _context.TeeTime.Remove(teeTime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeeTimeExists(int id)
        {
            return _context.TeeTime.Any(e => e.TeeTimeId == id);
        }
    }
}
