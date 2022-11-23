using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RentData;
using RentLibrary;

namespace RentAPI.Controllers
{
   // [Authorize]
    [ApiController]
    public class RentLogController : ControllerBase
    {
        Lazy<DBRentLog> dblog = new Lazy<DBRentLog>();
    
        [HttpPost]
        [Route("api/Log")]
        public void Post(Log log)
        {
            dblog.Value.AddLog(log);
        }
    }
}
