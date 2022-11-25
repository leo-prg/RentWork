using RentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentService
{
    public interface ILayerService
    {
        void AddObjectLayer(int oId, LayerRect lRect);
        void UpdateObjectLayer(int oldId, LayerRect newL);
        List<LayerRect> GetLayersOfObject(ObjectRect oRect);
        List<LayerRect> GetLayersOfObject(int id);
        List<LayerRect> GetObjectLayersList(int id);
        void DeleteLayer(int id);
    }
}
