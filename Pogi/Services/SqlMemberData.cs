using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Data;
using Pogi.Entities;

namespace Pogi.Services
{
    public class SqlMemberData : IMemberData
    {
        private PogiDbContext _context;

        public SqlMemberData(PogiDbContext context)
        {
            _context = context;

        }
        public Member get(int memberId)
        {
            return _context.Member.FirstOrDefault(r => r.MemberId == memberId);
        }
        public Member getByEmailAddr(string emailAddr)
        {
            Member member = _context.Member.FirstOrDefault(r => r.EmailAddr1st == emailAddr);
            if (member == null ) member = _context.Member.FirstOrDefault(r => r.EmailAddr2nd == emailAddr);
            return member;
        }
        public Member getByEmailAddr2nd(string emailAddr)
        {
            return _context.Member.FirstOrDefault(r => r.EmailAddr2nd == emailAddr);
        }

        public IEnumerable<Member> getAll()
        {
            return _context.Member.OrderBy(r => r.LastName);
        }
        public IEnumerable<Member> getActive()
        {
            return _context.Member.Where(r => r.MemberStatus == MemberState.Member ||
            r.MemberStatus == MemberState.Junior).OrderBy(r => r.LastName);
        }

        public List<SelectListItem> getSelectList()
        {
            List<SelectListItem> memberList = new List<SelectListItem>();
            SelectListItem sl = new SelectListItem { Text = "All Members", Value = "0" };
            memberList.Add(sl);
            IEnumerable<Member> members = _context.Member.Where(r => r.MemberStatus == MemberState.Member || r.MemberStatus == MemberState.Junior).OrderBy(r => r.LastName);
            foreach (Member member in members)
            {
               sl = new SelectListItem { Text = member.LastName+", "+member.FirstName, Value = member.MemberId.ToString() };
                memberList.Add(sl);
            }
            return memberList;
        }
    }
}
