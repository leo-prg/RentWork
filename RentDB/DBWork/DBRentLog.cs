using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentLibrary;
namespace RentData
{
    public class DBRentLog
    {
        readonly Lazy<RentContext> _context = new Lazy<RentContext>();

        public void AddLog(Log log)
        {
            _context.Value.RentLogs.Add(new RentLog()
            {
                UserName = log.UserName,
                DateOper = log.DateOper,
                Page = log.Page,
                Oper = log.Oper
            });
            _context.Value.SaveChanges();
        }



    }
}
