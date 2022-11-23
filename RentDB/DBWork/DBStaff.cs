using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentDto;

namespace RentData
{
    public class DBStaff
    {
        readonly Lazy<RentContext> context = new Lazy<RentContext>();

        public IEnumerable<StaffDto> GetAllStaff()
        {
            return context.Value.Staff.Select(s=> new StaffDto() { Id = s.Id, Name = s.Name, DateOfBirth = s.DateOfBirth, EmpDate = s.EmpDate,  Duty = s.Duty, 
                WorkPhone = s.WorkPhone,  Email = s.Email,  HomePhone = s.HomePhone, InternalPhone = s.InternalPhone } ).OrderBy(s=> s.Id);
        }


        public StaffDto GetStaffById(int id)
        {
            return context.Value.Staff.Select(s => new StaffDto()
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
            }).FirstOrDefault();
        }

        public void AddStaff(RentDto.StaffDto staff)
        {
            context.Value.Staff.Add(new Staff()
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
            context.Value.SaveChanges();
        }

        public void UpdateStaff(StaffDto oldS, StaffDto newS)
        {
            var sU = context.Value.Staff.Where(s => s.Id == oldS.Id).FirstOrDefault();
            sU.Name = newS.Name;
            sU.DateOfBirth = newS.DateOfBirth;
            sU.EmpDate = newS.EmpDate;
            sU.Duty = newS.Duty;
            sU.WorkPhone = newS.WorkPhone;
            sU.InternalPhone = newS.InternalPhone;
            sU.Email = newS.Email;
            sU.HomePhone = newS.HomePhone;
            context.Value.SaveChanges();
        }

        public void DropStaff(int id)
        {
            context.Value.Staff.Remove(context.Value.Staff.Select(s=>s).Where(s => s.Id == id).FirstOrDefault());
            context.Value.SaveChanges();
        }

    }
}
