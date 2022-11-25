using RentData;
using RentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentService
{
    public class LayerService : ILayerService
    {
        private readonly RentContext _context;

        public LayerService(RentContext context)
        {
            _context = context;
        }

        public void AddObjectLayer(int oId, LayerRect lRect)
        {
            // найдем объект в базе с таким адресом и названием и в него будем добавлять введенный пользователем слой;
            var dbo = _context.RentObjects.Where(x => x.Id == oId).FirstOrDefault();
            if (dbo != null)
            {
                _context.RentLayers.Add(new RentLayer()
                {
                    Name = lRect.Name,
                    LayerFileName = lRect.FileName,
                    Object = dbo,
                    LayerFile = lRect.LayerFile,
                    Height = lRect.Height,
                    Width = lRect.Width
                });
                _context.SaveChanges();
            }
        }

        public void DeleteLayer(int id)
        {
            _context.RentLayers.Remove(_context.RentLayers.Find(id));
            _context.SaveChanges();
        }

        public List<LayerRect> GetLayersOfObject(ObjectRect oRect)
        {
            var dbo = _context.RentObjects.Where(x => x.Id == oRect.Id).FirstOrDefault();
            return _context.RentLayers
                .Where(x => x.Object.Id == dbo.Id)
                .Select(x => new LayerRect() { Id = x.Id, Name = x.Name, FileName = x.LayerFileName,
                    LayerFile = x.LayerFile, Width = x.Width, Height = x.Height }).ToList();
        }

        public List<LayerRect> GetLayersOfObject(int id)
        {
            return _context.RentLayers
                 .Where(x => x.Object.Id == id)
                 .Select(x => new LayerRect()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     FileName = x.LayerFileName,
                     LayerFile = x.LayerFile,
                     Width = x.Width,
                     Height = x.Height
                 }).ToList();
        }

        public List<LayerRect> GetObjectLayersList(int id)
        {
            return _context.RentLayers
               .Where(x => x.Object.Id == id)
               .Select(x => new LayerRect()
               {
                   Id = x.Id,
                   Name = x.Name,
               }).ToList();
        }

        public void UpdateObjectLayer(int oldId, LayerRect newL)
        {
            // найдем объект в базе с таким ид и в него будем добавлять введенный пользователем слой;
            var old_layer = _context.RentLayers.Where(x => x.Id == oldId).FirstOrDefault();
            old_layer.Name = newL.Name;
            old_layer.LayerFileName = newL.FileName;
            old_layer.LayerFile = newL.LayerFile;
            old_layer.Width = newL.Width;
            old_layer.Height = newL.Height;
            // нужно удалить все связанные площади при замене файла слоя ????
            // замену файла слоя производить только при перепланировке, площади связанные будут
            // переделаны согласно нового плана и будут корректно отображаться на схеме
            _context.SaveChanges();
        }
    }
}
