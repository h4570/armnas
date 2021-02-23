namespace OSCommander.Models.PartitionInfo
{
    public interface IPartitionInfo
    {
        string Name { get; set; }
        string MountingPoint { get; set; }
        bool IsMain { get; }
        int MemoryInMB { get; set; }
    }
}