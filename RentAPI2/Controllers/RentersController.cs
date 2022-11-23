using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using RentData;
using RentDto;
using Microsoft.AspNetCore.Mvc;

namespace RentAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class RentersController : ControllerBase
    {
        Lazy<DBRenter> dbr = new Lazy<DBRenter>();

        [HttpGet]
        [Route("api/Renters")]
        public IEnumerable<RenterRect> Get()
        {
            return dbr.Value.GetAllRenters();
        }

        [HttpGet]
        [Route("api/Renters/{id}")]
        public RenterRect Get(int id)
        {
            return dbr.Value.GetRenterById(id);
        }

        [HttpPost]
        [Route("api/Renters/KillFile/{id}")]
        public void KillFile(int id)
        {
            dbr.Value.KillFile(id);
        }

        [HttpGet]
        [Route("api/Renters/DownloadFile/{id}")]
        public IActionResult DownloadFile(int id)
        {
            var file = dbr.Value.GetFileById(id);
            if (file != null)
            {
              return File(file, "application/octet-stream");
            }
            else
            {
                return new NoContentResult();
            }    
        }

        [HttpGet]
        [Route("api/Renters/GetRenterFileList")]
        public IEnumerable<RenterFileList> GetRenterFileList([FromQuery] int id)
        {
            return dbr.Value.GetRenterFileList(id);
        }

        [HttpPost]
        [Route("api/Renters")]
        public void Post(RenterRect newRenter)
        {
            dbr.Value.AddRenter(newRenter);
        }

        [HttpPut]
        [Route("api/Renters")]
        public void Put(RenterRect newRenter)
        {
            dbr.Value.UpdateRenter(newRenter, newRenter);
        }

        [HttpPost]
        [Route("api/Renters/AddRenterFile")]
        public void AddRenterFile([FromQuery] int id )
        {
            var httpRequest = HttpContext.Request.Form.Files;
            byte[] fileData = null;
            dynamic d = HttpContext.Request.Form["rf"];
            using (var binaryReader = new BinaryReader(httpRequest[0].OpenReadStream()))
            {
                fileData = binaryReader.ReadBytes((int)httpRequest[0].OpenReadStream().Length);
            }

            dbr.Value.AddRenterFile(id, fileData, JsonSerializer.Deserialize<RenterFileList>(d)); 
        }
        
        [HttpDelete]
        [Route("api/Renters")]
        public void Delete(int id)
        {
            RenterRect rr = dbr.Value.GetRenterById(id);
            dbr.Value.DropRenter(rr);
        }
    }
}
