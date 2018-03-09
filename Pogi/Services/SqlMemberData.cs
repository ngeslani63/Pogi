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

        public IEnumerable<Member> getAll()
        {
            return _context.Member.OrderBy(r => r.LastName);
        }
    }
}
