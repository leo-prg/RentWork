using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentDto
{
    public class Rates
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Rate  { get; set; }
        public string Currency { get; set; }
        public bool ForWholeArea { get; set; }
        public string Note { get; set; }
    }
}
