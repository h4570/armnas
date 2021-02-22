namespace OSCommander.Models
{
    public class RAMInfo
    {
        /** Total physical amount of RAM */
        public int TotalInKB { get; set; }
        /** The amount of physical RAM, in kilobytes, left unused by the system. */
        public int FreeInKB { get; set; }
        /** An estimate of how much memory is available for starting new applications, without swapping */
        public int AvailableInKB { get; set; }
    }
}
