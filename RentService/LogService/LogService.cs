using RentData;
using RentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentService
{
    public class LogService : ILogService
    {
        private readonly RentContext _context;
        public LogService(RentContext context)
        {
            _context = context;
        }

        public void AddLog(Log log)
        {
            _context.RentLogs.Add(new RentLog()
            {
                UserName = log.UserName,
                DateOper = log.DateOper,
                Page = log.Page,
                Oper = log.Oper
            });
            _context.SaveChanges();
        }

        
    }
}
