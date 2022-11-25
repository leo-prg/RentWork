using RentData;
using RentDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentService
{
    public class RateService : IRateService
    {
        private readonly RentContext _context;

        public RateService(RentContext context)
        {
            _context = context;
        }

        public void AddRentRate(Rates r_rate, int area_id)
        {
            RentArea ra = _context.RentAreas.Select(a => a).Where(a => a.Id == area_id).FirstOrDefault();

            if (ra != null)
            {
                _context.RentRates.Add(new RentRates()
                {
                    StartDate = r_rate.StartDate,
                    EndDate = r_rate.EndDate,
                    Rate = r_rate.Rate,
                    Currency = r_rate.Currency,
                    ForWholeArea = r_rate.ForWholeArea,
                    Note = r_rate.Note,
                    Area = ra
                });
                _context.SaveChanges();
            }
        }

        public void DropRate(int id)
        {
            _context.RentRates.Remove(_context.RentRates.Select(s => s).Where(s => s.Id == id).FirstOrDefault());
            _context.SaveChanges();
        }

        public IEnumerable<Rates> GetRentRates(int id)
        {
            return _context.RentRates.Where(r => r.Area.Id == id).Select(s =>
             new Rates()
             {
                 Id = s.Id,
                 StartDate = s.StartDate,
                 EndDate = s.EndDate,
                 Rate = s.Rate,
                 Currency = s.Currency,
                 ForWholeArea = s.ForWholeArea,
                 Note = s.Note
             }).OrderBy(s => s.StartDate);
        }

        public void UpdateRate(Rates oldR, Rates newR)
        {
            var sR = _context.RentRates.Where(s => s.Id == oldR.Id).FirstOrDefault();
            sR.StartDate = newR.StartDate;
            sR.EndDate = newR.EndDate;
            sR.Rate = newR.Rate;
            sR.Currency = newR.Currency;
            sR.ForWholeArea = newR.ForWholeArea;
            sR.Note = newR.Note;
            _context.SaveChanges();
        }
    }
}
