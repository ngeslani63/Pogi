using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models.TeeAssignViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    public class TeeAssignController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly ITeeAssignInfo _teeAssignInfo;

        public TeeAssignController(ITeeAssignInfo teeAssignInfo, PogiDbContext context)
        {
            _context = context;
            _teeAssignInfo = teeAssignInfo;
        }

        // GET: TeeAssign
        //public async Task<IActionResult> Index(PogiDbContext context)
        public IActionResult Index()
        {
            var model = new TeeAssignViewModel();
            model.TeeAssignInfos = _teeAssignInfo.getAll();

            return View(model);
            //return View(await _context.TeeAssign.ToListAsync());
        }

        // GET: TeeAssign/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeAssign = await _context.TeeAssign
                .SingleOrDefaultAsync(m => m.TeeAssignId == id);
            if (teeAssign == null)
            {
                return NotFound();
            }

            return View(teeAssign);
        }

        // GET: TeeAssign/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeeAssign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeeAssignId,TeeTimeId,MemberId,GuestName,Order,NoShow,RecordStatus")] TeeAssign teeAssign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teeAssign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teeAssign);
        }

        // GET: TeeAssign/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeAssign = await _context.TeeAssign.SingleOrDefaultAsync(m => m.TeeAssignId == id);
            if (teeAssign == null)
            {
                return NotFound();
            }
            return View(teeAssign);
        }

        // POST: TeeAssign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeeAssignId,TeeTimeId,MemberId,GuestName,Order,NoShow,RecordStatus")] TeeAssign teeAssign)
        {
            if (id != teeAssign.TeeAssignId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teeAssign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeeAssignExists(teeAssign.TeeAssignId))
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
            return View(teeAssign);
        }

        // GET: TeeAssign/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teeAssign = await _context.TeeAssign
                .SingleOrDefaultAsync(m => m.TeeAssignId == id);
            if (teeAssign == null)
            {
                return NotFound();
            }

            return View(teeAssign);
        }

        // POST: TeeAssign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teeAssign = await _context.TeeAssign.SingleOrDefaultAsync(m => m.TeeAssignId == id);
            _context.TeeAssign.Remove(teeAssign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeeAssignExists(int id)
        {
            return _context.TeeAssign.Any(e => e.TeeAssignId == id);
        }
    }
}
