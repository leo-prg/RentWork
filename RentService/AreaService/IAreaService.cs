using System;
using System.Collections.Generic;
using System.Text;
using RentDto;

namespace RentService.AreaService
{
    public interface IAreaService
    {
        void AddLayerArea(AreaRect area);
        void AddAreaPoints(int areaId, List<AreaPoint> points);
        List<AreaPoint> GetAreaPoints(int areaId);
        AreaRect GetAreaById(int id);



    }
}
