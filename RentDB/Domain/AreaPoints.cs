using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace RentData
{

    [Table("AreaPoints")]
    public class AreaPoints
    {
        public int Id { get; set; }
        public RentArea Area { get; set; }
        public int PointNo { get; set; }
        public int PointX { get; set; }
        public int PointY { get; set; }
    }
}
