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
        [JsonPropertyName("majmin")]
        public string MajMin { get; set; }
        [JsonPropertyName("rm")]
        public bool Rm { get; set; }
        [JsonPropertyName("size")]
        public string Size { get; set; }
        [JsonPropertyName("ro")]
        public bool Ro { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("mountpoint")]
        public string MountPoint { get; set; }
        [JsonPropertyName("children")]
        public List<Child> Children { get; set; }
    }

    public class Child
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("majmin")]
        public string MajMin { get; set; }
        [JsonPropertyName("rm")]
        public bool Rm { get; set; }
        [JsonPropertyName("size")]
        public string Size { get; set; }
        [JsonPropertyName("ro")]
        public bool Ro { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("mountpoint")]
        public string MountPoint { get; set; }
    }

}
