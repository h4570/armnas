using System.Collections.Generic;
using OSCommander.Models.SystemInformation.PartitionInfo;

namespace OSCommander.Models.SystemInformation
{
    public class LsblkDiskInfo
    {
        public string Name { get; set; }
        public int MemoryInMB { get; set; }
        public List<LsblkPartitionInfo> Partitions { get; set; }
    }
}
