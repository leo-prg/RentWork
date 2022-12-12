using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using RentData;
using RentDto;
using System.Web;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RentService;
using Microsoft.AspNetCore.Authorization;
using System.Windows.Forms;

namespace RentAPI.Controllers
{
    //[Authorize]
    [ApiController]
    public class ObjectsController : ControllerBase
    {

        private readonly IObjectService _objectService;
        private readonly IAreaService _areaService;

        public ObjectsController(IObjectService objectService, IAreaService areaService)
        {
            _objectService = objectService;
            _areaService = areaService;
        }

        [HttpGet, Authorize(Roles="Admin")]
        [Route("api/Objects")]
        public IEnumerable<ObjectRect> Get()
        {
            return _objectService.GetAllObjects();
        }

        [HttpGet]
        [Route("api/Objects/{id}")]
        public ActionResult<List<AreaRect>> Get(int id)
        {
                var oAreas = _areaService.GetObjectAreas(id);
            
                if (oAreas.Count != 0)
                {
                    return Ok(oAreas);
                }
                else
                {
                    return NoContent();
                }
        }

        [HttpGet]
        [Route("api/Objects/GetObjectById")]
        public ObjectRect GetObjectById([FromQuery]int id)
        {
            return _objectService.GetObjectById(id);
        }
        [HttpPost]
        [Route("api/Objects")]
        public void Post(ObjectRect objectRect)
        {
           _objectService.AddObject(objectRect);
        }

        [HttpPut]
        [Route("api/Objects")]
        public void Put(ObjectRect objectRect)
        {
           _objectService.UpdateObject(objectRect, objectRect);
        }
        [HttpDelete]
        [Route("api/Objects/{id}")]
        public void Delete(int id)
        {

        }
    }
}
