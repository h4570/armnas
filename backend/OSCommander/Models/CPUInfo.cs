using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCommander.Models
{
    public class CPUInfo
    {
        public string Name { get; set; }
        public decimal PercentageUsage { get; set; }
        public decimal Temperature { get; set; }
    }
}
