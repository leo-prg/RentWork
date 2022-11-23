using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentData
{
    [Table("Renters")]
    public class Renter
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string RenterName { get; set; }
        [StringLength(20)]
        public string Contract { get; set; }
        
        public DateTime StartPartnership { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string Annotation { get; set; }
        public virtual List<RentArea> RentAreas { get; set; }
        public virtual List<RenterFiles> RenterFiles { get; set; }
        public string AccCode { get; set; } // внутренний код бухгалтерии
        public Renter()
        {
            RentAreas = new List<RentArea>();
        }
    }
}
