using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public class SqlActivity : IActivity
    {
        private PogiDbContext _context;
        private readonly IMemberData _memberData;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTime _dateTime;

        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SqlActivity(PogiDbContext context, IMemberData sqlMemberData,
                        UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        RoleManager<IdentityRole> roleManager,
                        IHttpContextAccessor httpContextAccessor,
                        IDateTime dateTime)
        {
            _context = context;
            _memberData = sqlMemberData;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _dateTime = dateTime;
            
        }

        public void logActivity(string userName, string Action)
        {
            Pogi.Entities.Log2 Activity = new Pogi.Entities.Log2();
            Activity.createdTS = _dateTime.getNow();
            Activity.Action = Action;
            Activity.ipAddr = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (userName != null && userName.Length > 0)
            {
                Member member = _memberData.getByEmailAddr(userName.Trim().ToLower());
                if (member == null) { member = _memberData.getByEmailAddr2nd(userName.Trim().ToLower()); }
                if (member != null)
                {
                    Activity.MemberId = member.MemberId;
                    Activity.UserName = member.FirstName + " " + member.LastName;
                }
            }

            _context.Log2.Add(Activity);
            _context.SaveChanges();
        }

        public IQueryable<Log2> getActivity(int days)
        {
            if (days > 14) days = 7; // set Max 
            DateTime now = _dateTime.getNow();
            DateTime start = now.AddDays(days*-1).Date;
            return _context.Log2.Where(r => r.createdTS >= start).OrderByDescending(r => r.createdTS);
        }
    }
}
