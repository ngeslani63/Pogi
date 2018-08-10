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
using Pogi.Models;
using Pogi.Models.TeeAssignViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    [Authorize(Roles = "AdminRoot,AdminTour,AdminTeeTime")]
    public class TeeAssignController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly ITeeAssignInfo _teeAssignInfo;
        private readonly IPlayerInfo _playerInfo;
        private readonly IDateTime _dateTime;

        public object Integer { get; private set; }

        public TeeAssignController(IPlayerInfo playerInfo, ITeeAssignInfo teeAssignInfo, PogiDbContext context,
            IDateTime dateTime)
        {
            _context = context;
            _teeAssignInfo = teeAssignInfo;
            _playerInfo = playerInfo;
            _dateTime = dateTime;
        }
        public async Task<IActionResult> Edit2(int? id)
        {
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
                var model = new TeeAssignEditViewModel();
                model.TeeTime = teeTime;
                model.ReservedBy = await _context.Member.SingleOrDefaultAsync(m => m.MemberId == teeTime.ReservedById);
                model.Course = await _context.Course.SingleOrDefaultAsync(m => m.CourseId == teeTime.CourseId);

                var teeAssigns = _teeAssignInfo.getAllForTeeTime(teeTime.TeeTimeId);
                var playIds = new List<string>();
                foreach (var teeAssign in teeAssigns)
                {
                    playIds.Add(teeAssign.TeeAsign.PlayId.ToString());
                }
                model.PlayIds = playIds;
                model.Players = _playerInfo.getPlayers(teeTime.TeeTimeTS);

                return View(model);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(int id, List<string> playIds)
        {
            var teeTime = await _context.TeeTime.SingleOrDefaultAsync(m => m.TeeTimeId == id);
            if (teeTime == null)
            {
                return NotFound();
            }
            var model = new TeeAssignEditViewModel();
            model.TeeTime = teeTime;
            model.ReservedBy = await _context.Member.SingleOrDefaultAsync(m => m.MemberId == teeTime.ReservedById);
            model.Course = await _context.Course.SingleOrDefaultAsync(m => m.CourseId == teeTime.CourseId);
            var teeAssigns = _teeAssignInfo.getAllForTeeTime(teeTime.TeeTimeId);

            if (ModelState.IsValid)
            {
                try
                {
                    int i = 0;
                    foreach (TeeAssignInfo teeAssignInfo in teeAssigns)
                    {
                        TeeAssign teeAssign = teeAssignInfo.TeeAsign;
                        teeAssign.PlayId = int.Parse(playIds[i]);
                        if (teeAssign.PlayId > 0)
                        {
                            Player player = await _context.Player.SingleOrDefaultAsync(m => m.PlayId == teeAssign.PlayId);
                            teeAssign.MemberId = player.MemberId;
                            teeAssign.GuestName = "";
                            if (player.GuestName != null && player.GuestName.Length > 0)
                            {
                                teeAssign.GuestName = player.GuestName + ", Guest";
                            }

                        }
                        else
                        {
                            teeAssign.MemberId = 0;
                            teeAssign.GuestName = "";
                            //if(teeAssign.TeeAssignId > 0) _context.TeeAssign.Remove(teeAssign);
                        }
                        teeAssign.Group = (int)Math.Floor((float)i / 4.0F) + 1;
                        teeAssign.Order = i % 4 + 1;
                        if (teeAssign.TeeAssignId == 0) _context.Add(teeAssign);
                        else _context.Update(teeAssign);
                        i++;
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index),"TeeTime");
            }
            model.PlayIds = playIds;
            model.Players = _playerInfo.getPlayers(teeTime.TeeTimeTS);
            return View(model);
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
