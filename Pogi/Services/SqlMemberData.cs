using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
