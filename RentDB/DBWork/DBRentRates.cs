using RentLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentDB
{
    public class DBRentRates
    {
        readonly Lazy<RentModel> context = new Lazy<RentModel>();

        public IEnumerable<Rates> GetRentRates(int id)
        {
            return context.Value.RentRates.Where(r=> r.Area.Id == id).Select(s=>
            new Rates() 
            { Id = s.Id,
              StartDate = s.StartDate,
              EndDate = s.EndDate,
              Rate = s.Rate,
              Currency = s.Currency,
              ForWholeArea = s.ForWholeArea,
              Note = s.Note
            } ).OrderBy(s=> s.StartDate);
        }


        //public RentLibrary.Staff GetStaffById(int id)
        //{
        //    return context.Value.Staff.Select(s => new RentLibrary.Staff()
        //    {
        //        Id = s.Id,
        //        Name = s.Name,
        //        Duty = s.Duty,
        //        WorkPhone = s.WorkPhone,
        //        Email = s.Email,
        //        HomePhone = s.HomePhone,
        //        InternalPhone = s.InternalPhone
        //    }).FirstOrDefault();
        //}

        public void AddRentRate(Rates r_rate, int area_id)
        {
            RentArea ra = context.Value.RentAreas.Select(a=>a).Where(a=>a.Id == area_id).FirstOrDefault();

            if (ra != null)
            {
                context.Value.RentRates.Add(new RentRates()
                {
                    StartDate = r_rate.StartDate,
                    EndDate = r_rate.EndDate,
                    Rate = r_rate.Rate,
                    Currency = r_rate.Currency,
                    ForWholeArea = r_rate.ForWholeArea,
                    Note = r_rate.Note,
                    Area = ra
                }) ;
                context.Value.SaveChanges();
            }
        }

        public void UpdateRate(Rates oldR, Rates newR)
        {
            var sR = context.Value.RentRates.Where(s => s.Id == oldR.Id).FirstOrDefault();
            sR.StartDate = newR.StartDate;
            sR.EndDate = newR.EndDate;
            sR.Rate = newR.Rate;
            sR.Currency = newR.Currency;
            sR.ForWholeArea = newR.ForWholeArea;
            sR.Note = newR.Note;
            context.Value.SaveChanges();
        }

        public void DropRate(int id)
        {
            context.Value.RentRates.Remove(context.Value.RentRates.Select(s => s).Where(s => s.Id == id).FirstOrDefault());
            context.Value.SaveChanges();
        }

    }
}
