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
        private readonly ITeeTimeInfo _teeTimeInfo;

        public PlayerController(PogiDbContext context, SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager, IMemberData memberData,
                ITeeTimeInfo teeTimeInfo)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _memberData = memberData;
            _teeTimeInfo = teeTimeInfo;
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
            model.Members = _memberData.getSelectList();
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
        //public async Task<IActionResult> Create([Bind("PlayId,MemberId,GuestName,PlayDate,Order,PreferTeeTimeId1,PreferTeeTimeId2,PreferTeeTimeId3,RegistrationMethod,AssignedTee,Confirmed,Withdrawn,WithdrawReason")] Player players)
        public async Task<IActionResult> Create(PlayerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Player player = model.Player;
                Member member = model.Member;
                if (!model.MemberPlaying &&
                    (model.Player.GuestName == null || model.Player.GuestName.Length == 0))
                {
                    ModelState.AddModelError("MemberPlaying", "Check Member or specify Guest");
                    return View(model);
                }
                player.EnteredById = member.MemberId;
                if (player.MemberId == member.MemberId)
                {
                    player.RegistrationMethod = RegistrationType.Self;
                    player.Confirmed = true;
                }
                else
                {
                    player.RegistrationMethod = RegistrationType.Admin;
                    player.Confirmed = false;
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
                    player.RegistrationMethod = RegistrationType.Admin;
                    _context.Add(guest);
                }
                if (model.MemberPlaying == true)
                {
                    player.GuestName = "";
                    _context.Add(player);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "TeeTime");
            }
            return View(model);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Confirm(int? id)
        {
            var player = await _context.Player.SingleOrDefaultAsync(m => m.PlayId == id);
            if (player == null)
            {
                return NotFound();
            }
            var member = _memberData.get(player.MemberId);
            var enteredBy = _memberData.get(player.EnteredById);
            var model = new PlayerDisplayViewModel();
            model.MemberPlaying = true;
            model.Player = player;
            model.EnteredBy = enteredBy;
            model.Member = member;
            

            return View(model);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int id)
        {
            var player = await _context.Player.SingleOrDefaultAsync(m => m.PlayId == id);
            if (player == null)
            {
                return NotFound();
            }

            try
            {
                player.Confirmed = true;
                _context.Update(player);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayersExists(player.PlayId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "TeeTime");
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .SingleOrDefaultAsync(m => m.PlayId == id);
            if (player == null)
            {
                return NotFound();
            }
            var model = new PlayerCreateViewModel();
            //model.Members = _memberData.getAll();
            if (_signInManager.IsSignedIn(User))
            {
                model.Member = _memberData.get(player.MemberId);
                model.Player.MemberId = model.Member.MemberId;
                model.Player.PlayDate = player.PlayDate;
                model.MajorTourDay = _teeTimeInfo.majorTourDay(player.PlayDate);
                if (player.Withdrawn) model.MajorTourDay = false;
            }
            return View(model);

        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [Bind("MajorTourDay")] bool majorTourDay)
        {
            var player = await _context.Player.SingleOrDefaultAsync(m => m.PlayId == id);
            if (majorTourDay)
            {
                player.Withdrawn = true;
                _context.Update(player);
            }
            else
            {
                _context.Player.Remove(player);
            }
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "TeeTime");
        }
        [HttpPost, ActionName("Withdraw")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WithdrawConfirmed(int id)
        {
            var player = await _context.Player.SingleOrDefaultAsync(m => m.PlayId == id);
            player.Withdrawn = true;
            _context.Update(player);
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
