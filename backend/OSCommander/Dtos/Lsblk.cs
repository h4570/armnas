using System.Collections.Generic;
using System.Text.Json.Serialization;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace OSCommander.Dtos
{

    public class Lsblk
    {
        [JsonPropertyName("blockdevices")]
        public List<BlockDevice> BlockDevices { get; set; }
    }

    public class BlockDevice
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("kname")]
        public string KName { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("majmin")]
        public string MajMin { get; set; }
        [JsonPropertyName("fsavail")]
        public string FsAvailable { get; set; }
        [JsonPropertyName("fssize")]
        public string FsSize { get; set; }
        [JsonPropertyName("fstype")]
        public object FsType { get; set; }
        [JsonPropertyName("fsused")]
        public string FsUsed { get; set; }
        [JsonPropertyName("fsuse")]
        public string FsUse { get; set; }
        [JsonPropertyName("mountpoint")]
        public string MountPoint { get; set; }
        [JsonPropertyName("label")]
        public object Label { get; set; }
        [JsonPropertyName("uuid")]
        public object Uuid { get; set; }
        [JsonPropertyName("ptuuid")]
        public string PtUuid { get; set; }
        [JsonPropertyName("pttype")]
        public string PtType { get; set; }
        [JsonPropertyName("parttype")]
        public object PartType { get; set; }
        [JsonPropertyName("partlabel")]
        public object PartLabel { get; set; }
        [JsonPropertyName("partuuid")]
        public object PartUuid { get; set; }
        [JsonPropertyName("partflags")]
        public object PartFlags { get; set; }
        [JsonPropertyName("ra")]
        public int Ra { get; set; }
        [JsonPropertyName("ro")]
        public bool Ro { get; set; }
        [JsonPropertyName("rm")]
        public bool Rm { get; set; }
        [JsonPropertyName("hotplug")]
        public bool HotPlug { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("serial")]
        public string Serial { get; set; }
        [JsonPropertyName("size")]
        public string Size { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("owner")]
        public string Owner { get; set; }
        [JsonPropertyName("group")]
        public string Group { get; set; }
        [JsonPropertyName("mode")]
        public string Mode { get; set; }
        [JsonPropertyName("alignment")]
        public int Alignment { get; set; }
        [JsonPropertyName("minio")]
        public int MinIo { get; set; }
        [JsonPropertyName("optio")]
        public int OptIo { get; set; }
        [JsonPropertyName("physec")]
        public int PhySec { get; set; }
        [JsonPropertyName("logsec")]
        public int LogSec { get; set; }
        [JsonPropertyName("rota")]
        public bool Rota { get; set; }
        [JsonPropertyName("sched")]
        public string Schedule { get; set; }
        [JsonPropertyName("rqsize")]
        public int RqSize { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("discaln")]
        public int DiscAln { get; set; }
        [JsonPropertyName("discgran")]
        public string DiscGran { get; set; }
        [JsonPropertyName("discmax")]
        public string DiscMax { get; set; }
        [JsonPropertyName("disczero")]
        public bool DiscZero { get; set; }
        [JsonPropertyName("wsame")]
        public string Wsame { get; set; }
        [JsonPropertyName("wwn")]
        public string Wwn { get; set; }
        [JsonPropertyName("rand")]
        public bool Rand { get; set; }
        [JsonPropertyName("pkname")]
        public object PkName { get; set; }
        [JsonPropertyName("hctl")]
        public string Hctl { get; set; }
        [JsonPropertyName("tran")]
        public string Tran { get; set; }
        [JsonPropertyName("subsystems")]
        public string SubSystems { get; set; }
        [JsonPropertyName("rev")]
        public string Revision { get; set; }
        [JsonPropertyName("vendor")]
        public string Vendor { get; set; }
        [JsonPropertyName("zoned")]
        public string Zoned { get; set; }
        [JsonPropertyName("children")]
        public List<Child> Children { get; set; }
    }

    public class Child
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("kname")]
        public string KName { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("majmin")]
        public string MajMin { get; set; }
        [JsonPropertyName("fsavail")]
        public string FsAvailable { get; set; }
        [JsonPropertyName("fssize")]
        public string FsSize { get; set; }
        [JsonPropertyName("fstype")]
        public string FsType { get; set; }
        [JsonPropertyName("fsused")]
        public string FsUsed { get; set; }
        [JsonPropertyName("fsuse")]
        public string FsUse { get; set; }
        [JsonPropertyName("mountpoint")]
        public string MountPoint { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }
        [JsonPropertyName("ptuuid")]
        public string PtUuid { get; set; }
        [JsonPropertyName("pttype")]
        public string PtType { get; set; }
        [JsonPropertyName("parttype")]
        public string PartType { get; set; }
        [JsonPropertyName("partlabel")]
        public object PartLabel { get; set; }
        [JsonPropertyName("partuuid")]
        public string PartUuid { get; set; }
        [JsonPropertyName("partflags")]
        public string PartFlags { get; set; }
        [JsonPropertyName("ra")]
        public int Ra { get; set; }
        [JsonPropertyName("ro")]
        public bool Ro { get; set; }

        [JsonPropertyName("rm")]
        public bool Rm { get; set; }
        [JsonPropertyName("hotplug")]
        public bool HotPlug { get; set; }
        [JsonPropertyName("model")]
        public object Model { get; set; }
        [JsonPropertyName("serial")]
        public object Serial { get; set; }
        [JsonPropertyName("size")]
        public string Size { get; set; }
        [JsonPropertyName("state")]
        public object State { get; set; }
        [JsonPropertyName("owner")]
        public string Owner { get; set; }
        [JsonPropertyName("group")]
        public string Group { get; set; }
        [JsonPropertyName("mode")]
        public string Mode { get; set; }
        [JsonPropertyName("alignment")]
        public int Alignment { get; set; }
        [JsonPropertyName("minio")]
        public int MinIo { get; set; }
        [JsonPropertyName("optio")]
        public int OptIo { get; set; }
        [JsonPropertyName("physec")]
        public int PhySec { get; set; }
        [JsonPropertyName("logsec")]
        public int LogSec { get; set; }
        [JsonPropertyName("rota")]
        public bool Rota { get; set; }
        [JsonPropertyName("sched")]
        public string Schedule { get; set; }
        [JsonPropertyName("rqsize")]
        public int RqSize { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("discaln")]
        public int DisCaln { get; set; }
        [JsonPropertyName("discgran")]
        public string DiscGran { get; set; }
        [JsonPropertyName("discmax")]
        public string DiscMax { get; set; }
        [JsonPropertyName("disczero")]
        public bool DiscZero { get; set; }
        [JsonPropertyName("wsame")]
        public string Wsame { get; set; }
        [JsonPropertyName("wwn")]
        public string Wwn { get; set; }
        [JsonPropertyName("rand")]
        public bool Rand { get; set; }
        [JsonPropertyName("pkname")]
        public string PkName { get; set; }
        [JsonPropertyName("hctl")]
        public object Hctl { get; set; }
        [JsonPropertyName("tran")]
        public object Tran { get; set; }
        [JsonPropertyName("subsystems")]
        public string SubSystems { get; set; }
        [JsonPropertyName("rev")]
        public object Revision { get; set; }
        [JsonPropertyName("vendor")]
        public object Vendor { get; set; }
        [JsonPropertyName("zoned")]
        public string Zoned { get; set; }
    }


}
