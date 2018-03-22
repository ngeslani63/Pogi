using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface IMemberData
    {

        IEnumerable<Member> getAll();
        Member get(int memberId);

        Member getByEmailAddr(string emailAddr);

        Member getByEmailAddr2nd(string emailAddr);

    }
}
