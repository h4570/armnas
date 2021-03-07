namespace OSCommander.Models.SystemInformation.PartitionInfo
{
    public class DfPartitionInfo : IPartitionInfo
    {
        public string Name { get; set; }
        public string MountingPoint { get; set; }
        public bool IsMain => MountingPoint != null && MountingPoint.Equals("/");
        public int MemoryInMB { get; set; }
        public int UsedMemoryInMB { get; set; }
    }
}
