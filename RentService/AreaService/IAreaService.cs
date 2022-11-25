using System;
using System.Collections.Generic;
using System.Text;
using RentDto;

namespace RentService
{
    public interface IAreaService
    {
        void AddLayerArea(AreaRect area);
        void AddAreaPoints(int areaId, List<AreaPoint> points);
        List<AreaPoint> GetAreaPoints(int areaId);
        AreaRect GetAreaById(int id);
        List<AreaRect> GetAreasByRenterId(int id);
        List<AreaRect> GetLayerAreas(LayerRect layer);
        List<AreaRect> GetLayerAreas(int id);
        List<AreaRect> GetObjectAreas(int id);
        string FindAreaByPoint(LayerRect layer, int x, int y);
        AreaRect FindAreaInfoByPoint(int id, int x, int y);
        AreaRect FindAreaByName(string name);
        void AddRenterToArea(AreaRect area, RenterRect renter);
        RenterRect GetAreaRenter(AreaRect area);
        RenterRect GetAreaRenter(int id);
        void UpdateAreaData(AreaRect old, AreaRect _new);
        void DeleteArea(int areaId);




    }
}
