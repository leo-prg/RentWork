using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Services;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using RentCommon;
using RentLibrary;

namespace RentDB
{
    public class DBArea
    {
        readonly Lazy<RentModel> context = new Lazy<RentModel>();

        public void AddLayerArea(AreaRect area)
        {
            
            var dbl = context.Value.RentLayers.Where(x => x.Id == area.LayerId).FirstOrDefault();

            Transform.PointsToDimensions(area.x1, area.y1, area.x2, area.y2, out int width, out int height);

            if (dbl != null)
            {
                context.Value.RentAreas.Add(new RentArea()
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
                context.Value.SaveChanges();
            }
        }

        public void AddAreaPoints(int areaId, List<AreaPoint> points)
        {
            // before we adding points we must delete old set of points
            foreach (var p in points)
            {
                context.Value.AreaPoints.RemoveRange(context.Value.AreaPoints.Where(a => a.Area.Id == areaId).AsEnumerable());
            }
            context.Value.SaveChanges();

            // so then we add new points array
            foreach (var p in points)
            {
                context.Value.AreaPoints.Add(new AreaPoints() {  Area = context.Value.RentAreas.FirstOrDefault(x => x.Id == areaId), PointNo  = p.PointNo, PointX = p.PointX, PointY = p.PointY});    
            }
            context.Value.SaveChanges();
        }

        public List<AreaPoint> GetAreaPoints(int areaId)
        {
            List<AreaPoint> ap = new List<AreaPoint>();
            
            ap = context.Value.AreaPoints.
                Where(x=>x.Area.Id == areaId).
                Select(x=> new AreaPoint(){ PointNo = x.PointNo, PointX = x.PointX, PointY = x.PointY}).ToList();
            return ap;
        }

        public AreaRect GetAreaById(int id)
        {
            return context.Value.RentAreas.Where(x => x.Id == id).Select(x => new AreaRect()
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
                isRented = x.Renter != null
            }).FirstOrDefault();
        }

        public List<AreaRect> GetAreasByRenterId(int id)
        {

            
            return context.Value.RentAreas
                .Where(x => x.Renter.Id == id)
                .Select(x => new AreaRect()
                {
                    Id = x.Id,
                    ObjectName  = x.Layer.Object.Name,
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
                    isRented = x.Renter != null
                }).ToList();
        
        }


        public List<AreaRect> GetLayerAreas(LayerRect layer)
        {
            var dbl = context.Value.RentLayers.Where(x => x.Id == layer.Id).FirstOrDefault();
            return context.Value.RentAreas
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
                    isRented = x.Renter != null
                }).ToList();
        }

        public List<AreaRect> GetLayerAreas(int id)
        {
            var dbl = context.Value.RentLayers.Where(x => x.Id == id).FirstOrDefault();
            return context.Value.RentAreas
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
                    isRented = x.Renter != null
                }).ToList();
        }

        public List<AreaRect> GetObjectAreas(int id)
        {
            List<AreaRect> list = 
            context.Value.RentAreas
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
                    isRented = x.Renter != null
                }).OrderBy(x => x.LayerName).ThenBy(y => y.AreaName).ToList();

            return list.Distinct().ToList();
        }


        public string FindAreaByPoint(LayerRect layer ,int x, int y)
        {
            return context.Value.RentAreas
                .Where(a => x >= a.X && x <= (a.X + a.Width) && y >= a.Y && y <= (a.Y + a.Height) && a.Layer.Id == layer.Id)
                .Select(a => a.Name).FirstOrDefault(); 
        }

        public AreaRect FindAreaInfoByPoint(int id, int x, int y)
        {
            //// Выбираем площади указанного слоя, чтоб не обрабатывать все
            List<AreaRect> areaRects = context.Value.RentAreas.Where(w => w.Layer.Id == id).Select(s => new AreaRect()
            {
                Id = s.Id,
                AreaName = s.Name,
                RenterName = "(" + s.Renter.Contract + ") " + s.Renter.RenterName,
                Square = s.Square,
                Price = s.Price,
                Cost = s.Cost,
                isRented = (s.Renter != null)

            }).ToList();
            // Выбираем точки площадей по очереди

            foreach (var a in areaRects)
            {
                List<AreaPoint> apl = context.Value.AreaPoints.Where(s => s.Area.Id == a.Id).Select(s => new AreaPoint() { PointX = s.PointX, PointY = s.PointY }).ToList();
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

            //// определение для прямоугольника
            //return context.Value.RentAreas
            //    .Where(a => x >= a.X && x <= (a.X + a.Width) && y >= a.Y && y <= (a.Y + a.Height) && a.Layer.Id == id)
            //    .Select(a => new AreaRect()
            //    {
            //        Id = a.Id,
            //        AreaName = a.Name,
            //        RenterName = "(" + a.Renter.Contract + ") " + a.Renter.RenterName,
            //        x1 = a.X,
            //        y1 = a.Y,
            //        x2 = (a.X + a.Width),
            //        y2 = (a.Y + a.Height),
            //        Square = a.Square,
            //        Price = a.Price,
            //        Cost = a.Cost,
            //        isRented = (a.Renter != null)

            //    }).FirstOrDefault();
        }


        public AreaRect FindAreaByName(string name)
        {
            return context.Value.RentAreas
                .Where(a => a.Name == name)
                .Select(a => new AreaRect() { Id = a.Id, AreaName = name,
                    RenterName = "(" + a.Renter.Contract + ") " + a.Renter.RenterName,
                    x1 = a.X, y1 = a.Y, x2 = (a.X + a.Width), y2 = (a.Y + a.Height),
                   Square = a.Square,
                    Price = a.Price,
                    Cost = a.Cost
                }).FirstOrDefault();
        }

        public void AddRenterToArea(AreaRect area, RenterRect renter)
        {
            var ar = context.Value.RentAreas.Where(a => a.Id == area.Id).Select(c => c).FirstOrDefault();
            if (renter != null)
            {
                var rent = context.Value.Renters.Where(r => r.Id == renter.Id).Select(c => c).FirstOrDefault();
                ar.Renter = rent;
            }
            else 
            {
                context.Value.Entry(ar).Reference(r => r.Renter).CurrentValue = null;
                ar.Price = 0;
                ar.Cost = 0;
            }
            context.Value.SaveChanges();
        }

        public RenterRect GetAreaRenter(AreaRect area)
        {
            return context.Value.RentAreas
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
                RenterRect rr = context.Value.RentAreas
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

    
        public void UpdateAreaData(AreaRect old, AreaRect _new)
        {
            // найдем объект в базе с таким ид 
            var _old = context.Value.RentAreas.Where(x => x.Id == old.Id).FirstOrDefault();
            _old.Name = _new.AreaName;
            _old.Price = _new.Price;
            _old.Square = _new.Square;
            _old.Cost = _new.Cost;
            context.Value.SaveChanges();
        }

        public void DeleteArea(AreaRect old)
        {
            // найдем объект в базе с таким ид 
            context.Value.RentAreas.Remove(context.Value.RentAreas.Find(old.Id));
            context.Value.SaveChanges();
        }


    }
}
