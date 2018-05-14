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
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public ClaimsPrincipal User { get; private set; }

        public SqlActivity(PogiDbContext context, IMemberData sqlMemberData,
                        UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        RoleManager<IdentityRole> roleManager,
                        IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _memberData = sqlMemberData;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;

        }

        public void logActivity(string Action)
        {
            Pogi.Entities.Activity Activity = new Pogi.Entities.Activity();
            Activity.Action = Action;
            Activity.ipAddr = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (User != null &&_signInManager.IsSignedIn(User))
            {
                var userName = _userManager.GetUserName(User);
                Member member = _memberData.getByEmailAddr(userName.Trim().ToLower());
                if (member == null) { member = _memberData.getByEmailAddr2nd(userName.Trim().ToLower()); }
                if (member != null)
                {
                    Activity.MemberId = member.MemberId;
                    Activity.UserName = member.FirstName + " " + member.LastName;
                }
            }
            _context.Activity.Add(Activity);
            _context.SaveChanges();
        }
    }
}
