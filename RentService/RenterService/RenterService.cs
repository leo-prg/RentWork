using RentData;
using RentDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RentService
{
    public class RenterService : IRenterService
    {
        private readonly RentContext _context;

        public RenterService(RentContext context)
        {
            _context = context;
        }

        public void AddRenter(RenterRect renter)
        {
            _context.Renters.Add(new Renter()
            {
                RenterName = renter.RenterName,
                Contract = renter.Contract,
                StartDate = renter.StartDate,
                EndDate = renter.EndDate,
                ContactPerson = renter.ContactPerson,
                ContactPhone = renter.ContactPhone,
                Annotation = renter.Annotation,
                StartPartnership = renter.StartPartnership
            });
            _context.SaveChanges();
        }

        public void AddRenterFile(int id, byte[] f, RenterFileList fr)
        {
            _context.RenterFiles.Add(new RenterFiles()
            {
                Owner = _context.Renters.Where(x => x.Id == id).FirstOrDefault(),
                Description = fr.Description,
                FileName = fr.FileName,
                FileType = fr.FileType,
                FileSize = fr.FileSize,
                Modified = fr.Modified,
                Document = f
            }); ;
            _context.SaveChanges();
        }

        public void DropRenter(RenterRect renter)
        {
            // найти все площади в которых айди арендатора удаляется -  и очистить
            var areasOfRenter = _context.RentAreas.Where(r => r.Renter.Id == renter.Id).Select(s => s);
            if (areasOfRenter != null)
            {
                foreach (RentArea area in areasOfRenter)
                {
                    area.Renter = null;
                    _context.SaveChanges();
                }
            }
            // затем уже удалить 
            _context.Renters.Remove(_context.Renters.Find(renter.Id));
            _context.SaveChanges();
        }

        public List<RenterRect> GetAllRenters()
        {
            return _context.Renters.Select(r => new RenterRect()
            {
                Id = r.Id,
                RenterName = r.RenterName,
                Contract = r.Contract,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                ContactPerson = r.ContactPerson,
                ContactPhone = r.ContactPhone,
                Annotation = r.Annotation,
                StartPartnership = r.StartPartnership
            }).OrderBy(s => s.Id).ToList();
        }
        
        // получаем отдельно файл с документом чтобы не загружать сразу все на страницу
        public byte[] GetFileById(int id)
        {
            return _context.RenterFiles.Where(x => x.Id == id).Select(s => s.Document).FirstOrDefault();
        }

        public RenterRect GetRenterById(int id)
        {
            return _context.Renters.Where(r => r.Id == id).Select(
                x => new RenterRect()
                {
                    Id = x.Id,
                    RenterName = x.RenterName,
                    Contract = x.Contract,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    ContactPerson = x.ContactPerson,
                    ContactPhone = x.ContactPhone,
                    Annotation = x.Annotation,
                    StartPartnership = x.StartPartnership
                }).FirstOrDefault();
        }

        // получим список файлов прикрепленных к арендатору
        public IEnumerable<RenterFileList> GetRenterFileList(int id)
        {
            return _context.RenterFiles.Where(x => x.Owner.Id == id).Select(x => new RenterFileList()
            {
                Id = x.Id,
                Description = x.Description,
                FileName = x.FileName,
                FileType = x.FileType,
                FileSize = x.FileSize,
                Modified = x.Modified
            });
        }

        public void KillFile(int id)
        {
            var fl = _context.RenterFiles.Where(x => x.Id == id).FirstOrDefault();
            _context.RenterFiles.Remove(fl);
            _context.SaveChanges();
        }

        public void UpdateRenter(RenterRect oldR, RenterRect newR)
        {
            var rU = _context.Renters.Where(r => r.Id == oldR.Id).FirstOrDefault();
            rU.RenterName = newR.RenterName;
            rU.Contract = newR.Contract;
            rU.StartDate = newR.StartDate;
            rU.EndDate = newR.EndDate;
            rU.ContactPerson = newR.ContactPerson;
            rU.ContactPhone = newR.ContactPhone;
            rU.Annotation = newR.Annotation;
            rU.StartPartnership = newR.StartPartnership;
            _context.SaveChanges();
        }
    }
}
