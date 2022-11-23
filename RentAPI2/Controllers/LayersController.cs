using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RentData;
using RentDto;

namespace RentAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class LayersController : ControllerBase
    {
        Lazy<DBLayer> dbl = new Lazy<DBLayer>();

        [HttpGet]
        [Route("api/Layers/{id}")]
        public IEnumerable<LayerRect> Get(int id)
        {
            return dbl.Value.GetLayersOfObject(id);
        }

        
        [HttpGet]
        [Route("api/ListLayers/{id}")]
        public IEnumerable<LayerRect> GetList(int id)
        {
            return dbl.Value.GetObjectLayersList(id);
        }

        [HttpPost]
        [Route("api/Layers/AddLayer")]
        // id объекта куда добавляем слой
        public void AddLayer([FromQuery] int id, [FromBody] LayerRect lr)
        {
            dbl.Value.AddObjectLayer(id, lr);
        }

        [HttpPut]
        [Route("api/Layers/UpdateLayer")]
        // id исходного слоя в котором производим изменения
        public void UpdateLayer([FromQuery] int id, [FromBody] LayerRect lr)
        {
            //Console.WriteLine(lr.LayerFile);
            dbl.Value.UpdateObjectLayer(id, lr);
        }

        [HttpDelete]
        [Route("api/Layers/{id}")]
        public void Delete(int id)
        {
            dbl.Value.DeleteLayer(id);
        }



    }
}
