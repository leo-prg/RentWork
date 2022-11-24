using RentCommon;
using RentData;
using RentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentService.AreaService
{
    public class AreaService : IAreaService
    {
        private readonly RentContext _context;

        public AreaService(RentContext context)
        {
            _context = context;
        }

        public void AddAreaPoints(int areaId, List<AreaPoint> points)
        {
            // before we adding points we must delete old set of points
            foreach (var p in points)
            {
                _context.AreaPoints.RemoveRange(_context.AreaPoints.Where(a => a.Area.Id == areaId).AsEnumerable());
            }
            _context.SaveChanges();

            // so then we add new points array
            foreach (var p in points)
            {
                _context.AreaPoints.Add(new AreaPoints() { Area = _context.RentAreas.FirstOrDefault(x => x.Id == areaId),
                    PointNo = p.PointNo, PointX = p.PointX, PointY = p.PointY });
            }
            _context.SaveChanges();
        }

        public void AddLayerArea(AreaRect area)
        {
            var dbl = _context.RentLayers.Where(x => x.Id == area.LayerId).FirstOrDefault();

            Transform.PointsToDimensions(area.x1, area.y1, area.x2, area.y2, out int width, out int height);

            if (dbl != null)
            {
                _context.RentAreas.Add(new RentArea()
                {
                    Layer = dbl,
                    Name = area.AreaName,
                    X = area.x1,
                    Y = area.y1,
                    Width = width,
                    Height = height,
                    Square = area.Square,
                    Price = area.Price,
                    Cost = area.Cost
                });
                _context.SaveChanges();
            }
        }

        public List<AreaPoint> GetAreaPoints(int areaId)
        {
            List<AreaPoint> ap = new List<AreaPoint>();

            ap = _context.AreaPoints.
                Where(x => x.Area.Id == areaId).
                Select(x => new AreaPoint() { PointNo = x.PointNo, PointX = x.PointX, PointY = x.PointY }).ToList();
            return ap;
        }

    }
}
