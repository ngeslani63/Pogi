﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;
using Pogi.Models.CourseViewModels;
using Pogi.Services;

namespace Pogi.Controllers
{
    [Authorize(Roles = "Member")]
    public class CourseController : Controller
    {
        private readonly PogiDbContext _context;
        private readonly ICourseDetail _courseDetail;

        public SignInManager<ApplicationUser> _signInManager { get; }

        private UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public CourseController(ICourseDetail courseDetail, PogiDbContext context,
                        UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _courseDetail = courseDetail;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            var search = _session.GetString("SearchCourseName");
            if (search != null && search.Length > 0)
            {
                return View(await _context.Course.Where(r => r.CourseName.Contains(search)).OrderBy(r => r.CourseName).ToListAsync());
            }
            else
            {
                return View(await _context.Course.OrderBy(r => r.CourseName).ToListAsync());
            }
        }
        [Route("api/[controller]")]
        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Get(string search)
        {
            try
            {
                if (search != null && search.Length > 0)
                {
                    return Ok(await _context.Course.Where(r => r.CourseName.Contains(search)).OrderBy(r => r.CourseName).ToListAsync());
                }
                else
                {
                    return Ok(await _context.Course.OrderBy(r => r.CourseName).ToListAsync());
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get Courses " + ex.Message.ToString());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            if (search == null || search.Length == 0)
            {
                _session.Remove("SearchCourseName");
                return View(await _context.Course.OrderBy(r => r.CourseName).ToListAsync());
            }
            else
            {
                _session.SetString("SearchCourseName", search);
                return View(await _context.Course.Where(r => r.CourseName.Contains(search)).OrderBy(r => r.CourseName).ToListAsync());
            }
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            var model = new CourseEditViewModel();
            model.Course = new Course();
            List<CourseDetail> CourseDetails = new List<CourseDetail>(model.Course.NumTees);
            for (int i = 0; i < model.Course.NumTees; i++)
            {
                CourseDetail cd = new CourseDetail();

                switch (i)
                {
                    case 0:
                        cd.Color = "red";
                        cd.Rating = 0F;
                        cd.Slope = 114;
                        break;
                    case 1:
                        cd.Color = "white";
                        cd.Rating = 0F;
                        cd.Slope = 119;
                        break;
                    case 2:
                        cd.Color = "blue";
                        cd.Rating = 0F;
                        cd.Slope = 123;

                        break;
                    case 3:
                        cd.Color = "black";
                        cd.Rating = 0F;
                        cd.Slope = 130;
                        break;
                    default:
                        //CourseDetails[i].Color = "gold";
                        break;
                }
                CourseDetails.Add(cd);
            }
            model.CourseDetails = CourseDetails;
            return View(model);
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CourseId,CourseName,Street,City,State,Zip,Country,Phone,Par01,Par02,Par03,Par04,Par05,Par06,Par07,Par08,Par09,Par10,Par11,Par12,Par13,Par14,Par15,Par16,Par17,Par18,ParIn,ParOut,ParTotal,NumTees,CourseDetails")] CourseEditViewModel  courseEditViewModel)
        public async Task<IActionResult> Create([Bind("Course,CourseDetails")] CourseEditViewModel courseEditViewModel)

        {
            if (ModelState.IsValid)
            {
                var Course = courseEditViewModel.Course;
                Course.CreatedBy = User.Identity.Name;
                Course.CreatedTs = DateTime.Now;
                Course.LastUpdatedBy = User.Identity.Name;
                Course.LastUpdatedTs = DateTime.Now;

                _context.Add(Course);
                await _context.SaveChangesAsync();
                for (int i = 0; i < Course.NumTees; i++)
                {
                    var CourseDetail = courseEditViewModel.CourseDetails[i];
                    CourseDetail.CourseId = Course.CourseId;
                    CourseDetail.CreatedTs = DateTime.Now;
                    CourseDetail.CreatedBy = User.Identity.Name;
                    CourseDetail.LastUpdatedBy = User.Identity.Name;
                    CourseDetail.LastUpdatedTs = DateTime.Now;
                    _context.Add(CourseDetail);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseEditViewModel);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.SingleOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            var model = new CourseEditViewModel();
            model.Course = course;
            List<CourseDetail> CourseDetails = _courseDetail.getAll(course.CourseId);
            if (model.Course.NumTees == 0)
            {
                model.Course.NumTees = 3;
                for (int i = 0; i < model.Course.NumTees; i++)
                {
                    CourseDetail cd = new CourseDetail();

                    switch (i)
                    {
                        case 0:
                            cd.Color = "red";
                            cd.Rating = 0F;
                            cd.Slope = 114;
                            break;
                        case 1:
                            cd.Color = "white";
                            cd.Rating = 0F;
                            cd.Slope = 119;
                            break;
                        case 2:
                            cd.Color = "blue";
                            cd.Rating = 0F; ;
                            cd.Slope = 123;

                            break;
                        case 3:
                            cd.Color = "black";
                            cd.Rating = 0F;
                            cd.Slope = 130;
                            break;
                        default:
                            //CourseDetails[i].Color = "gold";
                            break;
                    }
                    CourseDetails.Add(cd);
                }
            }
            if (CourseDetails.Count < model.Course.NumTees)
            {
                for (int i = CourseDetails.Count; i < model.Course.NumTees; i++)
                {
                    CourseDetail cd = new CourseDetail();
                    cd.Color = "";
                    cd.Rating = 0F;
                    cd.Slope = 119;
                    CourseDetails.Add(cd);

                }
            }
            model.CourseDetails = CourseDetails;

            return View(model);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string submit, string addtee, [Bind("Course,CourseDetails")] CourseEditViewModel courseEditViewModel)
        {
            if (id != courseEditViewModel.Course.CourseId)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(addtee))
            {

                courseEditViewModel.Course.NumTees++;
                var model = courseEditViewModel;
                _context.Update(model.Course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = id });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    courseEditViewModel.Course.LastUpdatedBy = User.Identity.Name;
                    courseEditViewModel.Course.LastUpdatedTs = DateTime.Now;
                    _context.Update(courseEditViewModel.Course);
                    await _context.SaveChangesAsync();
                    foreach (var item in courseEditViewModel.CourseDetails)
                    {
                        item.CourseId = courseEditViewModel.Course.CourseId;
                        if (CourseDetailExists(item.CourseId, item.Color))
                        {
                            item.LastUpdatedBy = User.Identity.Name;
                            item.LastUpdatedTs = DateTime.Now;
                            _context.Update(item);
                        }
                        else
                        {
                            item.CreatedBy = User.Identity.Name;
                            item.CreatedTs = DateTime.Now;
                            item.LastUpdatedBy = User.Identity.Name;
                            item.LastUpdatedTs = DateTime.Now;
                            _context.Add(item);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(courseEditViewModel.Course.CourseId))
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
            return View(courseEditViewModel);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> DeleteColor(int? id, string Color)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            CourseDetail CourseDetail = _courseDetail.get((int)id, Color);
            _courseDetail.delete(CourseDetail);
            course.NumTees--;
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = id });
        }

        // GET: Course/Delete/5
        [Authorize(Roles = "AdminRoot,AdminCourse")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .SingleOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AdminRoot,AdminCourse")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _courseDetail.deleteAll(id);
            var course = await _context.Course.SingleOrDefaultAsync(m => m.CourseId == id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseId == id);
        }
        private bool CourseDetailExists(int courseId, string Color)
        {
            return _context.CourseDetail.Any(e => e.CourseId == courseId && e.Color == Color);
        }
    }
}
