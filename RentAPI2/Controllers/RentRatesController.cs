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
    public class RentRatesController : ControllerBase
    {
        Lazy<DBRentRates> dbr = new Lazy<DBRentRates>();
        
        [HttpGet]
        [Route("api/RentRates")]
        public IEnumerable<Rates> Get([FromQuery] int id)
        {
            return dbr.Value.GetRentRates(id);
        }


        [HttpPut]
        [Route("api/RentRates/AddRate")]
        public void AddRate([FromBody()] Rates rate, [FromQuery]int area_id)
        {
            dbr.Value.AddRentRate(rate, area_id);
        }

        [HttpPut]
        [Route("api/RentRates/UpdateRate")]
        public void UpdateRate(Rates urate)
        {
            dbr.Value.UpdateRate(urate, urate);
        }

        [HttpDelete]
        [Route("api/RentRates")]
        public void Delete([FromQuery] int id)
        {
            dbr.Value.DropRate(id);
        }
    }
}
