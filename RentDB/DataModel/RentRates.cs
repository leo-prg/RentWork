using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RentDB
{

    [Table("RentRates")]
    public class RentRates
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Rate { get; set; }
        public bool ForWholeArea { get; set; }
        [StringLength(100)]
        public string Note { get; set; }
        public RentArea Area {get; set; }
    }
}
