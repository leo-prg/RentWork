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
    public class RentRatesController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RentRatesController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet]
        [Route("api/RentRates")]
        public IEnumerable<Rates> Get([FromQuery] int id)
        {
            return _rateService.GetRentRates(id);
        }


        [HttpPut]
        [Route("api/RentRates/AddRate")]
        public void AddRate([FromBody()] Rates rate, [FromQuery]int area_id)
        {
            _rateService.AddRentRate(rate, area_id);
        }

        [HttpPut]
        [Route("api/RentRates/UpdateRate")]
        public void UpdateRate(Rates urate)
        {
            _rateService.UpdateRate(urate, urate);
        }

        [HttpDelete]
        [Route("api/RentRates")]
        public void Delete([FromQuery] int id)
        {
            _rateService.DropRate(id);
        }
    }
}
