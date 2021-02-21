
using System;
using System.Collections.Generic;
using OSCommander.Models;

namespace OSCommander.Mockup
{
    internal class SystemInformation : ISystemInformation
    {

        public string GetDistributionName() { return "Windows 10"; }

        public string GetKernelName() { return "20H2"; }

        public CPUInfo GetCPUInfo()
        {
            return new()
            {
                Name = "Intel Core i7-7280HQ",
                PercentageUsage = 12,
                Temperature = 48
            };
        }

        public RAMInfo GetRAMInfo()
        {
            return new()
            {
                TotalInKB = 1638956,
                FreeInKB = 1192544,
                AvailableInKB = 2038220
            };
        }

        public IEnumerable<DiskInfo> GetDisksInfo()
        {
            return new List<DiskInfo>()
            {
                new ()
                {
                    Name = "C",
                    MemoryInMB = 952320,
                    UsedMemoryInMB = 627712,
                    IsMain = true
                },
                new()
                {
                    Name = "D",
                    MemoryInMB = 519925,
                    UsedMemoryInMB = 177113,
                    IsMain = false
                }
            };
        }

        public DiskInfo GetDiskInfo(string diskName)
        {
            return new()
            {
                Name = "C",
                MemoryInMB = 952320,
                UsedMemoryInMB = 627712,
                IsMain = true
            };
        }

        public string GetIP() { return "192.168.0.155"; }

        public DateTime? GetStartTime() { return DateTime.Now.AddDays(-5).AddMinutes(-172); }

    }
}
