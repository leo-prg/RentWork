using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentData;
using RentDto;
using RentService;

namespace RentAPI.Controllers
{
    //[Authorize]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IRenterService _renterService;

        public AreasController(IAreaService areaService, IRenterService renterService)
        {
            _areaService = areaService;
            _renterService = renterService;
        }

        [HttpGet]
        [Route("api/Areas")]
        public AreaRect Get([FromQuery] int id, [FromQuery]  int x, [FromQuery] int y)
        {
            return _areaService.FindAreaInfoByPoint(id, x, y);
        }

        
        [HttpGet]
        [Route("api/Areas/GetAreaById")]
        public AreaRect GetAreaById([FromQuery] int id)
        {
            return _areaService.GetAreaById(id);
        }

        [HttpGet]
        [Route("api/Areas/GetAreasByRenterId")]
        public List<AreaRect> GetAreasByRenterId([FromQuery] int id)
        {
            return _areaService.GetAreasByRenterId(id);
        }

        [HttpGet]
        [Route("api/Areas/{id}")]
        public RenterRect Get(int id)
        {
            return id > 0 ? _areaService.GetAreaRenter(id) : null;
        }

        [HttpPost]
        [Route("api/Areas/NewArea")]
        public void Post([FromBody] AreaRect area)
        {
            _areaService.AddLayerArea(area);
        }

        [HttpPut]
        [Route("api/Areas")]
        public void Put(AreaRect area)
        {
            _areaService.UpdateAreaData(area, area);
        }

        // удаляем не площадь а арендатора - высвобождаем
        [HttpDelete]
        [Route("api/Areas")]
        public void Delete([FromQuery] int areaid)
        {
            _areaService.DeleteArea(areaid);
        }

        [HttpPut]
        [Route("api/Areas/NoRenter")]
        public void NoRenter([FromQuery] int areaid)
        {
            AreaRect area = _areaService.GetAreaById(areaid);
            _areaService.AddRenterToArea(area, null);
        }
      
        [HttpPut]
        [Route("api/Areas/AddRenter")]
        public void AddRenter( [FromQuery] int areaid, [FromQuery] int id)
        {
            AreaRect area = _areaService.GetAreaById(areaid);
            RenterRect renter = _renterService.GetRenterById(id);
            _areaService.AddRenterToArea(area, renter);
        }

        [HttpGet]
        [Route("api/Areas/GetLayerAreas")]
        public IEnumerable<AreaRect> GetLayerAreas([FromQuery] int id)
        {
            return _areaService.GetLayerAreas(id);
        }

        [HttpGet]
        [Route("api/Areas/GetAreaPoints")]
        public List<AreaPoint> GetAreaPoints([FromQuery] int id)
        {
            return _areaService.GetAreaPoints(id);
        }

        [HttpPost]
        [Route("api/Areas/AddAreaPoints")]
        public void AddAreaPoints([FromQuery] int areaId, [FromBody] List<AreaPoint> points)
        {
                _areaService.AddAreaPoints(areaId, points);
        }
        

    }
}
