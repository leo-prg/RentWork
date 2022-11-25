using RentDto;
using System;
using System.Collections.Generic;

namespace RentService
{
    public interface IRateService
    {
        IEnumerable<Rates> GetRentRates(int id);
        void AddRentRate(Rates r_rate, int area_id);
        void UpdateRate(Rates oldR, Rates newR);
        void DropRate(int id);
    }
}
