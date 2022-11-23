using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentData;
using RentLibrary;

namespace RentAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class AreasController : ControllerBase
    {
        Lazy<DBArea> dba = new Lazy<DBArea>();
        readonly Lazy<RentContext> context = new Lazy<RentContext>();
        Lazy<DBRenter> dbr = new Lazy<DBRenter>();

        [HttpGet]
        [Route("api/Areas")]
        public AreaRect Get([FromQuery] int id, [FromQuery]  int x, [FromQuery] int y)
        {
            return dba.Value.FindAreaInfoByPoint(id, x, y);
        }

        
        [HttpGet]
        [Route("api/Areas/GetAreaById")]
        public AreaRect GetAreaById([FromQuery] int id)
        {
            return dba.Value.GetAreaById(id);
        }

        [HttpGet]
        [Route("api/Areas/GetAreasByRenterId")]
        public List<AreaRect> GetAreasByRenterId([FromQuery] int id)
        {
            return dba.Value.GetAreasByRenterId(id);
        }

        [HttpGet]
        [Route("api/Areas/{id}")]
        public RenterRect Get(int id)
        {
            return id > 0 ? dba.Value.GetAreaRenter(id) : null;
        }

        [HttpPost]
        [Route("api/Areas/NewArea")]
        public void Post([FromBody] AreaRect area)
        {
            dba.Value.AddLayerArea(area);
        }

        [HttpPut]
        [Route("api/Areas")]
        public void Put(AreaRect area)
        {
            dba.Value.UpdateAreaData(area, area);
        }

        // удаляем не площадь а арендатора - высвобождаем
        [HttpDelete]
        [Route("api/Areas")]
        public void Delete([FromQuery] int areaid)
        {
            dba.Value.DeleteArea(areaid);
        }

        [HttpPut]
        [Route("api/Areas/NoRenter")]
        public void NoRenter([FromQuery] int areaid)
        {
            AreaRect area = dba.Value.GetAreaById(areaid);
            dba.Value.AddRenterToArea(area, null);
        }
      
        [HttpPut]
        [Route("api/Areas/AddRenter")]
        public void AddRenter( [FromQuery] int areaid, [FromQuery] int id)
        {
            AreaRect area = dba.Value.GetAreaById(areaid);
            RenterRect renter = dbr.Value.GetRenterById(id);
            dba.Value.AddRenterToArea(area, renter);
        }

        [HttpGet]
        [Route("api/Areas/GetLayerAreas")]
        public IEnumerable<AreaRect> GetLayerAreas([FromQuery] int id)
        {
            return dba.Value.GetLayerAreas(id);
        }

        [HttpGet]
        [Route("api/Areas/GetAreaPoints")]
        public List<AreaPoint> GetAreaPoints([FromQuery] int id)
        {
            return dba.Value.GetAreaPoints(id);
        }

        [HttpPost]
        [Route("api/Areas/AddAreaPoints")]
        public void AddAreaPoints([FromQuery] int areaId, [FromBody] List<AreaPoint> points)
        {
                dba.Value.AddAreaPoints(areaId, points);
        }
        

    }
}
