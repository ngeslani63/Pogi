using Microsoft.AspNetCore.Mvc.Rendering;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public interface IMemberData
    {
        List<SelectListItem> getSelectList();

        IEnumerable<Member> getAll();
        IEnumerable<Member> getAll(string search);
        IEnumerable<Member> getActive();
        Member get(int memberId);

        Member getByEmailAddr(string emailAddr);

        Member getByEmailAddr2nd(string emailAddr);

    }
}
