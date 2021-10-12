using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RentDB;
using RentLibrary;

namespace RentAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class StaffController : ControllerBase
    {
        Lazy<DBStaff> dbr = new Lazy<DBStaff>();
        [HttpGet]
        [Route("api/Staff")]
        public IEnumerable<RentLibrary.Staff> Get()
        {
            return dbr.Value.GetAllStaff();
        }

        [HttpPost]
        [Route("api/Staff")]
        public void Post(RentLibrary.Staff staff)
        {
            dbr.Value.AddStaff(staff);
        }

        [HttpPut]
        [Route("api/Staff")]
        public void Put(RentLibrary.Staff newStaff)
        {
            dbr.Value.UpdateStaff(newStaff, newStaff);
        }

        [HttpDelete]
        [Route("api/Staff/{id}")]
        public void Delete(int id)
        {
            dbr.Value.DropStaff(id);
        }
    }
}
