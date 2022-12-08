using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentData;
using RentDto;
using RentService;

namespace RentAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        [Route("api/Staff")]
        public IEnumerable<StaffDto> Get()
        {
            return _staffService.GetAllStaff();
        }

        [HttpPost]
        [Route("api/Staff")]
        public void Post(StaffDto staff)
        {
            _staffService.AddStaff(staff);
        }

        [HttpPut]
        [Route("api/Staff")]
        public void Put(StaffDto newStaff)
        {
            _staffService.UpdateStaff(newStaff);
        }

        [HttpDelete]
        [Route("api/Staff/{id}")]
        public void Delete(int id)
        {
            _staffService.DropStaff(id);
        }
    }
}
