using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.Internal
{

    [Index(nameof(Uuid))]
    public class PartitionBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Uuid { get; set; }
        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }
    }

    public class Partition : PartitionBase
    {
        public Partition() { }

        public Partition(PartitionODataHack hack)
        {
            Id = hack.Id;
            Uuid = hack.Uuid;
            DisplayName = hack.DisplayName;
        }

    }

    /// <summary>
    /// Sandro: You can think, WTF is that?
    /// Why just not use one Partition class?
    /// Reason: At the moment of writing this code (18.04.2021) oData had weird bug.
    /// I couldn't use a Partition class as a [BodyParameter] in POST method. POST payload was always null.
    /// When I split Partition class to two instances - first 'Partition' for OData configuration,
    /// second 'PartitionODataHack' for body param, everything started working.
    /// I think that you can remove these two implementations in future and
    /// rename PartitionBase to Partition. After this, please check if POST/PATCH message work!
    /// </summary>
    public class PartitionODataHack : PartitionBase { }

}
