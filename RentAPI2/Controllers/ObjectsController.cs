using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using RentDB;
using RentLibrary;
using System.Web;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace RentAPI.Controllers
{
    //[Authorize]
    [ApiController]
    public class ObjectsController : ControllerBase
    {
        Lazy<DBObject> dbo = new Lazy<DBObject>();
        Lazy<DBArea> dba = new Lazy<DBArea>();

        [HttpGet]
        [Route("api/Objects")]
        public IEnumerable<ObjectRect> Get()
        {
            return dbo.Value.GetAllObjects();
        }

        [HttpGet]
        [Route("api/Objects/{id}")]
        public List<AreaRect> Get(int id)
        {
            return dba.Value.GetObjectAreas(id);
        }

        [HttpGet]
        [Route("api/Objects/GetObjectById")]
        public ObjectRect GetObjectById([FromQuery]int id)
        {
            return dbo.Value.GetObjectById(id);
        }
        [HttpPost]
        [Route("api/Objects")]
        public void Post(ObjectRect objectRect)
        {
            dbo.Value.AddObject(objectRect);
        }

        [HttpPut]
        [Route("api/Objects")]
        public void Put(ObjectRect objectRect)
        {
            dbo.Value.UpdateObject(objectRect, objectRect);
        }
        [HttpDelete]
        [Route("api/Objects/{id}")]
        public void Delete(int id)
        {
        }
    }
}
