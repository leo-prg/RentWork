using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using RentLibrary;

namespace RentData
{
    public class DBRenter
    {
        readonly Lazy<RentContext> context = new Lazy<RentContext>();


        public void AddRenter(RenterRect renter)
        {
            context.Value.Renters.Add(new Renter()
            {
                RenterName = renter.RenterName,
                Contract = renter.Contract,
                StartDate = renter.StartDate,
                EndDate = renter.EndDate,
                ContactPerson = renter.ContactPerson,
                ContactPhone = renter.ContactPhone,
                Annotation = renter.Annotation,
                StartPartnership = renter.StartPartnership
            }) ;
            context.Value.SaveChanges();
        }

        public List<RenterRect> GetAllRenters()
        {
            return context.Value.Renters.Select(r => new RenterRect() 
                            { Id = r.Id, RenterName = r.RenterName, Contract = r.Contract, 
                              StartDate = r.StartDate, EndDate = r.EndDate, 
                              ContactPerson = r.ContactPerson, ContactPhone = r.ContactPhone, Annotation = r.Annotation, StartPartnership = r.StartPartnership
                            }).OrderBy(s=>s.Id).ToList();
        }

        public RenterRect GetRenterById(int id)
        {
            return context.Value.Renters.Where(r => r.Id == id).Select(
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


        public void UpdateRenter(RenterRect oldR, RenterRect newR)
        {
            var rU = context.Value.Renters.Where(r => r.Id == oldR.Id).FirstOrDefault();
            rU.RenterName = newR.RenterName;
            rU.Contract = newR.Contract;
            rU.StartDate = newR.StartDate;
            rU.EndDate = newR.EndDate;
            rU.ContactPerson = newR.ContactPerson;
            rU.ContactPhone = newR.ContactPhone;
            rU.Annotation = newR.Annotation;
            rU.StartPartnership = newR.StartPartnership;
            context.Value.SaveChanges();
        }

        public void DropRenter(RenterRect renter)
        {
            // найти все площади в которых айди арендатора удаляется -  и очистить
            var areasOfRenter = context.Value.RentAreas.Where(r => r.Renter.Id == renter.Id).Select(s => s);
            if (areasOfRenter != null)
            {
                foreach (RentArea area in areasOfRenter)
                {
                    area.Renter = null;
                    context.Value.SaveChanges();
                }
            }
            // затем уже удалить 
                context.Value.Renters.Remove(context.Value.Renters.Find(renter.Id));
                context.Value.SaveChanges();
        }

        // получаем отдельно файл с документом чтобы не загружать сразу все на страницу
        public byte[] GetFileById(int id)
        {
            return context.Value.RenterFiles.Where(x => x.Id == id).Select(s => s.Document).FirstOrDefault();
        }

        public void KillFile(int id)
        {
            var fl = context.Value.RenterFiles.Where(x => x.Id == id).FirstOrDefault();
            context.Value.RenterFiles.Remove(fl);
            context.Value.SaveChanges();
        }

        // получим список файлов прикрепленных к арендатору
        public IEnumerable<RenterFileList>  GetRenterFileList(int id)
        {
            return context.Value.RenterFiles.Where(x => x.Owner.Id == id).Select(x => new RenterFileList()
            { Id = x.Id, 
              Description = x.Description, 
              FileName = x.FileName, 
              FileType = x.FileType,
              FileSize = x.FileSize, 
              Modified = x.Modified });
        }


        public void AddRenterFile(int id, byte[] f, RenterFileList fr)
        {
            context.Value.RenterFiles.Add(new RenterFiles()
            {
                Owner = context.Value.Renters.Where(x => x.Id == id).FirstOrDefault(),
                Description = fr.Description, 
                FileName = fr.FileName, 
                FileType = fr.FileType, 
                FileSize = fr.FileSize, 
                Modified = fr.Modified, 
                Document = f
            }); ;
            context.Value.SaveChanges();
        }


    }
}
