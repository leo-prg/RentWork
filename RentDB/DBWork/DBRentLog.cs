using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentLibrary;
namespace RentDB
{
    public class DBRentLog
    {
        readonly Lazy<RentModel> _context = new Lazy<RentModel>();

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
