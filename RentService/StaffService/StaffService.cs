using RentData;
using RentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentService
{
    public class StaffService : IStaffService
    {
        private readonly RentContext _context;

        public StaffService(RentContext context)
        {
            _context = context;
        }

        public void AddStaff(StaffDto staff)
        {
            _context.Staff.Add(new Staff()
            {
                Name = staff.Name,
                DateOfBirth = staff.DateOfBirth,
                EmpDate = staff.EmpDate,
                Duty = staff.Duty,
                WorkPhone = staff.WorkPhone,
                InternalPhone = staff.InternalPhone,
                Email = staff.Email,
                HomePhone = staff.HomePhone
            });
            _context.SaveChanges();
        }

        public void DropStaff(int id)
        {
            var staff = _context.Staff.Find(id);
            _context.Staff.Remove(staff);
            _context.SaveChanges();

        }

        public IEnumerable<StaffDto> GetAllStaff()
        {
           var staffs =  _context.Staff.Select(s => new StaffDto()
           {
               Id = s.Id,
               Name = s.Name,
               DateOfBirth = s.DateOfBirth,
               EmpDate = s.EmpDate,
               Duty = s.Duty,
               WorkPhone = s.WorkPhone,
               Email = s.Email,
               HomePhone = s.HomePhone,
               InternalPhone = s.InternalPhone
           }).OrderBy(s => s.Id);
            return staffs;
        }

        public StaffDto GetStaffById(int id)
        {
            return new StaffDto();   //_context.Staff.Find(id);
        }

        public void UpdateStaff(StaffDto newS)
        {
            var sU = _context.Staff.Where(s => s.Id == newS.Id).FirstOrDefault();
            sU.Name = newS.Name;
            sU.DateOfBirth = newS.DateOfBirth;
            sU.EmpDate = newS.EmpDate;
            sU.Duty = newS.Duty;
            sU.WorkPhone = newS.WorkPhone;
            sU.InternalPhone = newS.InternalPhone;
            sU.Email = newS.Email;
            sU.HomePhone = newS.HomePhone;

            _context.SaveChanges();
        }
    }
}
