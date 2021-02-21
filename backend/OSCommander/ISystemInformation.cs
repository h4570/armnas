using System;
using System.Collections.Generic;
using OSCommander.Models;

namespace OSCommander
{
    public interface ISystemInformation
    {
        public string GetDistributionName();
        public string GetKernelName();
        public CPUInfo GetCPUInfo();
        public RAMInfo GetRAMInfo();
        public IEnumerable<DiskInfo> GetDisksInfo();
        public DiskInfo GetDiskInfo(string diskName);
        public string GetIP();
        public DateTime? GetStartTime();
    }
}
