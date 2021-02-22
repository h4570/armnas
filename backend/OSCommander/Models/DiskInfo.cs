namespace OSCommander.Models
{
    public class DiskInfo
    {
        public string Name { get; set; }
        /** Is mounted on "/"? (OS disk) */
        public bool IsMain { get; set; }
        public int MemoryInMB { get; set; }
        public int UsedMemoryInMB { get; set; }
    }
}
