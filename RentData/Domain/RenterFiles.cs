using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RentData

{
    [Table("RenterFiles")]
    public class RenterFiles
    {
        public int Id { get; set; }
        public byte[] Document { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public decimal FileSize { get; set; }
        public DateTime Modified { get; set; }
        public virtual Renter Owner { get; set; }

    }
}
