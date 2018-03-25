﻿using System;
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
using Pogi.Models.ScoreViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    [Authorize(Roles = "Member")]
    public class ScoreController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly IScoreInfo _scoreInfo;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemberData _memberData;
        private readonly ICourseData _courseData;
        private readonly ICourseDetail _courseDetail;

        public ScoreController(PogiDbContext context, IScoreInfo scoreInfo, 
             SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager, IMemberData memberData, ICourseData courseData,
                ICourseDetail courseDetail)
        {
            _context = context;
            _scoreInfo = scoreInfo;
            _signInManager = signInManager;
            _userManager = userManager;
            _memberData = memberData;
            _courseData = courseData;
            _courseDetail = courseDetail;
        }

        // GET: Score
        [AllowAnonymous]
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            var model = new ScoreDisplayViewModel();
            model.ScoreInfos = _scoreInfo.getAll();

            return View(model);
            //return View(await _context.Score.ToListAsync());
        }

        // GET: Score/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .SingleOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // GET: Score/Create
        public IActionResult Create()
        {
            //var model = new Score();
            //model.CreatedBy = User.Identity.Name;
            //model.CreatedTs = DateTime.Now;
            //model.LastUpdatedBy = User.Identity.Name;
            //model.LastUpdatedTs = DateTime.Now;
            //return View(model);
            var model = new ScoreCreateViewModel();
            model.Courses = _courseData.getSelectList();
            model.Members = _memberData.getSelectList();
            model.Colors = _courseDetail.getSelectList(1);
            
            if (_signInManager.IsSignedIn(User))
            {
                model.EnteredBy = _memberData.getByEmailAddr(_userManager.GetUserName(User));
             }
            return View(model);
        }

        // POST: Score/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ScoreId,MemberId,CourseId,Color,EnteredById,ScoreDate,Hole01,Hole02,Hole03,Hole04,Hole05,Hole06,Hole07,Hole08,Hole09,Hole10,Hole11,Hole12,Hole13,Hole14,Hole15,Hole16,Hole17,Hole18,HoleIn,HoleOut,HoleTotal,CreatedBy,CreatedTs,LastUpdatedBy,LastUpdatedTs")] Score score)
        //public async Task<IActionResult> Create([Bind("ScoreId,MemberId,CourseId,Color,EnteredById,ScoreDate,Hole01,Hole02,Hole03,Hole04,Hole05,Hole06,Hole07,Hole08,Hole09,Hole10,Hole11,Hole12,Hole13,Hole14,Hole15,Hole16,Hole17,Hole18,HoleIn,HoleOut,HoleTotal,CreatedBy,CreatedTs,LastUpdatedBy,LastUpdatedTs")] Score score)
        public async Task<IActionResult> Create(ScoreCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime ts;
                if (!DateTime.TryParse(model.ScoreDate.ToString(), out ts))
                {
                    ModelState.AddModelError("ScoreDate", "Invalid Date");
                    model.Courses = _courseData.getSelectList();
                    model.Members = _memberData.getSelectList();
                    model.EnteredBy = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                    return View(model);
                }
                if (ts > DateTime.Now)
                {
                    ModelState.AddModelError("ScoreDate", "Please do not specify a future Date and Time");
                    model.Courses = _courseData.getSelectList();
                    model.Members = _memberData.getSelectList();
                    model.EnteredBy = _memberData.getByEmailAddr(_userManager.GetUserName(User));
                    return View(model);
                }
                //_context.Add(score);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                Score Score = new Score();
                Score.MemberId = model.MemberId;
                Score.CourseId = model.CourseId;
                Score.Color = model.Color;
                Score.EnteredById = model.EnteredBy.MemberId;
                Score.Hole01 = model.Hole01;
                Score.Hole02 = model.Hole02;
                Score.Hole03 = model.Hole03;
                Score.Hole04 = model.Hole04;
                Score.Hole05 = model.Hole05;
                Score.Hole06 = model.Hole06;
                Score.Hole07 = model.Hole07;
                Score.Hole08 = model.Hole08;
                Score.Hole09 = model.Hole09;
                Score.Hole10 = model.Hole10;
                Score.Hole11 = model.Hole11;
                Score.Hole12 = model.Hole12;
                Score.Hole13 = model.Hole13;
                Score.Hole14 = model.Hole14;
                Score.Hole15 = model.Hole15;
                Score.Hole16 = model.Hole16;
                Score.Hole17 = model.Hole17;
                Score.Hole18 = model.Hole18;
                Score.HoleIn = model.HoleIn;
                Score.HoleOut = model.HoleOut;
                Score.HoleTotal = model.HoleTotal;
                Score.CreatedBy = User.Identity.Name;
                Score.CreatedTs = DateTime.Now;
                Score.LastUpdatedBy = User.Identity.Name;
                Score.LastUpdatedTs = DateTime.Now;

                _context.Add(Score);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Score/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score.SingleOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }
            var model = new ScoreCreateViewModel();
            model.Courses = _courseData.getSelectList();
            model.Members = _memberData.getSelectList();
            model.MemberId = score.MemberId;
            model.CourseId = score.CourseId;
            model.Color = score.Color;
            model.EnteredBy = _memberData.get(score.EnteredById);
            model.Hole01 = score.Hole01;
            model.Hole02 = score.Hole02;
            model.Hole03 = score.Hole03;
            model.Hole04 = score.Hole04;
            model.Hole05 = score.Hole05;
            model.Hole06 = score.Hole06;
            model.Hole07 = score.Hole07;
            model.Hole08 = score.Hole08;
            model.Hole09 = score.Hole09;
            model.Hole10 = score.Hole10;
            model.Hole11 = score.Hole11;
            model.Hole12 = score.Hole12;
            model.Hole13 = score.Hole13;
            model.Hole14 = score.Hole14;
            model.Hole15 = score.Hole15;
            model.Hole16 = score.Hole16;
            model.Hole17 = score.Hole17;
            model.Hole18 = score.Hole18;
            model.HoleIn = score.HoleIn;
            model.HoleOut = score.HoleOut;
            model.HoleTotal = score.HoleTotal;

            if (_signInManager.IsSignedIn(User))
            {
                model.EnteredBy = _memberData.getByEmailAddr(_userManager.GetUserName(User));
            }
            return View(model);

        }

        // POST: Score/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ScoreId,MemberId,CourseId,Color,EnteredById,ScoreDate,Hole01,Hole02,Hole03,Hole04,Hole05,Hole06,Hole07,Hole08,Hole09,Hole10,Hole11,Hole12,Hole13,Hole14,Hole15,Hole16,Hole17,Hole18,HoleIn,HoleOut,HoleTotal,CreatedBy,CreatedTs,LastUpdatedBy,LastUpdatedTs")] Score score)
        public async Task<IActionResult> Edit(int id, ScoreCreateViewModel model)
        {
            if (id != model.ScoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Score Score = new Score();
                    Score.ScoreId = model.ScoreId;
                    Score.MemberId = model.MemberId;
                    Score.CourseId = model.CourseId;
                    Score.Color = model.Color;
                    Score.EnteredById = model.EnteredBy.MemberId;
                    Score.Hole01 = model.Hole01;
                    Score.Hole02 = model.Hole02;
                    Score.Hole03 = model.Hole03;
                    Score.Hole04 = model.Hole04;
                    Score.Hole05 = model.Hole05;
                    Score.Hole06 = model.Hole06;
                    Score.Hole07 = model.Hole07;
                    Score.Hole08 = model.Hole08;
                    Score.Hole09 = model.Hole09;
                    Score.Hole10 = model.Hole10;
                    Score.Hole11 = model.Hole11;
                    Score.Hole12 = model.Hole12;
                    Score.Hole13 = model.Hole13;
                    Score.Hole14 = model.Hole14;
                    Score.Hole15 = model.Hole15;
                    Score.Hole16 = model.Hole16;
                    Score.Hole17 = model.Hole17;
                    Score.Hole18 = model.Hole18;
                    Score.HoleIn = model.HoleIn;
                    Score.HoleOut = model.HoleOut;
                    Score.HoleTotal = model.HoleTotal;
                    Score.CreatedBy = User.Identity.Name;
                    Score.CreatedTs = DateTime.Now;
                    Score.LastUpdatedBy = User.Identity.Name;
                    Score.LastUpdatedTs = DateTime.Now;
                    _context.Update(Score);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreExists(model.ScoreId))
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
            return View(model);
        }

        // GET: Score/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .SingleOrDefaultAsync(m => m.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // POST: Score/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var score = await _context.Score.SingleOrDefaultAsync(m => m.ScoreId == id);
            _context.Score.Remove(score);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreExists(int id)
        {
            return _context.Score.Any(e => e.ScoreId == id);
        }
    }
}
