namespace OSCommander.Models.PartitionInfo
{
    public class LsblkPartitionInfo : IPartitionInfo
    {
        public string Name { get; set; }
        public string MountingPoint { get; set; }
        public bool IsMain => MountingPoint != null && MountingPoint.Equals("/");
        public int MemoryInMB { get; set; }
    }
}
