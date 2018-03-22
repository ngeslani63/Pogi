using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;
using Pogi.Models.PlayerViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    [Authorize(Roles = "Member")]
    public class PlayerController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemberData _memberData;

        public PlayerController(PogiDbContext context, SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager, IMemberData memberData)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _memberData = memberData;
        }

        // GET: Player
        public async Task<IActionResult> Index()
        {
            return View(await _context.Player.ToListAsync());
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Player
                .SingleOrDefaultAsync(m => m.PlayId == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            var model = new PlayerCreateViewModel();
            model.Members = _memberData.getAll();
            if (_signInManager.IsSignedIn(User))
            {
               model.Member = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                model.Player.MemberId = model.Member.MemberId;
            }
            return View(model);
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PlayId,MemberId,GuestName,PlayDate")] Player players)
        public async Task<IActionResult> Create(PlayerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Player player = model.Player;
                if (!model.MemberPlaying &&
                    (model.Player.GuestName == null || model.Player.GuestName.Length == 0))
                {
                    ModelState.AddModelError("MemberPlaying", "Check Member or specify Guest");
                    return View(model);
                }
                if (model.Player.GuestName != null && model.Player.GuestName.Length > 0)
                {
                    Player guest = new Player();
                    guest.MemberId = player.MemberId;
                    guest.GuestName = player.GuestName;
                    guest.PlayDate = player.PlayDate;
                    guest.preferTeeTimeId1 = player.preferTeeTimeId1;
                    guest.preferTeeTimeId2 = player.preferTeeTimeId2;
                    guest.preferTeeTimeId3 = player.preferTeeTimeId3;
                    _context.Add(guest);
                }
                if (model.MemberPlaying == true)
                {
                    player.GuestName = "";
                    _context.Add(player);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index","TeeTime" );
            }
            return View(model);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Player.SingleOrDefaultAsync(m => m.PlayId == id);
            if (players == null)
            {
                return NotFound();
            }
            return View(players);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayId,MemberId,GuestName,PlayDate,Order,preferTeeTimeId1,preferTeeTimeId2,preferTeeTimeId3")] Player players)
        {
            if (id != players.PlayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(players);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayersExists(players.PlayId))
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
            return View(players);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Player
                .SingleOrDefaultAsync(m => m.PlayId == id);
            if (players == null)
            {
                return NotFound();
            }
            var model = new PlayerCreateViewModel();
            //model.Members = _memberData.getAll();
            if (_signInManager.IsSignedIn(User))
            {
                model.Member = _memberData.get(players.MemberId);
                model.Player.MemberId = model.Member.MemberId;
            }
            return View(model);

        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var players = await _context.Player.SingleOrDefaultAsync(m => m.PlayId == id);
            _context.Player.Remove(players);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "TeeTime");
        }

        private bool PlayersExists(int id)
        {
            return _context.Player.Any(e => e.PlayId == id);
        }
    }
}
