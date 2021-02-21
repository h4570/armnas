using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCommander.Models
{
    public class DiskInfo
    {
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int MemoryInMB { get; set; }
        public int UsedMemoryInMB { get; set; }
    }
}
