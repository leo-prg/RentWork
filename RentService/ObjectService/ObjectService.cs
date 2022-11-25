using RentData;
using RentDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentService
{
    public class ObjectService : IObjectService
    {

        private readonly RentContext _context;

        public ObjectService(RentContext context)
        {
            _context = context;
        }

        public void AddObject(ObjectRect obj)
        {
            _context.RentObjects.Add(new RentObject()
            { Name = obj.Name, Address = obj.Address, CNo = obj.CNo, Lat = obj.Lat, Lon = obj.Lon }); ;
            _context.SaveChanges();
        }

        public List<ObjectRect> GetAllObjects()
        {
            return _context.RentObjects.Select(x => new ObjectRect()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                CNo = x.CNo,
                Lat = x.Lat,
                Lon = x.Lon
            }).ToList();
        }

        public ObjectRect GetObjectById(int id)
        {
            return _context.RentObjects.Where(x => x.Id == id).Select(x => new ObjectRect()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                CNo = x.CNo,
                Lat = x.Lat,
                Lon = x.Lon
            }).FirstOrDefault();
        }

        public void RemoveObject(ObjectRect obj)
        {
            var to_del = _context.RentObjects.Find(obj.Id);
            _context.RentObjects.Remove(to_del);
            _context.SaveChanges();
        }

        public void UpdateObject(ObjectRect oldO, ObjectRect newO)
        {
            var ro = _context.RentObjects.Find(oldO.Id);
            ro.Name = newO.Name;
            ro.Address = newO.Address;
            ro.CNo = newO.CNo;
            ro.Lat = newO.Lat;
            ro.Lon = newO.Lon;
            _context.SaveChanges();
        }
    }
}
