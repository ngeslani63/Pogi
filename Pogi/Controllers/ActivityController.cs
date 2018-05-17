using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Services;

namespace Pogi.Controllers
{
    public class ActivityController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly IActivity _activity;

        public ActivityController(PogiDbContext context,
            IActivity activity)
        {
            _context = context;
            _activity = activity;
        }

        // GET: Activity
        public IActionResult Index(int? id)
        {
            int days = 1;
            if (id != null)
            {
                days = (int)id;
            }
            return View(_activity.getActivity(days).ToList());
            //return View(await _context.Log2.ToListAsync());
        }

        // GET: Activity/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var log2 = await _context.Log2
                .SingleOrDefaultAsync(m => m.ActivityId == id);
            if (log2 == null)
            {
                return NotFound();
            }

            return View(log2);
        }

        // GET: Activity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Activity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,MemberId,UserName,ipAddr,Action,createdTS")] Log2 log2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(log2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(log2);
        }

        // GET: Activity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var log2 = await _context.Log2.SingleOrDefaultAsync(m => m.ActivityId == id);
            if (log2 == null)
            {
                return NotFound();
            }
            return View(log2);
        }

        // POST: Activity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityId,MemberId,UserName,ipAddr,Action,createdTS")] Log2 log2)
        {
            if (id != log2.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(log2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Log2Exists(log2.ActivityId))
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
            return View(log2);
        }

        // GET: Activity/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var log2 = await _context.Log2
                .SingleOrDefaultAsync(m => m.ActivityId == id);
            if (log2 == null)
            {
                return NotFound();
            }

            return View(log2);
        }

        // POST: Activity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var log2 = await _context.Log2.SingleOrDefaultAsync(m => m.ActivityId == id);
            _context.Log2.Remove(log2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Log2Exists(int id)
        {
            return _context.Log2.Any(e => e.ActivityId == id);
        }
    }
}
