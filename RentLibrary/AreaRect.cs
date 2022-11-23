using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentLibrary
{
    public class AreaRect
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public int LayerId { get; set; }
        public string LayerName { get; set; }
        public string AreaName { get; set; }
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }
        public float Square { get; set; }
        public float Price { get; set; }
        public float Cost { get; set; }
        public bool isRented { get;  set; }
        public string RenterName { get; set; }
        public string Currency { get; set; }
    }

    public class AreaPoint
    {
        public int PointNo { get; set; }
        public int PointX { get; set; }
        public int PointY { get; set; }
    }

  
  
}
