using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentDto
{
    public class RenterFileList : IDisposable
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public decimal FileSize { get; set; }
        public DateTime Modified { get; set; }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}
