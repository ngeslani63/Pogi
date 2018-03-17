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
using Pogi.Services;

namespace Pogi.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly IMemberData _memberData;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MemberController(PogiDbContext context, IMemberData sqlMemberData,
                        UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _memberData = sqlMemberData;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }
        // GET: Member
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            var model = _memberData.getAll();
            return View(model);
            //return View(await _context.Member.ToListAsync());
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .SingleOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Member/Create
        [Authorize(Roles = "AdminRoot,AdminUser")]
        public IActionResult Create()
        {
            var model = new Member();
            return View(model);
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRoot,AdminUser")]
        public async Task<IActionResult> Create([Bind("MemberId,FirstName,LastName,Phone1st,Phone1stType,Phone2nd,Phone2ndType,EmailAddr1st,EmailAddr2nd,RecordStatus,MemberStatus,Profession,MaritalStatus,Street,City,State,Zip,Country,GhinNumber,CurrHandicap," +
            "RoleAdminRoot,RoleAdminUser,RoleAdminCourse,RoleAdminTeeTime,RoleAdminTour")] Member member)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(member.EmailAddr1st);
                if (user != null)
                {
                    if (!await _roleManager.RoleExistsAsync("AdminRoot"))
                        await _roleManager.CreateAsync(new IdentityRole { Name = "AdminRoot" });
                    if (!await _roleManager.RoleExistsAsync("AdminUser"))
                        await _roleManager.CreateAsync(new IdentityRole { Name = "AdminUser" });
                    if (!await _roleManager.RoleExistsAsync("AdminCourse"))
                        await _roleManager.CreateAsync(new IdentityRole { Name = "AdminCourse" });
                    if (!await _roleManager.RoleExistsAsync("AdminTeeTime"))
                        await _roleManager.CreateAsync(new IdentityRole { Name = "AdminTeeTime" });
                    if (!await _roleManager.RoleExistsAsync("AdminTour"))
                        await _roleManager.CreateAsync(new IdentityRole { Name = "AdminTour" });


                    if (member.RoleAdminRoot != await _userManager.IsInRoleAsync(user, "AdminRoot"))
                    {
                        if (member.RoleAdminRoot == true) await _userManager.AddToRoleAsync(user, "AdminRoot");
                        else await _userManager.RemoveFromRoleAsync(user, "AdminRoot");
                    }
                    if (member.RoleAdminUser != await _userManager.IsInRoleAsync(user, "AdminUser"))
                    {
                        if (member.RoleAdminUser == true) await _userManager.AddToRoleAsync(user, "AdminUser");
                        else await _userManager.RemoveFromRoleAsync(user, "AdminUser");
                    }
                    if (member.RoleAdminCourse != await _userManager.IsInRoleAsync(user, "AdminCourse"))
                    {
                        if (member.RoleAdminCourse == true) await _userManager.AddToRoleAsync(user, "AdminCourse");
                        else await _userManager.RemoveFromRoleAsync(user, "AdminCourse");
                    }
                    if (member.RoleAdminTeeTime != await _userManager.IsInRoleAsync(user, "AdminTeeTime"))
                    {
                        if (member.RoleAdminTeeTime == true) await _userManager.AddToRoleAsync(user, "AdminTeeTime");
                        else await _userManager.RemoveFromRoleAsync(user, "AdminTeeTime");
                    }
                    if (member.RoleAdminTour != await _userManager.IsInRoleAsync(user, "AdminTour"))
                        {
                        if (member.RoleAdminTeeTime == true) await _userManager.AddToRoleAsync(user, "AdminTour");
                        else await _userManager.RemoveFromRoleAsync(user, "AdminTour");
                    }
                }

                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Member/Edit/5
        [Authorize(Roles = "AdminRoot,AdminUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.SingleOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRoot,AdminUser")]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,FirstName,LastName,Phone1st,Phone1stType,Phone2nd,Phone2ndType,EmailAddr1st,EmailAddr2nd,RecordStatus,MemberStatus,Profession,MaritalStatus,Street,City,State,Zip,Country,GhinNumber,CurrHandicap," +
            "RoleAdminRoot,RoleAdminUser,RoleAdminCourse,RoleAdminTeeTime,RoleAdminTour" )] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var user = await _userManager.FindByEmailAsync(member.EmailAddr1st);
                    if (user != null)
                    {
                        if (!await _roleManager.RoleExistsAsync("AdminRoot"))
                            await _roleManager.CreateAsync(new IdentityRole { Name = "AdminRoot" });
                        if (!await _roleManager.RoleExistsAsync("AdminUser"))
                            await _roleManager.CreateAsync(new IdentityRole { Name = "AdminUser" });
                        if (!await _roleManager.RoleExistsAsync("AdminCourse"))
                            await _roleManager.CreateAsync(new IdentityRole { Name = "AdminCourse" });
                        if (!await _roleManager.RoleExistsAsync("AdminTeeTime"))
                            await _roleManager.CreateAsync(new IdentityRole { Name = "AdminTeeTime" });
                        if (!await _roleManager.RoleExistsAsync("AdminTour"))
                            await _roleManager.CreateAsync(new IdentityRole { Name = "AdminTour" });


                        if (member.RoleAdminRoot != await _userManager.IsInRoleAsync(user, "AdminRoot"))
                        {
                            if (member.RoleAdminRoot == true) await _userManager.AddToRoleAsync(user, "AdminRoot");
                            else await _userManager.RemoveFromRoleAsync(user, "AdminRoot");
                        }
                        if (member.RoleAdminUser != await _userManager.IsInRoleAsync(user, "AdminUser"))
                        {
                            if (member.RoleAdminUser == true) await _userManager.AddToRoleAsync(user, "AdminUser");
                            else await _userManager.RemoveFromRoleAsync(user, "AdminUser");
                        }
                        if (member.RoleAdminCourse != await _userManager.IsInRoleAsync(user, "AdminCourse"))
                        {
                            if (member.RoleAdminCourse == true) await _userManager.AddToRoleAsync(user, "AdminCourse");
                            else await _userManager.RemoveFromRoleAsync(user, "AdminCourse");
                        }
                        if (member.RoleAdminTeeTime != await _userManager.IsInRoleAsync(user, "AdminTeeTime"))
                        {
                            if (member.RoleAdminTeeTime == true) await _userManager.AddToRoleAsync(user, "AdminTeeTime");
                            else await _userManager.RemoveFromRoleAsync(user, "AdminTeeTime");
                        }
                        if (member.RoleAdminTour != await _userManager.IsInRoleAsync(user, "AdminTour"))
                        {
                            if (member.RoleAdminTeeTime == true) await _userManager.AddToRoleAsync(user, "AdminTour");
                            else await _userManager.RemoveFromRoleAsync(user, "AdminTour");
                        }
                    }


                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
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
            return View(member);
        }

        // GET: Member/Delete/5
        [Authorize(Roles = "AdminRoot,AdminUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .SingleOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRoot,AdminUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Member.SingleOrDefaultAsync(m => m.MemberId == id);
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.MemberId == id);
        }
    }
}
