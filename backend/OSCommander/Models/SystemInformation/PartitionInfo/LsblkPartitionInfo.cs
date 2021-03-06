﻿namespace OSCommander.Models.SystemInformation.PartitionInfo
{
    public class LsblkPartitionInfo : IPartitionInfo
    {
        public string DeviceName => $"/dev/{Name}";
        public string Name { get; set; }
        public string MountingPoint { get; set; }
        public bool IsMain => MountingPoint != null && MountingPoint.Equals("/");
        public int MemoryInMB { get; set; }
        public string Uuid { get; set; }
    }
}
