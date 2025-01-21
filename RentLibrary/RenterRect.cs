using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentLibrary
{
    public class RenterRect
    {
        public int Id { get; set; }
        public string RenterName { get; set; }
        public int UNP { get; set; }
        public string Contract { get; set; }
        public int ContractNo => 
        Convert.ToInt32(String.IsNullOrEmpty(Regex.Replace(Contract, "[A-Za-zА-Яа-я/-]", ""))?"0": Regex.Replace(Contract, "[A-Za-zА-Яа-я/-]", ""));
        public DateTime StartPartnership { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string Annotation { get; set; }

    }
}
