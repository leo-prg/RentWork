using RentCommon;
using RentData;
using RentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RentService
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

        public void AddRenterToArea(AreaRect area, RenterRect renter)
        {
            var ar = _context.RentAreas.Where(a => a.Id == area.Id).Select(c => c).FirstOrDefault();
            if (renter != null)
            {
                var rent = _context.Renters.Where(r => r.Id == renter.Id).Select(c => c).FirstOrDefault();
                ar.Renter = rent;
            }
            else
            {
                _context.Entry(ar).Reference(r => r.Renter).CurrentValue = null;
                ar.Price = 0;
                ar.Cost = 0;
            }
            _context.SaveChanges();
        }

        public void DeleteArea(int areaId)
        {
            // удалим точки этой площади из базы
            var ap = _context.AreaPoints.Where(a => a.Area.Id == areaId).AsEnumerable();
            if (ap != null)
            {
                _context.AreaPoints.RemoveRange(ap);
                _context.SaveChanges();
            }

            // найдем и удалим ставки аренды по этой площади, если удаляем значит они не нужны
            var ar = _context.RentRates.Where(a => a.Area.Id == areaId).AsEnumerable();
            if (ar != null)
            {
                _context.RentRates.RemoveRange(ar);
                _context.SaveChanges();
            }


            // найдем объект в базе с таким ид 
            _context.RentAreas.Remove(_context.RentAreas.Find(areaId));
            _context.SaveChanges();
        }

        public AreaRect FindAreaByName(string name)
        {
            return _context.RentAreas
               .Where(a => a.Name == name)
               .Select(a => new AreaRect()
               {
                   Id = a.Id,
                   AreaName = name,
                   RenterName = "(" + a.Renter.Contract + ") " + a.Renter.RenterName,
                   x1 = a.X,
                   y1 = a.Y,
                   x2 = (a.X + a.Width),
                   y2 = (a.Y + a.Height),
                   Square = a.Square,
                   Price = a.Price,
                   Cost = a.Cost,
                   Currency = a.Currency
               }).FirstOrDefault();
        }

        public string FindAreaByPoint(LayerRect layer, int x, int y)
        {
            return _context.RentAreas
                .Where(a => x >= a.X && x <= (a.X + a.Width) && y >= a.Y && y <= (a.Y + a.Height) && a.Layer.Id == layer.Id)
                .Select(a => a.Name).FirstOrDefault();
        }
        
        public AreaRect FindAreaInfoByPoint(int id, int x, int y)
        {
            //// Выбираем площади указанного слоя, чтоб не обрабатывать все
            List<AreaRect> areaRects = _context.RentAreas.Where(w => w.Layer.Id == id).Select(s => new AreaRect()
            {
                Id = s.Id,
                AreaName = s.Name,
                RenterName = "(" + s.Renter.Contract + ") " + s.Renter.RenterName,
                Square = s.Square,
                Price = s.Price,
                Cost = s.Cost,
                Currency = s.Currency,
                isRented = (s.Renter != null)

            }).ToList();
            // Выбираем точки площадей по очереди

            foreach (var a in areaRects)
            {
                List<AreaPoint> apl = _context.AreaPoints.Where(s => s.Area.Id == a.Id).Select(s => new AreaPoint() { PointX = s.PointX, PointY = s.PointY }).ToList();
                if (RentCommon.Transform.PointInPolygon(apl, new AreaPoint() { PointX = x, PointY = y }))
                {
                    return a;
                }
                else
                {
                    continue;
                }
            }
            return null;
           
        }
        
        public AreaRect GetAreaById(int id)
        {
            var area = _context.RentAreas.Where(x => x.Id == id).Select(x => new AreaRect()
            {
                Id = x.Id,
                ObjectName = x.Layer.Object.Name,
                LayerName = x.Layer.Name,
                AreaName = x.Name,
                RenterName = "( дог. №" + x.Renter.Contract + " ) " + x.Renter.RenterName,
                x1 = x.X,
                y1 = x.Y,
                x2 = Math.Abs(x.Width + x.X),
                y2 = Math.Abs(x.Height + x.Y),
                Square = x.Square,
                Price = x.Price,
                Cost = x.Cost,
                Currency = x.Currency,
                isRented = x.Renter != null
            }).FirstOrDefault();
            
            return area;
        }

        public List<AreaPoint> GetAreaPoints(int areaId)
        {
            List<AreaPoint> ap = new List<AreaPoint>();

            ap = _context.AreaPoints.
                Where(x => x.Area.Id == areaId).
                Select(x => new AreaPoint() { PointNo = x.PointNo, PointX = x.PointX, PointY = x.PointY }).ToList();
            return ap;
        }

        public RenterRect GetAreaRenter(AreaRect area)
        {
            return _context.RentAreas
                .Where(a => a.Id == area.Id)
                .Select(r => new RenterRect()
                {
                    RenterName = r.Renter.RenterName,
                    Contract = r.Renter.Contract,
                    StartDate = (DateTime?)r.Renter.StartDate ?? DateTime.Now,
                    EndDate = (DateTime?)r.Renter.EndDate ?? DateTime.Now,
                    ContactPerson = r.Renter.ContactPerson,
                    ContactPhone = r.Renter.ContactPhone,
                    Annotation = r.Renter.Annotation
                }).FirstOrDefault();
        }

        public RenterRect GetAreaRenter(int id)
        {
            RenterRect rr = _context.RentAreas
              .Where(a => a.Id == id)
              .Select(r => new RenterRect()
              {
                  Id = r.Renter.Id,
                  RenterName = r.Renter.RenterName,
                  Contract = r.Renter.Contract,
                  StartDate = (DateTime?)r.Renter.StartDate ?? DateTime.Now,
                  EndDate = (DateTime?)r.Renter.EndDate ?? DateTime.Now,
                  ContactPerson = r.Renter.ContactPerson,
                  ContactPhone = r.Renter.ContactPhone,
                  Annotation = r.Renter.Annotation
              }).FirstOrDefault();

            if (rr != null) { return rr; }
            else { return null; }
        }

        public List<AreaRect> GetAreasByRenterId(int id)
        {
                var area = _context.RentAreas
                .Where(r => r.Renter.Id == id)
                .Select(x => new AreaRect()
                {
                    Id = x.Id,
                    ObjectName = x.Layer.Object.Name,
                    LayerName = x.Layer.Name,
                    AreaName = x.Name,
                    RenterName = "(" + x.Renter.Contract + ") " + x.Renter.RenterName,
                    x1 = x.X,
                    y1 = x.Y,
                    x2 = Math.Abs(x.Width + x.X),
                    y2 = Math.Abs(x.Height + x.Y),
                    Square = x.Square,
                    Price = x.Price,
                    Cost = x.Cost,
                    Currency = x.Currency,
                    isRented = x.Renter != null
                }).ToList();
            return area;
        }

        public List<AreaRect> GetLayerAreas(LayerRect layer)
        {
            var dbl = _context.RentLayers.Where(x => x.Id == layer.Id).FirstOrDefault();
            return _context.RentAreas
                .Where(x => x.Layer.Id == dbl.Id)
                .Select(x => new AreaRect()
                {
                    Id = x.Id,
                    LayerName = x.Layer.Name,
                    AreaName = x.Name,
                    RenterName = "(" + x.Renter.Contract + ") " + x.Renter.RenterName,
                    x1 = x.X,
                    y1 = x.Y,
                    x2 = Math.Abs(x.Width + x.X),
                    y2 = Math.Abs(x.Height + x.Y),
                    Square = x.Square,
                    Price = x.Price,
                    Cost = x.Cost,
                    Currency = x.Currency,
                    isRented = x.Renter != null
                }).ToList();
        }

        public List<AreaRect> GetLayerAreas(int id)
        {
            var dbl = _context.RentLayers.Where(x => x.Id == id).FirstOrDefault();
            
            return _context.RentAreas
                .Where(x => x.Layer.Id == dbl.Id)
                .Select(x => new AreaRect()
                {
                    Id = x.Id,
                    LayerName = x.Layer.Name,
                    AreaName = x.Name,
                    RenterName = "(" + x.Renter.Contract + ") " + x.Renter.RenterName,
                    x1 = x.X,
                    y1 = x.Y,
                    x2 = Math.Abs(x.Width + x.X),
                    y2 = Math.Abs(x.Height + x.Y),
                    Square = x.Square,
                    Price = x.Price,
                    Cost = x.Cost,
                    Currency = x.Currency,
                    isRented = x.Renter != null
                }).ToList();
        }

        public List<AreaRect> GetObjectAreas(int id)
        {
            List<AreaRect> list =
            _context.RentAreas
                .Where(x => x.Layer.Object.Id == id)
                .Select(x => new AreaRect()
                {
                    Id = x.Id,
                    LayerName = x.Layer.Name,
                    AreaName = x.Name,
                    RenterName = "(" + x.Renter.Contract + ") " + x.Renter.RenterName,
                    x1 = x.X,
                    y1 = x.Y,
                    x2 = Math.Abs(x.Width + x.X),
                    y2 = Math.Abs(x.Height + x.Y),
                    Square = x.Square,
                    Price = x.Price,
                    Cost = x.Cost,
                    Currency = x.Currency,
                    isRented = x.Renter != null
                }).OrderBy(x => x.LayerName).ThenBy(y => y.AreaName).ToList();

            return list.Distinct().ToList();
        }

        public void UpdateAreaData(AreaRect old, AreaRect _new)
        {
            // найдем объект в базе с таким ид 
            var _old = _context.RentAreas.Where(x => x.Id == old.Id).FirstOrDefault();
            var _layer = _context.RentLayers.Find(_new.LayerId);
            _old.Layer = _layer;
            _old.Name = _new.AreaName;
            _old.Price = _new.Price;
            _old.Square = _new.Square;
            _old.Cost = _new.Cost;
            _old.Currency = _new.Currency;
            _context.SaveChanges();
        }

    }
}
