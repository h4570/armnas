using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.Internal
{

    [Index(nameof(Uuid))]
    public class Partition
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
}
