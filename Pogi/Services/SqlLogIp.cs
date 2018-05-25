using Pogi.Data;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Services
{
    public class SqlLogIp : ILogIp
    {
        private PogiDbContext _context;
        private IDateTime _dateTime;

        public SqlLogIp(PogiDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public string getUser(string IpAddr)
        {
            var IpGuest = _context.IpGuest.FirstOrDefault(e => e.IpAddr == IpAddr.Trim());
            if (IpGuest == null || IpGuest.UserName.Length == 0)
            {
                return "Guest";
            }
            else
            {
                return IpGuest.UserName;
            }
        }

        public void logIp(string IpAddr, string UserName)
        {
            IpGuest IpGuest = new IpGuest();
            IpGuest.IpAddr = IpAddr.Trim();
            IpGuest.UserName = UserName;
            IpGuest.LastUpdtTs = _dateTime.getNow();
            if (_context.IpGuest.Any(e => e.IpAddr == IpAddr))
            {
                _context.IpGuest.Update(IpGuest);
            }
            else
            {
                _context.IpGuest.Add(IpGuest);
            }
            _context.SaveChanges();
        }
    }
}
