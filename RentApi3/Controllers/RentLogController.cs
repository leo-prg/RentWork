using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RentData;
using RentDto;
using RentService;

namespace RentAPI.Controllers
{
   // [Authorize]
    [ApiController]
    public class RentLogController : ControllerBase
    {
        private readonly ILogService _logService;

        public RentLogController(ILogService logService)
        {
            _logService =  logService;

        }

        [HttpPost]
        [Route("api/Log")]
        public void Post(Log log)
        {
           _logService.AddLog(log);
        }
    }
}
