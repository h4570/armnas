using System.Collections.Generic;
using OSCommander.Models.PartitionInfo;

namespace OSCommander.Models
{
    public class LsblkDiskInfo
    {
        public string Name { get; set; }
        public int MemoryInMB { get; set; }
        public List<LsblkPartitionInfo> Partitions { get; set; }
    }
}
