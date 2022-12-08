using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentDto;
using RentService;

namespace RentAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class LayersController : ControllerBase
    {
        private protected ILayerService _layerService;

        public LayersController(ILayerService layerService)
        {
            _layerService = layerService;
        }

        [HttpGet]
        [Route("api/Layers/{id}")]
        public IEnumerable<LayerRect> Get(int id)
        {
            return _layerService.GetLayersOfObject(id);
        }

        
        [HttpGet]
        [Route("api/ListLayers/{id}")]
        public IEnumerable<LayerRect> GetList(int id)
        {
            return _layerService.GetObjectLayersList(id);
        }

        [HttpPost]
        [Route("api/Layers/AddLayer")]
        // id объекта куда добавляем слой
        public void AddLayer([FromQuery] int id, [FromBody] LayerRect lr)
        {
            _layerService.AddObjectLayer(id, lr);
        }

        [HttpPut]
        [Route("api/Layers/UpdateLayer")]
        // id исходного слоя в котором производим изменения
        public void UpdateLayer([FromQuery] int id, [FromBody] LayerRect lr)
        {
            //Console.WriteLine(lr.LayerFile);
            _layerService.UpdateObjectLayer(id, lr);
        }

        [HttpDelete]
        [Route("api/Layers/{id}")]
        public void Delete(int id)
        {
            _layerService.DeleteLayer(id);
        }



    }
}
