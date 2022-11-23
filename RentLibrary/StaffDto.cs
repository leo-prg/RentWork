using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentDto
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? EmpDate { get; set; }
        public string Duty { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
        public string WorkPhone { get; set; }
        public string InternalPhone { get; set; }
    }
}
