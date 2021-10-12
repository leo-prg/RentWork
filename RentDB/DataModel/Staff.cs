using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RentDB
{

    [Table("Staff")]
    public class Staff
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Duty { get; set; }
        [StringLength(50)]
        public string HomePhone { get; set; }
        [StringLength(30)] 
        public string Email { get; set; }
        [StringLength(20)]
        public string WorkPhone { get; set; }
        [StringLength(3)]
        public string InternalPhone { get; set; }
    }
}
