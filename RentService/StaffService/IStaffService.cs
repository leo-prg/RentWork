using System;
using System.Collections.Generic;
using System.Text;
using RentDto;

namespace RentService.StaffService
{
    public interface IStaffService
    {
        IEnumerable<StaffDto> GetAllStaff();
        StaffDto GetStaffById(int id);
        void AddStaff(StaffDto staff);
        void UpdateStaff(StaffDto newS);
        void DropStaff(int id);

    }
}
